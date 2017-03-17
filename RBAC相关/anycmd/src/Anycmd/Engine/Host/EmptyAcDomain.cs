﻿
namespace Anycmd.Engine.Host
{
    using Ac;
    using Ac.Infra;
    using Bus;
    using Edi;
    using Edi.Handlers;
    using Engine.Ac;
    using Engine.Ac.Accounts;
    using Engine.Ac.AppSystems;
    using Engine.Ac.Catalogs;
    using Engine.Ac.Dsd;
    using Engine.Ac.EntityTypes;
    using Engine.Ac.Functions;
    using Engine.Ac.Groups;
    using Engine.Ac.Privileges;
    using Engine.Ac.Roles;
    using Engine.Ac.Ssd;
    using Engine.Ac.UiViews;
    using Engine.Edi.Abstractions;
    using Engine.Rdb;
    using Hecp;
    using IdGenerators;
    using Logging;
    using Serialization;
    using System;
    using System.Collections.Generic;

    public class EmptyAcDomain : ServiceContainer, IAcDomain
    {
        public static readonly IAcDomain SingleInstance = new EmptyAcDomain();

        private EmptyAcDomain()
        {
            this.NodeHost = new EmptyNodeHost(this);
            this.SignIn = (args) =>
            {
            };
            this.SignOut = () =>
            {
            };
        }

        public IAppSystemSet AppSystemSet
        {
            get { return Ac.MemorySets.AppSystemSet.Empty; }
        }

        public IButtonSet ButtonSet
        {
            get { return Ac.MemorySets.ButtonSet.Empty; }
        }

        public ICommandBus CommandBus
        {
            get { return EmptyCommandBus.Empty; }
        }

        public IAppConfig Config
        {
            get
            {
                return EmptyAppConfig.Empty;
            }
        }

        /// <summary>
        /// 标识生成器
        /// </summary>
        public IIdGenerator IdGenerator
        {
            get { return EmptyIdGenerator.Empty; }
        }

        /// <summary>
        /// 序列标识生成器
        /// </summary>
        public ISequenceIdGenerator SequenceIdGenerator
        {
            get { return EmptySequenceIdGenerator.Empty; }
        }

        private IObjectSerializer _objectJsonSerializer = null;

        public IObjectSerializer JsonSerializer
        {
            get { return _objectJsonSerializer ?? (_objectJsonSerializer = new ObjectJsonSerializer()); }
        }

        public IAcSession CreateSession(Guid sessionId, AccountState account)
        {
            return AcSessionState.Empty;
        }

        public IDbTableColumnSet DbTableColumns
        {
            get { return Rdb.MemorySets.DbTableColumnSet.Empty; }
        }

        public IDbTableSet DbTables
        {
            get { return Rdb.MemorySets.DbTableSet.Empty; }
        }

        public IDbViewColumnSet DbViewColumns
        {
            get { return Rdb.MemorySets.DbViewColumnSet.Empty; }
        }

        public IDbViewSet DbViews
        {
            get { return Rdb.MemorySets.DbViewSet.Empty; }
        }

        public void DeleteSession(Guid sessionId)
        {
            // 什么也不需要做
        }

        public IEntityTypeSet EntityTypeSet
        {
            get { return Ac.MemorySets.EntityTypeSet.Empty; }
        }

        public IEventBus EventBus
        {
            get { return EmptyEventBus.Empty; }
        }

        public IFunctionSet FunctionSet
        {
            get { return Ac.MemorySets.FunctionSet.Empty; }
        }

        public IGroupSet GroupSet
        {
            get { return Ac.MemorySets.GroupSet.Empty; }
        }

        public Guid Id
        {
            get { return Guid.Empty; }
        }

        public IMenuSet MenuSet
        {
            get { return Ac.MemorySets.MenuSet.Empty; }
        }

        public IMessageDispatcher MessageDispatcher
        {
            get { return EmptyMessageDispatcher.Empty; }
        }

        public string Name
        {
            get { return "EmptyAcDomain"; }
        }

        public ICatalogSet CatalogSet
        {
            get { return Ac.MemorySets.CatalogSet.Empty; }
        }

        public IUiViewSet UiViewSet
        {
            get { return Ac.MemorySets.UiViewSet.Empty; }
        }

        public IPrivilegeSet PrivilegeSet
        {
            get { return Ac.MemorySets.PrivilegeSet.Empty; }
        }

        public IRdbSet RdbSet
        {
            get { return Rdb.MemorySets.RdbSet.Empty; }
        }

        public IRoleSet RoleSet
        {
            get { return Ac.MemorySets.RoleSet.Empty; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ISsdSetSet SsdSetSet
        {
            get { return Ac.MemorySets.SsdSetSet.Empty; }
        }
        /// <summary>
        /// 
        /// </summary>
        public IDsdSetSet DsdSetSet
        {
            get { return Ac.MemorySets.DsdSetSet.Empty; }
        }
        public Action<Dictionary<string, object>> SignIn { get; set; }
        public Action SignOut { get; set; }

        public ISysUserSet SysUserSet
        {
            get { return Ac.MemorySets.SysUserSet.Empty; }
        }

        public string BuildInPluginsBaseDirectory
        {
            get { return string.Empty; }
        }

        public ILoggingService LoggingService
        {
            get { return EmptyLoggingService.Instance; }
        }

        public string GetPluginBaseDirectory(PluginType pluginType)
        {
            return string.Empty;
        }

        public INodeHost NodeHost { get; private set; }

        private class EmptyNodeHost : INodeHost
        {
            private readonly IAcDomain _acDomain;

            public EmptyNodeHost(IAcDomain acDomain)
            {
                this._acDomain = acDomain;
            }

            public IStateCodeSet StateCodes
            {
                get
                {
                    return Edi.MemorySets.StateCodeSet.Empty;
                }
            }

            public IProcesseSet Processs
            {
                get { return Edi.MemorySets.ProcesseSet.Empty; }
            }

            public INodeSet Nodes
            {
                get { return Edi.MemorySets.NodeSet.Empty; }
            }

            public IInfoDicSet InfoDics
            {
                get { return Edi.MemorySets.InfoDicSet.Empty; }
            }

            public IOntologySet Ontologies
            {
                get { return Edi.MemorySets.OntologySet.Empty; }
            }

            public IInfoStringConverterSet InfoStringConverters
            {
                get { return Edi.MemorySets.InfoStringConverterSet.Empty; }
            }

            public IInfoRuleSet InfoRules
            {
                get { return Edi.MemorySets.InfoRuleSet.Empty; }
            }

            public IMessageProviderSet MessageProviders
            {
                get { return Edi.MemorySets.MessageProviderSet.Empty; }
            }

            public IEntityProviderSet EntityProviders
            {
                get { return Edi.MemorySets.EntityProviderSet.Empty; }
            }

            public IMessageTransferSet Transfers
            {
                get { return Edi.MemorySets.MessageTransferSet.Empty; }
            }

            public IMessageProducer MessageProducer
            {
                get { return new DefaultMessageProducer(); }
            }

            public IHecpHandler HecpHandler
            {
                get
                {
                    return new HecpHandler();
                }
            }

            public List<Func<HecpContext, ProcessResult>> PreHecpRequestFilters
            {
                get { return new List<Func<HecpContext, ProcessResult>>(); }
            }

            public List<Func<MessageContext, ProcessResult>> GlobalEdiMessageHandingFilters
            {
                get { return new List<Func<MessageContext, ProcessResult>>(); }
            }

            public List<Func<MessageContext, ProcessResult>> GlobalEdiMessageHandledFilters
            {
                get { return new List<Func<MessageContext, ProcessResult>>(); }
            }

            public List<Func<HecpContext, ProcessResult>> GlobalHecpResponseFilters
            {
                get { return new List<Func<HecpContext, ProcessResult>>(); }
            }

            public ProcessResult ApplyPreHecpRequestFilters(HecpContext context)
            {
                return ProcessResult.Ok;
            }

            public ProcessResult ApplyEdiMessageHandingFilters(MessageContext context)
            {
                return ProcessResult.Ok;
            }

            public ProcessResult ApplyEdiMessageHandledFilters(MessageContext context)
            {
                return ProcessResult.Ok;
            }

            public ProcessResult ApplyHecpResponseFilters(HecpContext context)
            {
                return ProcessResult.Ok;
            }
        }

        private class EmptyAppConfig : IAppConfig
        {
            public static readonly IAppConfig Empty = new EmptyAppConfig();

            public string CurrentAcSessionCacheKey
            {
                get { return string.Empty; }
            }

            public bool EnableClientCache
            {
                get { return false; }
            }

            public bool EnableOperationLog
            {
                get { return false; }
            }

            public string SelfAppSystemCode
            {
                get { return string.Empty; }
            }

            public string SqlServerTableColumnsSelect
            {
                get { return string.Empty; }
            }

            public string SqlServerTablesSelect
            {
                get { return string.Empty; }
            }

            public string SqlServerViewColumnsSelect
            {
                get { return string.Empty; }
            }

            public string SqlServerViewsSelect
            {
                get { return string.Empty; }
            }

            public int TicksTimeout
            {
                get { return 0; }
            }

            public string SequenceIdGenerator
            {
                get { return null; }
            }

            public string IdGenerator
            {
                get { return null; }
            }


            public string InfoFormat
            {
                get { return string.Empty; }
            }

            public string EntityArchivePath
            {
                get { return string.Empty; }
            }

            public string EntityBackupPath
            {
                get { return string.Empty; }
            }

            public bool ServiceIsAlive
            {
                get { return false; }
            }

            public bool TraceIsEnabled
            {
                get { return false; }
            }

            public int BeatPeriod
            {
                get { return int.MaxValue; }
            }

            public string CenterNodeId
            {
                get { return string.Empty; }
            }

            public string ThisNodeId
            {
                get { return string.Empty; }
            }

            public ConfigLevel AuditLevel
            {
                get { return ConfigLevel.Invalid; }
            }

            public AuditType ImplicitAudit
            {
                get { return AuditType.Invalid; }
            }

            public ConfigLevel AclLevel
            {
                get { return ConfigLevel.Invalid; }
            }

            public AllowType ImplicitAllow
            {
                get { return AllowType.Invalid; }
            }

            public ConfigLevel EntityLogonLevel
            {
                get { return ConfigLevel.Invalid; }
            }

            public EntityLogon ImplicitEntityLogon
            {
                get { return EntityLogon.Invalid; }
            }
        }

        private class EmptyLoggingService : ILoggingService
        {
            public static readonly ILoggingService Instance = new EmptyLoggingService();

            public void Log(IAnyLog anyLog)
            {
                // 什么也不需要做
            }

            public void Log(IAnyLog[] anyLogs)
            {
                // 什么也不需要做
            }

            public IAnyLog Get(Guid id)
            {
                return null;
            }

            public IList<IAnyLog> GetPlistAnyLogs(List<Query.FilterData> filters, Query.PagingInput paging)
            {
                return new List<IAnyLog>();
            }

            public IList<OperationLog> GetPlistOperationLogs(Guid? targetId, DateTime? leftCreateOn, DateTime? rightCreateOn, List<Query.FilterData> filters, Query.PagingInput paging)
            {
                return new List<OperationLog>();
            }

            public IList<ExceptionLog> GetPlistExceptionLogs(List<Query.FilterData> filters, Query.PagingInput paging)
            {
                return new List<ExceptionLog>();
            }

            public void ClearAnyLog()
            {
                // 什么也不需要做
            }

            public void ClearExceptionLog()
            {
                // 什么也不需要做
            }

            public void Debug(object message)
            {
                // 什么也不需要做
            }

            public void DebugFormatted(string format, params object[] args)
            {
                // 什么也不需要做
            }

            public void Info(object message)
            {
                // 什么也不需要做
            }

            public void InfoFormatted(string format, params object[] args)
            {
                // 什么也不需要做
            }

            public void Warn(object message)
            {
                // 什么也不需要做
            }

            public void Warn(object message, Exception exception)
            {
                // 什么也不需要做
            }

            public void WarnFormatted(string format, params object[] args)
            {
                // 什么也不需要做
            }

            public void Error(object message)
            {
                // 什么也不需要做
            }

            public void Error(object message, Exception exception)
            {
                // 什么也不需要做
            }

            public void ErrorFormatted(string format, params object[] args)
            {
                // 什么也不需要做
            }

            public void Fatal(object message)
            {
                // 什么也不需要做
            }

            public void Fatal(object message, Exception exception)
            {
                // 什么也不需要做
            }

            public void FatalFormatted(string format, params object[] args)
            {
                // 什么也不需要做
            }

            public bool IsDebugEnabled
            {
                get { return false; }
            }

            public bool IsInfoEnabled
            {
                get { return false; }
            }

            public bool IsWarnEnabled
            {
                get { return false; }
            }

            public bool IsErrorEnabled
            {
                get { return false; }
            }

            public bool IsFatalEnabled
            {
                get { return false; }
            }
        }

        private class EmptyCommandBus : ICommandBus
        {
            public static readonly ICommandBus Empty = new EmptyCommandBus();

            public void Publish<TMessage>(TMessage message)
            {
                // 什么也不需要做
            }

            public void Publish<TMessage>(IEnumerable<TMessage> messages)
            {
                // 什么也不需要做
            }

            public void Clear()
            {
                // 什么也不需要做
            }

            public bool DistributedTransactionSupported
            {
                get { return false; }
            }

            public bool Committed
            {
                get { return true; }
            }

            public void Commit()
            {
                // 什么也不需要做
            }

            public void Rollback()
            {
                // 什么也不需要做
            }

            public void Dispose()
            {
                // 什么也不需要做
            }
        }

        private class EmptyEventBus : IEventBus
        {
            public static readonly IEventBus Empty = new EmptyEventBus();

            public void Publish<TMessage>(TMessage message)
            {
                // 什么也不需要做
            }

            public void Publish<TMessage>(IEnumerable<TMessage> messages)
            {
                // 什么也不需要做
            }

            public void Clear()
            {
                // 什么也不需要做
            }

            public bool DistributedTransactionSupported
            {
                get { return false; }
            }

            public bool Committed
            {
                get { return true; }
            }

            public void Commit()
            {
                // 什么也不需要做
            }

            public void Rollback()
            {
                // 什么也不需要做
            }

            public void Dispose()
            {
                // 什么也不需要做
            }
        }

        private class EmptyMessageDispatcher : IMessageDispatcher
        {
            public static readonly IMessageDispatcher Empty = new EmptyMessageDispatcher();

            public void Clear()
            {
                // 什么也不需要做
            }

            public void DispatchMessage<T>(T message)
            {
                // 什么也不需要做
            }

            public void Register<T>(IHandler<T> handler)
            {
                // 什么也不需要做
            }

            public void UnRegister<T>(IHandler<T> handler)
            {
                // 什么也不需要做
            }

            public event EventHandler<MessageDispatchEventArgs> Dispatching;

            public event EventHandler<MessageDispatchEventArgs> DispatchFailed;

            public event EventHandler<MessageDispatchEventArgs> Dispatched;
        }

        private class EmptyIdGenerator : IIdGenerator
        {
            public static readonly EmptyIdGenerator Empty = new EmptyIdGenerator();

            public object Generate()
            {
                return Guid.Empty;
            }
        }

        private class EmptySequenceIdGenerator : ISequenceIdGenerator
        {
            public static readonly EmptySequenceIdGenerator Empty = new EmptySequenceIdGenerator();

            public object Next
            {
                get { return Guid.Empty; }
            }
        }
    }
}
