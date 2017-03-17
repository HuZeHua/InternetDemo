﻿
namespace Anycmd.Engine.Host.Edi.MemorySets
{
    using Bus;
    using Engine.Edi;
    using Engine.Edi.Abstractions;
    using Engine.Edi.Messages;
    using Entities;
    using Exceptions;
    using Repositories;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Util;

    /// <summary>
    /// 
    /// </summary>
    internal sealed class ProcesseSet : IProcesseSet, IMemorySet
    {
        public static readonly IProcesseSet Empty = new ProcesseSet(EmptyAcDomain.SingleInstance);

        private readonly Dictionary<Guid, ProcessDescriptor> _dic = new Dictionary<Guid, ProcessDescriptor>();
        private bool _initialized = false;
        private static readonly object Locker = new object();

        private readonly Guid _id = Guid.NewGuid();
        private readonly IAcDomain _acDomain;

        public Guid Id
        {
            get { return _id; }
        }

        public ProcesseSet(IAcDomain acDomain)
        {
            if (acDomain == null)
            {
                throw new ArgumentNullException("acDomain");
            }
            if (acDomain.Equals(EmptyAcDomain.SingleInstance))
            {
                _initialized = true;
            }
            this._acDomain = acDomain;
            new MessageHandler(this).Register();
        }

        /// <summary>
        /// 根据发送策略名索引发送策略
        /// </summary>
        /// <param name="processId">发送策略名</param>
        /// <exception cref="GeneralException">当给定名称的发送策略不存在时引发</exception>
        /// <returns></returns>
        /// <exception cref="GeneralException">当进程标识非法时抛出</exception>
        public ProcessDescriptor this[Guid processId]
        {
            get
            {
                if (!_initialized)
                {
                    Init();
                }
                if (!_dic.ContainsKey(processId))
                {
                    throw new GeneralException("意外的进程标识");
                }

                return _dic[processId];
            }
        }

        public bool ContainsProcess(Guid processId)
        {
            if (!_initialized)
            {
                Init();
            }
            return _dic.ContainsKey(processId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="process"></param>
        /// <returns></returns>
        public bool TryGetProcess(Guid processId, out ProcessDescriptor process)
        {
            if (!_initialized)
            {
                Init();
            }
            return _dic.TryGetValue(processId, out process);
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Refresh()
        {
            if (_initialized)
            {
                _initialized = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerator<ProcessDescriptor> GetEnumerator()
        {
            if (!_initialized)
            {
                Init();
            }
            return _dic.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            if (!_initialized)
            {
                Init();
            }
            return _dic.Values.GetEnumerator();
        }

        private void Init()
        {
            if (_initialized) return;
            lock (Locker)
            {
                if (_initialized) return;
                _acDomain.MessageDispatcher.DispatchMessage(new MemorySetInitingEvent(this));
                _dic.Clear();
                var processes = _acDomain.RetrieveRequiredService<INodeHostBootstrap>().GetProcesses();
                foreach (var process in processes)
                {
                    _dic.Add(process.Id, new ProcessDescriptor(_acDomain, ProcessState.Create(process), process.Id));
                }
                _initialized = true;
                _acDomain.MessageDispatcher.DispatchMessage(new MemorySetInitializedEvent(this));
            }
        }

        #region MessageHandler
        private class MessageHandler : 
            IHandler<AddProcessCommand>,
            IHandler<UpdateProcessCommand>,
            IHandler<RemoveProcessCommand>,
            IHandler<ChangeProcessCatalogCommand>,
            IHandler<ChangeProcessNetPortCommand>
        {
            private readonly ProcesseSet _set;

            public MessageHandler(ProcesseSet set)
            {
                this._set = set;
            }

            public void Register()
            {
                var messageDispatcher = _set._acDomain.MessageDispatcher;
                if (messageDispatcher == null)
                {
                    throw new ArgumentNullException("AcDomain对象'{0}'尚未设置MessageDispatcher。".Fmt(_set._acDomain.Name));
                }
                messageDispatcher.Register((IHandler<AddProcessCommand>)this);
                messageDispatcher.Register((IHandler<UpdateProcessCommand>)this);
                messageDispatcher.Register((IHandler<RemoveProcessCommand>)this);
                messageDispatcher.Register((IHandler<ChangeProcessCatalogCommand>)this);
                messageDispatcher.Register((IHandler<ChangeProcessNetPortCommand>)this);
            }

            public void Handle(AddProcessCommand message)
            {
                var acDomain = _set._acDomain;
                var processRepository = _set._acDomain.RetrieveRequiredService<IRepository<Process, Guid>>();
                if (!message.Input.Id.HasValue)
                {
                    throw new ValidationException("标识是必须的");
                }
                if (acDomain.NodeHost.Processs.ContainsProcess(message.Input.Id.Value))
                {
                    throw new ValidationException("给定标识标识的记录已经存在");
                }

                var entity = Process.Create(message.Input);

                lock (Locker)
                {
                    if (!_set._dic.ContainsKey(entity.Id))
                    {
                        _set._dic.Add(entity.Id, new ProcessDescriptor(acDomain, ProcessState.Create(entity), entity.Id));
                    }
                    try
                    {
                        processRepository.Add(entity);
                        processRepository.Context.Commit();
                    }
                    catch
                    {
                        if (_set._dic.ContainsKey(entity.Id))
                        {
                            _set._dic.Remove(entity.Id);
                        }
                        processRepository.Context.Rollback();
                        throw;
                    }
                }
                _set._acDomain.PublishEvent(new ProcessAddedEvent(message.AcSession, entity));
                _set._acDomain.CommitEventBus();
            }

            public void Handle(UpdateProcessCommand message)
            {
                var acDomain = _set._acDomain;
                var processRepository = _set._acDomain.RetrieveRequiredService<IRepository<Process, Guid>>();
                if (!acDomain.NodeHost.Processs.ContainsProcess(message.Input.Id))
                {
                    throw new NotExistException();
                }
                var entity = processRepository.GetByKey(message.Input.Id);
                if (entity == null)
                {
                    throw new NotExistException();
                }
                var bkState = _set._dic[entity.Id];

                entity.Update(message.Input);

                var newState = new ProcessDescriptor(acDomain, ProcessState.Create(entity), entity.Id);
                bool stateChanged = newState != bkState;
                lock (Locker)
                {
                    if (stateChanged)
                    {
                        Update(newState);
                    }
                }
                try
                {
                    processRepository.Update(entity);
                    processRepository.Context.Commit();
                }
                catch
                {
                    if (stateChanged)
                    {
                        Update(bkState);
                    }
                    processRepository.Context.Rollback();
                    throw;
                }
                if (stateChanged)
                {
                    _set._acDomain.PublishEvent(new ProcessUpdatedEvent(message.AcSession, entity));
                    _set._acDomain.CommitEventBus();
                }
            }

            private void Update(ProcessDescriptor state)
            {
                _set._dic[state.Process.Id] = state;
            }

            public void Handle(RemoveProcessCommand message)
            {
                // TODO:处理RemoveProcessCommand命令
            }

            public void Handle(ChangeProcessCatalogCommand message)
            {
                // TODO:处理ChangeProcessCatalogCommand命令
            }

            public void Handle(ChangeProcessNetPortCommand message)
            {
                // TODO:处理ChangeProcessNetPortCommand命令
            }
        }
        #endregion
    }
}