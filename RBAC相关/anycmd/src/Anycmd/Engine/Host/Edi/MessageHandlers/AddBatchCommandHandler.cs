﻿
namespace Anycmd.Engine.Host.Edi.MessageHandlers
{
    using Commands;
    using DataContracts;
    using Engine.Edi;
    using Engine.Edi.Abstractions;
    using Engine.Edi.Messages;
    using Entities;
    using Exceptions;
    using Handlers;
    using Hecp;
    using Info;
    using Query;
    using Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Util;

    public class AddBatchCommandHandler : CommandHandler<AddBatchCommand>
    {
        private readonly IAcDomain _acDomain;

        public AddBatchCommandHandler(IAcDomain acDomain)
        {
            this._acDomain = acDomain;
        }

        public override void Handle(AddBatchCommand command)
        {
            var batchRepository = _acDomain.RetrieveRequiredService<IRepository<Batch, Guid>>();
            OntologyDescriptor ontology;
            if (!_acDomain.NodeHost.Ontologies.TryGetOntology(command.Input.OntologyId, out ontology))
            {
                throw new ValidationException("非法的本体标识" + command.Input.OntologyId);
            }
            BatchType type;
            if (!command.Input.Type.TryParse(out type))
            {
                throw new ValidationException("意外的批类型" + command.Input.Type);
            }
            
            var entity = Batch.Create(command.Input);

            var descriptor = new BatchDescriptor(_acDomain, entity);
            const int pageSize = 1000;
            const int pageIndex = 0;
            bool includeDescendants = entity.IncludeDescendants.HasValue && entity.IncludeDescendants.Value;
            NodeDescriptor toNode = null;
            if (!_acDomain.NodeHost.Nodes.TryGetNodeById(entity.NodeId.ToString(), out toNode))
            {
                throw new GeneralException("意外的节点标识" + entity.NodeId);
            }

            string thisNodeId = _acDomain.NodeHost.Nodes.ThisNode.Node.Id.ToString();
            Verb actionCode;
            switch (descriptor.Type)
            {
                case BatchType.BuildCreateCommand:
                    actionCode = Verb.Create;
                    break;
                case BatchType.BuildUpdateCommand:
                    actionCode = Verb.Update;
                    break;
                case BatchType.BuildDeleteCommand:
                    actionCode = Verb.Delete;
                    break;
                default:
                    throw new GeneralException("意外的批类型" + entity.Type);
            }
            var commandFactory = _acDomain.NodeHost.MessageProducer;
            bool goOn = true;
            int count = 0;
            var pagingData = new PagingInput(pageIndex, pageSize, ontology.IncrementIdElement.Element.Code, "asc");
            var selectElements = new OrderedElementSet {ontology.IdElement};
            foreach (var item in ontology.Elements.Values.Where(a => a.Element.IsEnabled == 1))
            {
                if (toNode.IsCareforElement(item) || toNode.IsInfoIdElement(item))
                {
                    selectElements.Add(item);
                }
            }
            var filters = new List<FilterData>();
            if (ontology.Ontology.IsCataloguedEntity && !string.IsNullOrEmpty(entity.CatalogCode))
            {
                filters.Add(includeDescendants
                    ? FilterData.Like("ZZJGM", entity.CatalogCode + "%")
                    : FilterData.EQ("ZZJGM", entity.CatalogCode));
            }
            do
            {
                IDataTuples entities = ontology.EntityProvider.GetPlist(ontology, selectElements, filters, pagingData);
                if (entities != null && entities.Columns != null)
                {
                    int idIndex = -1;
                    for (int i = 0; i < entities.Columns.Count; i++)
                    {
                        if (entities.Columns[i] == ontology.IdElement)
                        {
                            idIndex = i;
                            break;
                        }
                    }
                    if (entities.Columns.Count == 0)
                    {
                        throw new GeneralException("意外的查询列数");
                    }
                    if (idIndex == -1)
                    {
                        throw new GeneralException("未查询得到实体标识列");
                    }
                    var products = new List<MessageEntity>();
                    foreach (var item in entities.Tuples)
                    {
                        var idItems = new InfoItem[] { InfoItem.Create(ontology.IdElement, item[idIndex].ToString()) };
                        var valueItems = new InfoItem[entities.Columns.Count - 1];
                        for (int i = 0; i < entities.Columns.Count; i++)
                        {
                            if (i < idIndex)
                            {
                                valueItems[i] = InfoItem.Create(entities.Columns[i], item[i].ToString());
                            }
                            else if (i > idIndex)
                            {
                                valueItems[i - 1] = InfoItem.Create(entities.Columns[i], item[i].ToString());
                            }
                        }
                        var commandContext = new MessageContext(_acDomain,
                                new MessageRecord(
                                    MessageTypeKind.Received,
                                    Guid.NewGuid(),
                                    DataItemsTuple.Create(_acDomain, idItems, valueItems, null, "json"))
                                    {
                                        Verb = actionCode,
                                        ClientId = thisNodeId,
                                        ClientType = ClientType.Node,
                                        TimeStamp = DateTime.Now,
                                        CreateOn = DateTime.Now,
                                        Description = entity.Type,
                                        LocalEntityId = item[idIndex].ToString(),
                                        Ontology = ontology.Ontology.Code,
                                        CatalogCode = null,// 如果按照目录分片分发命令的话则目录为空的分发命令是由专门的分发器分发的
                                        MessageId = Guid.NewGuid().ToString(),
                                        MessageType = MessageType.Action,
                                        ReasonPhrase = "Ok",
                                        Status = 200,
                                        EventSourceType = string.Empty,
                                        EventSubjectCode = string.Empty,
                                        UserName = command.AcSession.Account.Id.ToString(),
                                        IsDumb = false,
                                        ReceivedOn = DateTime.Now,
                                        Version = ApiVersion.V1.ToName()
                                    });
                        products.AddRange(commandFactory.Produce(new MessageTuple(commandContext, valueItems), toNode));
                    }
                    ontology.MessageProvider.SaveCommands(ontology, products.ToArray());
                    count = count + products.Count;
                    pagingData.PageIndex++;// 注意这里不要引入bug。
                }
                if (entities == null || entities.Tuples.Length == 0)
                {
                    goOn = false;
                }
            } while (goOn);
            entity.Total = count;

            batchRepository.Add(entity);
            batchRepository.Context.Commit();

            _acDomain.PublishEvent(new BatchAddedEvent(command.AcSession, entity));
            _acDomain.CommitEventBus();
        }

        #region MessageRecord
        public class MessageRecord : MessageBase
        {
            public MessageRecord(MessageTypeKind type, Guid id, DataItemsTuple dataTuple)
                : base(type, id, dataTuple)
            {

            }
            #region Public Properties
            /// <summary>
            /// 协议版本号
            /// </summary>
            public new string Version
            {
                get { return base.Version; }
                protected internal set
                {
                    base.Version = value;
                }
            }

            /// <summary>
            /// 命令类型
            /// </summary>
            public new MessageTypeKind CommandType
            {
                get { return base.CommandType; }
            }

            /// <summary>
            /// 命令标识
            /// </summary>
            public new Guid Id
            {
                get { return base.Id; }
                set
                {
                    base.Id = value;
                }
            }

            /// <summary>
            /// 是否是哑的
            /// </summary>
            public new bool IsDumb
            {
                get { return base.IsDumb; }
                protected internal set
                {
                    base.IsDumb = value;
                }
            }

            /// <summary>
            /// 请求类型
            /// </summary>
            public new MessageType MessageType
            {
                get { return base.MessageType; }
                protected internal set
                {
                    base.MessageType = value;
                }
            }

            /// <summary>
            /// 远端命令标识
            /// </summary>
            public new string MessageId
            {
                get { return base.MessageId; }
                protected internal set
                {
                    base.MessageId = value;
                }
            }

            public new string RelatesTo
            {
                get { return base.RelatesTo; }
                protected internal set
                {
                    base.RelatesTo = value;
                }
            }

            public new string To
            {
                get { return base.To; }
                protected internal set
                {
                    base.To = value;
                }
            }

            public new string SessionId
            {
                get { return base.SessionId; }
                protected internal set
                {
                    base.SessionId = value;
                }
            }

            public new FromData From
            {
                get { return base.From; }
                protected internal set
                {
                    base.From = value;
                }
            }

            /// <summary>
            /// 本体实体标识
            /// </summary>
            public new string LocalEntityId
            {
                get { return base.LocalEntityId; }
                protected internal set
                {
                    base.LocalEntityId = value;
                }
            }

            /// <summary>
            /// 本地目录码
            /// </summary>
            public new string CatalogCode
            {
                get { return base.CatalogCode; }
                protected internal set
                {
                    base.CatalogCode = value;
                }
            }

            /// <summary>
            /// 客户端类型
            /// </summary>
            public new ClientType ClientType
            {
                get { return base.ClientType; }
                protected internal set
                {
                    base.ClientType = value;
                }
            }

            /// <summary>
            /// 本体码
            /// </summary>
            public new string Ontology
            {
                get { return base.Ontology; }
                protected internal set
                {
                    base.Ontology = value;
                }
            }

            /// <summary>
            /// 动作类型
            /// </summary>
            public new Verb Verb
            {
                get { return base.Verb; }
                protected internal set
                {
                    base.Verb = value;
                }
            }

            /// <summary>
            /// 节点标识
            /// </summary>
            public new string ClientId
            {
                get { return base.ClientId; }
                protected internal set
                {
                    base.ClientId = value;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            public new DateTime ReceivedOn
            {
                get { return base.ReceivedOn; }
                protected internal set
                {
                    base.ReceivedOn = value;
                }
            }

            /// <summary>
            /// 命令时间戳
            /// </summary>
            public new DateTime TimeStamp
            {
                get { return base.TimeStamp; }
                protected internal set
                {
                    base.TimeStamp = value;
                }
            }

            /// <summary>
            /// 发送时间
            /// </summary>
            public new DateTime CreateOn
            {
                get { return base.CreateOn; }
                protected internal set
                {
                    base.CreateOn = value;
                }
            }

            /// <summary>
            /// 命令状态码
            /// </summary>
            public new int Status
            {
                get { return base.Status; }
                protected internal set
                {
                    base.Status = value;
                }
            }

            /// <summary>
            /// 原因短语
            /// </summary>
            public new string ReasonPhrase
            {
                get { return base.ReasonPhrase; }
                protected internal set
                {
                    base.ReasonPhrase = value;
                }
            }

            /// <summary>
            /// 命令状态描述
            /// </summary>
            public new string Description
            {
                get { return base.Description; }
                protected internal set
                {
                    base.Description = value;
                }
            }

            /// <summary>
            /// 事件主题
            /// </summary>
            public new string EventSubjectCode
            {
                get { return base.EventSubjectCode; }
                protected internal set
                {
                    base.EventSubjectCode = value;
                }
            }

            /// <summary>
            /// 其值为枚举字符串
            /// </summary>
            public new string EventSourceType
            {
                get { return base.EventSourceType; }
                protected internal set
                {
                    base.EventSourceType = value;
                }
            }

            /// <summary>
            /// 发起人
            /// </summary>
            public new string UserName
            {
                get { return base.UserName; }
                protected internal set
                {
                    base.UserName = value;
                }
            }

            /// <summary>
            /// 数据项集合对
            /// </summary>
            public new DataItemsTuple DataTuple
            {
                get { return base.DataTuple; }
            }
            #endregion
        }
        #endregion
    }
}
