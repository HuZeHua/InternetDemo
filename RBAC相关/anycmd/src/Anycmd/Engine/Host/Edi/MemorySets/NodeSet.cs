﻿
namespace Anycmd.Engine.Host.Edi.MemorySets
{
    using Bus;
    using Engine.Ac;
    using Engine.Edi;
    using Engine.Edi.Abstractions;
    using Engine.Edi.InOuts;
    using Engine.Edi.Messages;
    using Entities;
    using Exceptions;
    using Hecp;
    using Repositories;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Util;
    using elementId = System.Guid;
    using isCare = System.Boolean;
    using ontologyId = System.Guid;

    /// <summary>
    /// 节点上下文访问接口默认实现
    /// </summary>
    internal sealed class NodeSet : INodeSet, IMemorySet
    {
        public static readonly INodeSet Empty = new NodeSet(EmptyAcDomain.SingleInstance);
        private static readonly object Locker = new object();

        private readonly Dictionary<string, NodeDescriptor>
            _allNodesById = new Dictionary<string, NodeDescriptor>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, NodeDescriptor>
            _allNodesByPublicKey = new Dictionary<string, NodeDescriptor>(StringComparer.OrdinalIgnoreCase);
        private NodeDescriptor _selfNode = null;
        private NodeDescriptor _centerNode = null;
        private bool _initialized = false;

        private readonly Guid _id = Guid.NewGuid();
        private readonly NodeCareSet _nodeCareSet;
        private readonly NodeElementActionSet _actionSet;
        private readonly CatalogSet _catalogSet;

        private readonly IAcDomain _acDomain;

        public Guid Id
        {
            get { return _id; }
        }

        /// <summary>
        /// 构造并接入总线
        /// </summary>
        internal NodeSet(IAcDomain acDomain)
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
            this._nodeCareSet = new NodeCareSet(acDomain);
            this._actionSet = new NodeElementActionSet(acDomain);
            this._catalogSet = new CatalogSet(acDomain);
            new MessageHandler(this).Register();
        }

        public NodeDescriptor CenterNode
        {
            get
            {
                if (!_initialized)
                {
                    Init();
                }
                if (_centerNode == null)
                {
                    throw new GeneralException("尚没有设定中心节点，请先设定中心节点");
                }

                return _centerNode;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public NodeDescriptor ThisNode
        {
            get
            {
                if (!_initialized)
                {
                    Init();
                }
                if (_selfNode == null)
                {
                    throw new GeneralException("尚没有设定这个节点，请先设定这个节点");
                }

                return _selfNode;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeId"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool TryGetNodeById(string nodeId, out NodeDescriptor node)
        {
            if (nodeId == null)
            {
                throw new ArgumentNullException("nodeId");
            }
            if (!_initialized)
            {
                Init();
            }
            return _allNodesById.TryGetValue(nodeId, out node);
        }

        public bool TryGetNodeByPublicKey(string publicKey, out NodeDescriptor node)
        {
            if (publicKey == null)
            {
                throw new ArgumentNullException("publicKey");
            }
            if (!_initialized)
            {
                Init();
            }
            return _allNodesByPublicKey.TryGetValue(publicKey, out node);
        }

        #region GetNodeElementActions
        public IReadOnlyDictionary<Verb, NodeElementActionState> GetNodeElementActions(NodeDescriptor node, ElementDescriptor element)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            if (!_initialized)
            {
                Init();
            }
            return _actionSet[node, element];
        }
        #endregion

        public IEnumerable<ElementDescriptor> GetInfoIdElements(NodeDescriptor node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }
            if (!_initialized)
            {
                Init();
            }
            return _nodeCareSet.GetInfoIdElements(node);
        }

        public bool IsInfoIdElement(NodeDescriptor node, ElementDescriptor element)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            if (!_initialized)
            {
                Init();
            }
            return _nodeCareSet.IsInfoIdElement(node, element);
        }

        public IReadOnlyCollection<NodeElementCareState> GetNodeElementCares(NodeDescriptor node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }
            if (!_initialized)
            {
                Init();
            }
            return _nodeCareSet.GetNodeElementCares(node);
        }

        public IReadOnlyCollection<NodeOntologyCareState> GetNodeOntologyCares(NodeDescriptor node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }
            if (!_initialized)
            {
                Init();
            }
            return _nodeCareSet.GetNodeOntologyCares(node);
        }

        public IEnumerable<NodeOntologyCareState> GetNodeOntologyCares()
        {
            if (!_initialized)
            {
                Init();
            }
            return _nodeCareSet.GetNodeOntologyCares();
        }

        public bool IsCareforElement(NodeDescriptor node, ElementDescriptor element)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            if (!_initialized)
            {
                Init();
            }
            return _nodeCareSet.IsCareforElement(node, element);
        }

        public bool IsCareForOntology(NodeDescriptor node, OntologyDescriptor ontology)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }
            if (ontology == null)
            {
                throw new ArgumentNullException("ontology");
            }
            if (!_initialized)
            {
                Init();
            }
            return _nodeCareSet.IsCareForOntology(node, ontology);
        }

        public IReadOnlyDictionary<CatalogState, NodeOntologyCatalogState> GetNodeOntologyCatalogs(NodeDescriptor node, OntologyDescriptor ontology)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }
            if (ontology == null)
            {
                throw new ArgumentNullException("ontology");
            }
            if (!_initialized)
            {
                Init();
            }
            return _catalogSet[node, ontology];
        }

        public IEnumerable<NodeOntologyCatalogState> GetNodeOntologyCatalogs()
        {
            if (!_initialized)
            {
                Init();
            }
            return _catalogSet.GetNodeOntologyCatalogs();
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
        public IEnumerator<NodeDescriptor> GetEnumerator()
        {
            if (!_initialized)
            {
                Init();
            }
            return _allNodesById.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            if (!_initialized)
            {
                Init();
            }
            return _allNodesById.Values.GetEnumerator();
        }

        /// <summary>
        /// 初始化节点上下文
        /// </summary>
        private void Init()
        {
            if (_initialized) return;
            lock (Locker)
            {
                if (_initialized) return;
                _acDomain.MessageDispatcher.DispatchMessage(new MemorySetInitingEvent(this));
                _allNodesById.Clear();
                _allNodesByPublicKey.Clear();
                var allNodes = _acDomain.RetrieveRequiredService<INodeHostBootstrap>().GetNodes();
                foreach (var node in allNodes)
                {
                    var nodeState = NodeState.Create(_acDomain, node);
                    var descriptor = new NodeDescriptor(_acDomain, nodeState);
                    _allNodesById.Add(node.Id.ToString(), descriptor);
                    if (_allNodesByPublicKey.ContainsKey(node.PublicKey))
                    {
                        throw new GeneralException("重复的公钥" + node.PublicKey);
                    }
                    _allNodesByPublicKey.Add(node.PublicKey, descriptor);
                    if (node.Id.ToString().Equals(_acDomain.Config.ThisNodeId, StringComparison.OrdinalIgnoreCase))
                    {
                        _selfNode = descriptor;
                    }
                    if (node.Id.ToString().Equals(_acDomain.Config.CenterNodeId, StringComparison.OrdinalIgnoreCase))
                    {
                        _centerNode = descriptor;
                    }
                }
                _initialized = true;
                _acDomain.MessageDispatcher.DispatchMessage(new MemorySetInitializedEvent(this));
            }
        }

        #region MessageHandler
        private class MessageHandler :
            IHandler<AddNodeCommand>,
            IHandler<NodeAddedEvent>,
            IHandler<UpdateNodeCommand>,
            IHandler<NodeUpdatedEvent>,
            IHandler<RemoveNodeCommand>,
            IHandler<NodeRemovedEvent>
        {
            private readonly NodeSet _set;

            internal MessageHandler(NodeSet set)
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
                messageDispatcher.Register((IHandler<AddNodeCommand>)this);
                messageDispatcher.Register((IHandler<NodeAddedEvent>)this);
                messageDispatcher.Register((IHandler<UpdateNodeCommand>)this);
                messageDispatcher.Register((IHandler<NodeUpdatedEvent>)this);
                messageDispatcher.Register((IHandler<RemoveNodeCommand>)this);
                messageDispatcher.Register((IHandler<NodeRemovedEvent>)this);
            }

            public void Handle(AddNodeCommand message)
            {
                this.Handle(message.AcSession, message.Input, true);
            }

            public void Handle(NodeAddedEvent message)
            {
                if (message.IsPrivate)
                {
                    return;
                }
                this.Handle(message.AcSession, message.Output, false);
            }

            private void Handle(IAcSession acSession, INodeCreateIo input, bool isCommand)
            {
                var acDomain = _set._acDomain;
                var allNodesById = _set._allNodesById;
                var allNodesByPublicKey = _set._allNodesByPublicKey;
                var nodeRepository = acDomain.RetrieveRequiredService<IRepository<Node, Guid>>();
                if (string.IsNullOrEmpty(input.Code))
                {
                    throw new ValidationException("编码不能为空");
                }
                if (input.Id.HasValue)
                {
                    throw new ValidationException("标识是必须的");
                }
                Node entity;
                lock (Locker)
                {
                    NodeDescriptor node;
                    Debug.Assert(input.Id != null, "input.Id != null");
                    if (acDomain.NodeHost.Nodes.TryGetNodeById(input.Id.Value.ToString(), out node))
                    {
                        throw new ValidationException("已经存在");
                    }
                    if (acDomain.NodeHost.Nodes.Any(a => a.Node.Code.Equals(input.Code)))
                    {
                        throw new ValidationException("重复的编码");
                    }

                    entity = Node.Create(input);

                    var state = new NodeDescriptor(acDomain, NodeState.Create(acDomain, entity));
                    allNodesById.Add(entity.Id.ToString(), state);
                    allNodesByPublicKey.Add(entity.PublicKey, state);
                    if (isCommand)
                    {
                        try
                        {
                            nodeRepository.Add(entity);
                            nodeRepository.Context.Commit();
                        }
                        catch
                        {
                            allNodesById.Remove(entity.Id.ToString());
                            allNodesByPublicKey.Remove(entity.PublicKey);
                            nodeRepository.Context.Rollback();
                            throw;
                        }
                    }
                }
                if (isCommand)
                {
                    acDomain.MessageDispatcher.DispatchMessage(new NodeAddedEvent(acSession, entity, input, isPrivate: true));
                }
            }

            public void Handle(UpdateNodeCommand message)
            {
                this.Handle(message.AcSession, message.Input, true);
            }

            public void Handle(NodeUpdatedEvent message)
            {
                if (message.IsPrivate)
                {
                    return;
                }
                this.Handle(message.AcSession, message.Output, false);
            }

            private void Handle(IAcSession acSession, INodeUpdateIo input, bool isCommand)
            {
                var acDomain = _set._acDomain;
                var nodeRepository = acDomain.RetrieveRequiredService<IRepository<Node, Guid>>();
                if (string.IsNullOrEmpty(input.Code))
                {
                    throw new ValidationException("编码不能为空");
                }
                if (acDomain.NodeHost.Nodes.Any(a => a.Node.Code.Equals(input.Code) && a.Node.Id != input.Id))
                {
                    throw new ValidationException("重复的编码");
                }
                Node entity;
                bool stateChanged = false;
                lock (Locker)
                {
                    NodeDescriptor node;
                    if (!acDomain.NodeHost.Nodes.TryGetNodeById(input.Id.ToString(), out node))
                    {
                        throw new NotExistException();
                    }
                    entity = nodeRepository.GetByKey(input.Id);
                    if (entity == null)
                    {
                        throw new NotExistException();
                    }
                    var bkState = new NodeDescriptor(acDomain, NodeState.Create(acDomain, entity));

                    entity.Update(input);

                    var newState = new NodeDescriptor(acDomain, NodeState.Create(acDomain, entity));
                    stateChanged = newState != bkState;
                    if (stateChanged)
                    {
                        Update(newState);
                    }
                    if (isCommand)
                    {
                        try
                        {
                            nodeRepository.Update(entity);
                            nodeRepository.Context.Commit();
                        }
                        catch
                        {
                            if (stateChanged)
                            {
                                Update(bkState);
                            }
                            nodeRepository.Context.Rollback();
                            throw;
                        }
                    }
                }
                if (isCommand && stateChanged)
                {
                    acDomain.MessageDispatcher.DispatchMessage(new NodeUpdatedEvent(acSession, entity, input, isPrivate: true));
                }
            }

            private void Update(NodeDescriptor state)
            {
                var allNodesById = _set._allNodesById;
                var allNodesByPublicKey = _set._allNodesByPublicKey;
                var oldState = allNodesById[state.Node.Id.ToString()];
                allNodesById[state.Node.Id.ToString()] = state;
                if (!allNodesByPublicKey.ContainsKey(state.Node.PublicKey))
                {
                    allNodesByPublicKey.Add(state.Node.PublicKey, state);
                    allNodesByPublicKey.Remove(oldState.Node.PublicKey);
                }
                else
                {
                    allNodesByPublicKey[state.Node.PublicKey] = state;
                }
            }

            public void Handle(RemoveNodeCommand message)
            {
                this.Handle(message.AcSession, message.EntityId, true);
            }

            public void Handle(NodeRemovedEvent message)
            {
                if (message.IsPrivate)
                {
                    return;
                }
                this.Handle(message.AcSession, message.Source.Id, false);
            }

            private void Handle(IAcSession acSession, Guid nodeId, bool isCommand)
            {
                var acDomain = _set._acDomain;
                var allNodesById = _set._allNodesById;
                var allNodesByPublicKey = _set._allNodesByPublicKey;
                var nodeRepository = acDomain.RetrieveRequiredService<IRepository<Node, Guid>>();
                NodeDescriptor bkState;
                if (!acDomain.NodeHost.Nodes.TryGetNodeById(nodeId.ToString(), out bkState))
                {
                    return;
                }
                Node entity;
                lock (Locker)
                {
                    entity = nodeRepository.GetByKey(nodeId);
                    if (entity == null)
                    {
                        return;
                    }
                    allNodesById.Remove(entity.Id.ToString());
                    allNodesByPublicKey.Remove(entity.PublicKey);
                    if (isCommand)
                    {
                        try
                        {
                            nodeRepository.Remove(entity);
                            nodeRepository.Context.Commit();
                        }
                        catch
                        {
                            allNodesById.Add(entity.Id.ToString(), bkState);
                            allNodesByPublicKey.Add(entity.PublicKey, bkState);
                            nodeRepository.Context.Rollback();
                            throw;
                        }
                    }
                }
                if (isCommand)
                {
                    acDomain.MessageDispatcher.DispatchMessage(new NodeRemovedEvent(acSession, entity, isPrivate: true));
                }
            }
        }
        #endregion

        // 内部类
        #region NodeElementActionSet
        private sealed class NodeElementActionSet
        {
            private static readonly object NodeElementActionSetLocker = new object();
            private readonly Dictionary<NodeDescriptor, Dictionary<ElementDescriptor, Dictionary<Verb, NodeElementActionState>>> _nodeElementActionDic = new Dictionary<NodeDescriptor, Dictionary<ElementDescriptor, Dictionary<Verb, NodeElementActionState>>>();
            private bool _initialized = false;

            private readonly Guid _id = Guid.NewGuid();
            private readonly IAcDomain _acDomain;

            public Guid Id
            {
                get { return _id; }
            }

            internal NodeElementActionSet(IAcDomain acDomain)
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
                new NodeElementActionMessageHandler(this).Register();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="node"></param>
            /// <param name="element"></param>
            /// <returns></returns>
            public Dictionary<Verb, NodeElementActionState> this[NodeDescriptor node, ElementDescriptor element]
            {
                get
                {
                    if (!_initialized)
                    {
                        Init();
                    }
                    if (!_nodeElementActionDic.ContainsKey(node))
                    {
                        return new Dictionary<Verb, NodeElementActionState>();
                    }
                    if (!_nodeElementActionDic[node].ContainsKey(element))
                    {
                        return new Dictionary<Verb, NodeElementActionState>();
                    }

                    return _nodeElementActionDic[node][element];
                }
            }

            public void Refresh()
            {
                if (_initialized)
                {
                    _initialized = false;
                }
            }

            #region Init
            /// <summary>
            /// 初始化信息分组上下文
            /// </summary>
            private void Init()
            {
                if (_initialized) return;
                lock (NodeElementActionSetLocker)
                {
                    if (_initialized) return;
                    _nodeElementActionDic.Clear();
                    var nodeElementActions = _acDomain.RetrieveRequiredService<INodeHostBootstrap>().GetNodeElementActions();
                    foreach (var item in nodeElementActions)
                    {
                        NodeDescriptor node;
                        _acDomain.NodeHost.Nodes.TryGetNodeById(item.NodeId.ToString(), out node);
                        ElementDescriptor element = _acDomain.NodeHost.Ontologies.GetElement(item.ElementId);
                        if (!_nodeElementActionDic.ContainsKey(node))
                        {
                            _nodeElementActionDic.Add(node, new Dictionary<ElementDescriptor, Dictionary<Verb, NodeElementActionState>>());
                        }
                        if (!_nodeElementActionDic[node].ContainsKey(element))
                        {
                            _nodeElementActionDic[node].Add(element, new Dictionary<Verb, NodeElementActionState>());
                        }
                        var state = NodeElementActionState.Create(item);
                        var action = element.Ontology.Actions.Values.First(a => a.Id == item.ActionId);
                        _nodeElementActionDic[node][element].Add(action.ActionVerb, state);
                    }
                    _initialized = true;
                }
            }

            #endregion

            #region NodeElementActionMessageHandler
            private class NodeElementActionMessageHandler :
                IHandler<AddNodeElementActionCommand>,
                IHandler<NodeElementActionAddedEvent>,
                IHandler<RemoveNodeElementActionCommand>,
                IHandler<NodeElementActionRemovedEvent>
            {
                private readonly NodeElementActionSet _set;

                internal NodeElementActionMessageHandler(NodeElementActionSet set)
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
                    messageDispatcher.Register((IHandler<AddNodeElementActionCommand>)this);
                    messageDispatcher.Register((IHandler<NodeElementActionAddedEvent>)this);
                    messageDispatcher.Register((IHandler<RemoveNodeElementActionCommand>)this);
                    messageDispatcher.Register((IHandler<NodeElementActionRemovedEvent>)this);
                }

                public void Handle(AddNodeElementActionCommand message)
                {
                    this.Handle(message.AcSession, message.Input, true);
                }

                public void Handle(NodeElementActionAddedEvent message)
                {
                    if (message.IsPrivate)
                    {
                        return;
                    }
                    this.Handle(message.AcSession, message.Output, false);
                }

                private void Handle(IAcSession acSession, INodeElementActionCreateIo input, bool isCommand)
                {
                    var acDomain = _set._acDomain;
                    var nodeElementActionDic = _set._nodeElementActionDic;
                    var repository = acDomain.RetrieveRequiredService<IRepository<NodeElementAction, Guid>>();
                    NodeElementAction entity;
                    lock (NodeElementActionSetLocker)
                    {
                        NodeDescriptor node;
                        if (!acDomain.NodeHost.Nodes.TryGetNodeById(input.NodeId.ToString(), out node))
                        {
                            throw new ValidationException("意外的节点标识" + input.NodeId);
                        }
                        ElementDescriptor element;
                        if (!acDomain.NodeHost.Ontologies.TryGetElement(input.ElementId, out element))
                        {
                            throw new ValidationException("意外的本体元素标识" + input.ElementId);
                        }
                        if (!nodeElementActionDic.ContainsKey(node))
                        {
                            nodeElementActionDic.Add(node, new Dictionary<ElementDescriptor, Dictionary<Verb, NodeElementActionState>>());
                        }
                        if (!nodeElementActionDic[node].ContainsKey(element))
                        {
                            nodeElementActionDic[node].Add(element, new Dictionary<Verb, NodeElementActionState>());
                        }
                        Debug.Assert(input.Id != null, "input.Id != null");
                        entity = new NodeElementAction(input.Id.Value)
                        {
                            ActionId = input.ActionId,
                            ElementId = input.ElementId,
                            IsAllowed = input.IsAllowed,
                            IsAudit = input.IsAudit,
                            NodeId = input.NodeId
                        };
                        var state = NodeElementActionState.Create(entity);
                        var action = element.Ontology.Actions.Values.FirstOrDefault(a => a.Id == input.ActionId);
                        if (action == null)
                        {
                            throw new ValidationException("意外的本体动作标识" + input.ActionId);
                        }
                        nodeElementActionDic[node][element].Add(action.ActionVerb, state);
                        if (isCommand)
                        {
                            try
                            {
                                repository.Add(entity);
                                repository.Context.Commit();
                            }
                            catch
                            {
                                if (nodeElementActionDic.ContainsKey(node) && nodeElementActionDic[node].ContainsKey(element) && nodeElementActionDic[node][element].ContainsKey(action.ActionVerb))
                                {
                                    nodeElementActionDic[node][element].Remove(action.ActionVerb);
                                }
                                repository.Context.Rollback();
                                throw;
                            }
                        }
                    }
                    if (isCommand)
                    {
                        acDomain.MessageDispatcher.DispatchMessage(new NodeElementActionAddedEvent(acSession, entity, input, isPrivate: true));
                    }
                }

                public void Handle(RemoveNodeElementActionCommand message)
                {
                    this.Handle(message.AcSession, message.EntityId, true);
                }

                public void Handle(NodeElementActionRemovedEvent message)
                {
                    if (message.IsPrivate)
                    {
                        return;
                    }
                    this.Handle(message.AcSession, message.Source.Id, false);
                }

                private void Handle(IAcSession acSession, Guid nodeElementActionId, bool isCommand)
                {
                    var acDomain = _set._acDomain;
                    var nodeElementActionDic = _set._nodeElementActionDic;
                    var repository = acDomain.RetrieveRequiredService<IRepository<NodeElementAction, Guid>>();
                    NodeElementAction entity;
                    lock (NodeElementActionSetLocker)
                    {
                        bool exist = false;
                        NodeElementActionState bkState = null;
                        NodeDescriptor node = null;
                        ElementDescriptor element = null;
                        foreach (var item in nodeElementActionDic)
                        {
                            foreach (var item1 in item.Value)
                            {
                                foreach (var item2 in item1.Value)
                                {
                                    if (item2.Value.Id == nodeElementActionId)
                                    {
                                        exist = true;
                                        bkState = item2.Value;
                                        break;
                                    }
                                }
                                if (exist)
                                {
                                    element = item1.Key;
                                    break;
                                }
                            }
                            if (exist)
                            {
                                node = item.Key;
                                break;
                            }
                        }
                        if (!exist)
                        {
                            return;
                        }
                        entity = repository.GetByKey(nodeElementActionId);
                        if (entity == null)
                        {
                            return;
                        }
                        if (nodeElementActionDic.ContainsKey(node) && nodeElementActionDic[node].ContainsKey(element))
                        {
                            var action = element.Ontology.Actions.Values.FirstOrDefault(a => a.Id == entity.ActionId);
                            nodeElementActionDic[node][element].Remove(action.ActionVerb);
                        }
                        if (isCommand)
                        {
                            try
                            {
                                repository.Remove(entity);
                                repository.Context.Commit();
                            }
                            catch
                            {
                                var action = element.Ontology.Actions.Values.FirstOrDefault(a => a.Id == entity.ActionId);
                                if (nodeElementActionDic.ContainsKey(node) && nodeElementActionDic[node].ContainsKey(element) && !nodeElementActionDic[node][element].ContainsKey(action.ActionVerb))
                                {
                                    nodeElementActionDic[node][element].Add(action.ActionVerb, bkState);
                                }
                                repository.Context.Rollback();
                                throw;
                            }
                        }
                    }
                    if (isCommand)
                    {
                        acDomain.MessageDispatcher.DispatchMessage(new NodeElementActionRemovedEvent(acSession, entity, isPrivate: true));
                    }
                }
            }
            #endregion
        }
        #endregion

        // 内部类
        #region NodeCareSet
        /// <summary>
        /// 节点关心本体和节点关心本体元素
        /// </summary>
        private sealed class NodeCareSet
        {
            private readonly Dictionary<NodeDescriptor, IDictionary<ontologyId, isCare>> _ontologyCareDic = new Dictionary<NodeDescriptor, IDictionary<ontologyId, isCare>>();
            private readonly Dictionary<NodeDescriptor, IDictionary<elementId, isCare>> _elementCareDic = new Dictionary<NodeDescriptor, IDictionary<elementId, isCare>>();
            private readonly Dictionary<NodeDescriptor, List<NodeOntologyCareState>> _nodeOntologyCareList = new Dictionary<NodeDescriptor, List<NodeOntologyCareState>>();
            private readonly Dictionary<NodeDescriptor, List<NodeElementCareState>> _nodeElementCareList = new Dictionary<NodeDescriptor, List<NodeElementCareState>>();
            private readonly Dictionary<NodeDescriptor, HashSet<ElementDescriptor>> _nodeInfoIdElements = new Dictionary<NodeDescriptor, HashSet<ElementDescriptor>>();
            private bool _initialized = false;
            private static readonly object NodeCareLocker = new object();
            private readonly Guid _id = Guid.NewGuid();
            private readonly IAcDomain _acDomain;

            public Guid Id
            {
                get { return _id; }
            }

            internal NodeCareSet(IAcDomain acDomain)
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
                new NodeCareMessageHandler(this).Register();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="node"></param>
            /// <returns></returns>
            public IEnumerable<ElementDescriptor> GetInfoIdElements(NodeDescriptor node)
            {
                if (node == null)
                {
                    throw new ArgumentNullException("node");
                }
                if (!_initialized)
                {
                    Init();
                }
                return _nodeInfoIdElements[node];
            }

            public bool IsInfoIdElement(NodeDescriptor node, ElementDescriptor element)
            {
                if (node == null)
                {
                    throw new ArgumentNullException("node");
                }
                if (element == null)
                {
                    throw new ArgumentNullException("element");
                }
                if (!_initialized)
                {
                    Init();
                }
                return _nodeInfoIdElements.ContainsKey(node) && _nodeInfoIdElements[node].Contains(element);
            }

            /// <summary>
            /// 判断本节点是否关心给定的本体元素
            /// </summary>
            /// <param name="node"></param>
            /// <param name="element">本体元素码</param>
            /// <returns>True表示关心，False表示不关心</returns>
            public bool IsCareforElement(NodeDescriptor node, ElementDescriptor element)
            {
                if (node == null)
                {
                    throw new ArgumentNullException("node");
                }
                if (element == null)
                {
                    throw new ArgumentNullException("element");
                }
                if (!_initialized)
                {
                    Init();
                }
                if (!_elementCareDic.ContainsKey(node))
                {
                    return false;
                }
                if (!_ontologyCareDic[node].ContainsKey(element.Element.OntologyId))
                {
                    return false;
                }
                if (!_elementCareDic[node].ContainsKey(element.Element.Id))
                {
                    return false;
                }

                return _elementCareDic[node][element.Element.Id];
            }

            /// <summary>
            /// 判断本节点是否关心给定的本体
            /// </summary>
            /// <param name="node"></param>
            /// <param name="ontology"></param>
            /// <returns>True表示关心，False表示不关心</returns>
            public bool IsCareForOntology(NodeDescriptor node, OntologyDescriptor ontology)
            {
                if (node == null)
                {
                    throw new ArgumentNullException("node");
                }
                if (ontology == null)
                {
                    throw new ArgumentNullException("ontology");
                }
                if (!_initialized)
                {
                    Init();
                }
                if (!_ontologyCareDic.ContainsKey(node))
                {
                    return false;
                }
                if (!_ontologyCareDic[node].ContainsKey(ontology.Ontology.Id))
                {
                    return false;
                }

                return _ontologyCareDic[node][ontology.Ontology.Id];
            }

            public IReadOnlyCollection<NodeOntologyCareState> GetNodeOntologyCares(NodeDescriptor node)
            {
                if (node == null)
                {
                    throw new ArgumentNullException("node");
                }
                if (!_initialized)
                {
                    Init();
                }
                if (!_nodeOntologyCareList.ContainsKey(node))
                {
                    return new List<NodeOntologyCareState>();
                }
                return _nodeOntologyCareList[node];
            }

            public IEnumerable<NodeOntologyCareState> GetNodeOntologyCares()
            {
                if (!_initialized)
                {
                    Init();
                }
                return _nodeOntologyCareList.SelectMany(g => g.Value);
            }

            public IReadOnlyCollection<NodeElementCareState> GetNodeElementCares(NodeDescriptor node)
            {
                if (node == null)
                {
                    throw new ArgumentNullException("node");
                }
                if (!_initialized)
                {
                    Init();
                }
                if (!_nodeElementCareList.ContainsKey(node))
                {
                    return new List<NodeElementCareState>();
                }
                return _nodeElementCareList[node];
            }

            #region Init
            private void Init()
            {
                if (_initialized) return;
                lock (NodeCareLocker)
                {
                    if (_initialized) return;
                    _ontologyCareDic.Clear();
                    _elementCareDic.Clear();
                    _nodeOntologyCareList.Clear();
                    _nodeElementCareList.Clear();
                    _nodeInfoIdElements.Clear();
                    var nodeOntologyCareStates = _acDomain.RetrieveRequiredService<INodeHostBootstrap>().GetNodeOntologyCares().Select(NodeOntologyCareState.Create);
                    var nodeElementCareStates = _acDomain.RetrieveRequiredService<INodeHostBootstrap>().GetNodeElementCares().Select(NodeElementCareState.Create);
                    foreach (var node in _acDomain.NodeHost.Nodes)
                    {
                        var node1 = node;
                        _nodeOntologyCareList.Add(node, nodeOntologyCareStates.Where(a => a.NodeId == node1.Node.Id).ToList());
                        var node2 = node;
                        _nodeElementCareList.Add(node, nodeElementCareStates.Where(a => a.NodeId == node2.Node.Id).ToList());
                    }

                    foreach (var ontology in _acDomain.NodeHost.Ontologies)
                    {
                        foreach (var element in _acDomain.NodeHost.Ontologies[ontology.Ontology.Id].Elements.Values)
                        {
                            foreach (var node in _acDomain.NodeHost.Nodes)
                            {
                                if (element == null)
                                {
                                    return;
                                }
                                if (!_ontologyCareDic.ContainsKey(node))
                                {
                                    _ontologyCareDic.Add(node, new Dictionary<ontologyId, isCare>());
                                }
                                if (!_ontologyCareDic[node].ContainsKey(element.Element.OntologyId))
                                {
                                    var element1 = element;
                                    _ontologyCareDic[node].Add(element.Element.OntologyId, _nodeOntologyCareList[node]
                                        .Any(s => s.OntologyId == element1.Element.OntologyId));
                                }
                                if (!_elementCareDic.ContainsKey(node))
                                {
                                    _elementCareDic.Add(node, new Dictionary<elementId, isCare>());
                                }
                                if (!_nodeInfoIdElements.ContainsKey(node))
                                {
                                    _nodeInfoIdElements.Add(node, new HashSet<ElementDescriptor>());
                                    _nodeInfoIdElements[node].Add(ontology.IdElement);
                                }
                                if (!_elementCareDic[node].ContainsKey(element.Element.Id))
                                {
                                    var element2 = element;
                                    var nodeElementCare = _nodeElementCareList[node].FirstOrDefault(f => f.ElementId == element2.Element.Id);
                                    _elementCareDic[node].Add(element.Element.Id, nodeElementCare != null);
                                    if (nodeElementCare != null && nodeElementCare.IsInfoIdItem)
                                    {
                                        _nodeInfoIdElements[node].Add(element);
                                    }
                                }
                            }
                        }
                    }
                    _initialized = true;
                }
            }

            #endregion

            #region NodeCareMessageHandler
            private class NodeCareMessageHandler :
                IHandler<AddNodeOntologyCareCommand>,
                IHandler<NodeOntologyCareAddedEvent>,
                IHandler<RemoveNodeOntologyCareCommand>,
                IHandler<NodeOntologyCareRemovedEvent>,
                IHandler<AddNodeElementCareCommand>,
                IHandler<NodeElementCareAddedEvent>,
                IHandler<UpdateNodeElementCareCommand>,
                IHandler<NodeElementCareUpdatedEvent>,
                IHandler<RemoveNodeElementCareCommand>,
                IHandler<NodeElementCareRemovedEvent>
            {
                private readonly NodeCareSet _set;

                internal NodeCareMessageHandler(NodeCareSet set)
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
                    messageDispatcher.Register((IHandler<AddNodeOntologyCareCommand>)this);
                    messageDispatcher.Register((IHandler<NodeOntologyCareAddedEvent>)this);
                    messageDispatcher.Register((IHandler<RemoveNodeOntologyCareCommand>)this);
                    messageDispatcher.Register((IHandler<NodeOntologyCareRemovedEvent>)this);
                    messageDispatcher.Register((IHandler<AddNodeElementCareCommand>)this);
                    messageDispatcher.Register((IHandler<NodeElementCareAddedEvent>)this);
                    messageDispatcher.Register((IHandler<UpdateNodeElementCareCommand>)this);
                    messageDispatcher.Register((IHandler<NodeElementCareUpdatedEvent>)this);
                    messageDispatcher.Register((IHandler<RemoveNodeElementCareCommand>)this);
                    messageDispatcher.Register((IHandler<NodeElementCareRemovedEvent>)this);
                }

                public void Handle(AddNodeOntologyCareCommand message)
                {
                    this.Handle(message.AcSession, message.Input, true);
                }

                public void Handle(NodeOntologyCareAddedEvent message)
                {
                    if (message.IsPrivate)
                    {
                        return;
                    }
                    this.Handle(message.AcSession, message.Output, false);
                }

                private void Handle(IAcSession acSession, INodeOntologyCareCreateIo input, bool isCommand)
                {
                    var acDomain = _set._acDomain;
                    var nodeOntologyCareList = _set._nodeOntologyCareList;
                    var ontologyCareDic = _set._ontologyCareDic;
                    var repository = acDomain.RetrieveRequiredService<IRepository<NodeOntologyCare, Guid>>();
                    NodeDescriptor bNode;
                    if (!acDomain.NodeHost.Nodes.TryGetNodeById(input.NodeId.ToString(), out bNode))
                    {
                        throw new ValidationException("意外的节点标识" + input.NodeId);
                    }
                    OntologyDescriptor ontology;
                    if (!acDomain.NodeHost.Ontologies.TryGetOntology(input.OntologyId, out ontology))
                    {
                        throw new ValidationException("意外的本体标识" + input.OntologyId);
                    }
                    NodeOntologyCare entity;
                    lock (NodeCareLocker)
                    {
                        if (nodeOntologyCareList[bNode].Any(a => a.OntologyId == input.OntologyId && a.NodeId == input.NodeId))
                        {
                            throw new ValidationException("给定的节点已关心给定的本体，无需重复关心");
                        }
                        entity = NodeOntologyCare.Create(input);
                        var state = NodeOntologyCareState.Create(entity);
                        if (!nodeOntologyCareList.ContainsKey(bNode))
                        {
                            nodeOntologyCareList.Add(bNode, new List<NodeOntologyCareState>());
                        }
                        if (!nodeOntologyCareList[bNode].Contains(state))
                        {
                            nodeOntologyCareList[bNode].Add(state);
                        }
                        if (!ontologyCareDic.ContainsKey(bNode))
                        {
                            ontologyCareDic.Add(bNode, new Dictionary<ontologyId, isCare>());
                        }
                        if (!ontologyCareDic[bNode].ContainsKey(input.OntologyId))
                        {
                            ontologyCareDic[bNode].Add(input.OntologyId, true);
                        }
                        if (isCommand)
                        {
                            try
                            {
                                repository.Add(entity);
                                repository.Context.Commit();
                            }
                            catch
                            {
                                if (nodeOntologyCareList.ContainsKey(bNode) && nodeOntologyCareList[bNode].Contains(state))
                                {
                                    nodeOntologyCareList[bNode].Remove(state);
                                }
                                if (ontologyCareDic.ContainsKey(bNode) && ontologyCareDic[bNode].ContainsKey(input.OntologyId))
                                {
                                    ontologyCareDic[bNode].Remove(input.OntologyId);
                                }
                                repository.Context.Rollback();
                                throw;
                            }
                        }
                    }
                    if (isCommand)
                    {
                        acDomain.MessageDispatcher.DispatchMessage(new NodeOntologyCareAddedEvent(acSession, entity, input, isPrivate: true));
                    }
                }

                public void Handle(RemoveNodeOntologyCareCommand message)
                {
                    this.Handle(message.AcSession, message.EntityId, true);
                }

                public void Handle(NodeOntologyCareRemovedEvent message)
                {
                    if (message.IsPrivate)
                    {
                        return;
                    }
                    this.Handle(message.AcSession, message.Source.Id, false);
                }

                private void Handle(IAcSession acSession, Guid nodeOntologyCareId, bool isCommand)
                {
                    var acDomain = _set._acDomain;
                    var nodeOntologyCareList = _set._nodeOntologyCareList;
                    var ontologyCareDic = _set._ontologyCareDic;
                    var repository = acDomain.RetrieveRequiredService<IRepository<NodeOntologyCare, Guid>>();
                    NodeOntologyCare entity;
                    lock (NodeCareLocker)
                    {
                        NodeOntologyCareState bkState = null;
                        NodeDescriptor bNode = null;
                        foreach (var item in nodeOntologyCareList)
                        {
                            foreach (var item1 in item.Value)
                            {
                                if (item1.Id == nodeOntologyCareId)
                                {
                                    bkState = item1;
                                    break;
                                }
                            }
                            if (bkState != null)
                            {
                                bNode = item.Key;
                            }
                        }
                        if (bkState == null)
                        {
                            return;
                        }
                        entity = repository.GetByKey(nodeOntologyCareId);
                        if (entity == null)
                        {
                            return;
                        }
                        nodeOntologyCareList[bNode].Remove(bkState);
                        ontologyCareDic[bNode].Remove(bkState.OntologyId);
                        try
                        {
                            if (isCommand)
                            {
                                repository.Remove(entity);
                                repository.Context.Commit();
                            }
                        }
                        catch
                        {
                            nodeOntologyCareList[bNode].Add(bkState);
                            ontologyCareDic[bNode].Add(bkState.OntologyId, true);
                            repository.Context.Rollback();
                            throw;
                        }
                    }

                    if (isCommand)
                    {
                        acDomain.MessageDispatcher.DispatchMessage(new NodeOntologyCareRemovedEvent(acSession, entity, isPrivate: true));
                    }
                }

                public void Handle(AddNodeElementCareCommand message)
                {
                    this.Handle(message.AcSession, message.Input, true);
                }

                public void Handle(NodeElementCareAddedEvent message)
                {
                    if (message.IsPrivate)
                    {
                        return;
                    }
                    this.Handle(message.AcSession, message.Output, false);
                }

                private void Handle(IAcSession acSession, INodeElementCareCreateIo input, bool isCommand)
                {
                    var acDomain = _set._acDomain;
                    var nodeElementCareList = _set._nodeElementCareList;
                    var elementCareDic = _set._elementCareDic;
                    var repository = acDomain.RetrieveRequiredService<IRepository<NodeElementCare, Guid>>();
                    NodeDescriptor bNode;
                    if (!acDomain.NodeHost.Nodes.TryGetNodeById(input.NodeId.ToString(), out bNode))
                    {
                        throw new ValidationException("意外的节点标识" + input.NodeId);
                    }
                    ElementDescriptor element;
                    if (!acDomain.NodeHost.Ontologies.TryGetElement(input.ElementId, out element))
                    {
                        throw new ValidationException("意外的本体元素标识" + input.ElementId);
                    }
                    NodeElementCare entity;
                    lock (NodeCareLocker)
                    {
                        if (nodeElementCareList[bNode].Any(a => a.ElementId == input.ElementId && a.NodeId == input.NodeId))
                        {
                            throw new ValidationException("给定的节点已关心给定的本体元素，无需重复关心");
                        }
                        entity = NodeElementCare.Create(input);
                        var state = NodeElementCareState.Create(entity);
                        if (!nodeElementCareList.ContainsKey(bNode))
                        {
                            nodeElementCareList.Add(bNode, new List<NodeElementCareState>());
                        }
                        if (!nodeElementCareList[bNode].Contains(state))
                        {
                            nodeElementCareList[bNode].Add(state);
                        }
                        if (!elementCareDic.ContainsKey(bNode))
                        {
                            elementCareDic.Add(bNode, new Dictionary<elementId, isCare>());
                        }
                        if (!elementCareDic[bNode].ContainsKey(input.ElementId))
                        {
                            elementCareDic[bNode].Add(input.ElementId, true);
                        }
                        if (isCommand)
                        {
                            try
                            {
                                repository.Add(entity);
                                repository.Context.Commit();
                            }
                            catch
                            {
                                if (nodeElementCareList.ContainsKey(bNode) && nodeElementCareList[bNode].Contains(state))
                                {
                                    nodeElementCareList[bNode].Remove(state);
                                }
                                if (elementCareDic.ContainsKey(bNode) && elementCareDic[bNode].ContainsKey(input.ElementId))
                                {
                                    elementCareDic[bNode].Remove(input.ElementId);
                                }
                                repository.Context.Rollback();
                                throw;
                            }
                        }
                    }
                    if (isCommand)
                    {
                        acDomain.MessageDispatcher.DispatchMessage(new NodeElementCareAddedEvent(acSession, entity, input, isPrivate: true));
                    }
                }

                public void Handle(UpdateNodeElementCareCommand message)
                {
                    this.Handle(message.AcSession, message.NodeElementCareId, message.IsInfoIdItem, true);
                }

                public void Handle(NodeElementCareUpdatedEvent message)
                {
                    if (message.IsPrivate)
                    {
                        return;
                    }
                    this.Handle(message.AcSession, message.Source.Id, message.IsInfoIdItem, false);
                }

                private void Handle(IAcSession acSession, Guid nodeElementCareId, bool isInfoIdItem, bool isCommand)
                {
                    var acDomain = _set._acDomain;
                    var nodeElementCareList = _set._nodeElementCareList;
                    var nodeInfoIdElements = _set._nodeInfoIdElements;
                    var repository = acDomain.RetrieveRequiredService<IRepository<NodeElementCare, Guid>>();
                    NodeElementCare entity;
                    lock (NodeCareLocker)
                    {
                        NodeElementCareState bkState = null;
                        NodeDescriptor bNode = null;
                        foreach (var item in nodeElementCareList)
                        {
                            foreach (var item1 in item.Value)
                            {
                                if (item1.Id == nodeElementCareId)
                                {
                                    bkState = item1;
                                    break;
                                }
                            }
                            if (bkState != null)
                            {
                                bNode = item.Key;
                            }
                        }
                        if (bkState == null)
                        {
                            throw new NotExistException();
                        }
                        entity = repository.GetByKey(nodeElementCareId);
                        if (entity == null)
                        {
                            throw new NotExistException();
                        }
                        ElementDescriptor element;
                        if (!acDomain.NodeHost.Ontologies.TryGetElement(entity.ElementId, out element))
                        {
                            throw new ValidationException("意外的本体元素标识" + entity.ElementId);
                        }
                        entity.IsInfoIdItem = isInfoIdItem;
                        var newState = NodeElementCareState.Create(entity);
                        nodeElementCareList[bNode].Remove(bkState);
                        nodeElementCareList[bNode].Add(newState);
                        nodeInfoIdElements[bNode].Add(element);
                        try
                        {
                            if (isCommand)
                            {
                                repository.Update(entity);
                                repository.Context.Commit();
                            }
                        }
                        catch
                        {
                            nodeElementCareList[bNode].Remove(newState);
                            nodeElementCareList[bNode].Add(bkState);
                            nodeInfoIdElements[bNode].Remove(element);
                            repository.Context.Rollback();
                            throw;
                        }
                    }

                    if (isCommand)
                    {
                        acDomain.MessageDispatcher.DispatchMessage(new NodeElementCareUpdatedEvent(acSession, entity, isPrivate: true));
                    }
                }

                public void Handle(RemoveNodeElementCareCommand message)
                {
                    this.HandleElement(message.AcSession, message.EntityId, true);
                }

                public void Handle(NodeElementCareRemovedEvent message)
                {
                    if (message.IsPrivate)
                    {
                        return;
                    }
                    this.HandleElement(message.AcSession, message.Source.Id, false);
                }

                private void HandleElement(IAcSession acSession, Guid nodeElementCareId, bool isCommand)
                {
                    var acDomain = _set._acDomain;
                    var nodeElementCareList = _set._nodeElementCareList;
                    var elementCareDic = _set._elementCareDic;
                    var nodeInfoIdElements = _set._nodeInfoIdElements;
                    var repository = acDomain.RetrieveRequiredService<IRepository<NodeElementCare, Guid>>();
                    NodeElementCare entity;
                    lock (NodeCareLocker)
                    {
                        NodeElementCareState bkState = null;
                        NodeDescriptor bNode = null;
                        foreach (var item in nodeElementCareList)
                        {
                            foreach (var item1 in item.Value)
                            {
                                if (item1.Id == nodeElementCareId)
                                {
                                    bkState = item1;
                                    break;
                                }
                            }
                            if (bkState != null)
                            {
                                bNode = item.Key;
                            }
                        }
                        if (bkState == null)
                        {
                            return;
                        }
                        entity = repository.GetByKey(nodeElementCareId);
                        if (entity == null)
                        {
                            return;
                        }
                        ElementDescriptor element;
                        if (!acDomain.NodeHost.Ontologies.TryGetElement(entity.ElementId, out element))
                        {
                            throw new ValidationException("意外的本体元素标识" + entity.ElementId);
                        }
                        nodeElementCareList[bNode].Remove(bkState);
                        elementCareDic[bNode].Remove(bkState.ElementId);
                        bool isInfoIdElement = false;
                        if (nodeInfoIdElements.ContainsKey(bNode) && nodeInfoIdElements[bNode].Contains(element))
                        {
                            isInfoIdElement = true;
                            nodeInfoIdElements[bNode].Remove(element);
                        }
                        try
                        {
                            if (isCommand)
                            {
                                repository.Remove(entity);
                                repository.Context.Commit();
                            }
                        }
                        catch
                        {
                            nodeElementCareList[bNode].Add(bkState);
                            elementCareDic[bNode].Add(bkState.ElementId, true);
                            if (isInfoIdElement)
                            {
                                nodeInfoIdElements[bNode].Add(element);
                            }
                            repository.Context.Rollback();
                            throw;
                        }
                    }

                    if (isCommand)
                    {
                        acDomain.MessageDispatcher.DispatchMessage(new NodeElementCareRemovedEvent(acSession, entity, isPrivate: true));
                    }
                }
            }
            #endregion
        }
        #endregion

        // 内部类
        #region CatalogSet
        private sealed class CatalogSet
        {
            private readonly Dictionary<NodeDescriptor, Dictionary<OntologyDescriptor, Dictionary<CatalogState, NodeOntologyCatalogState>>>
                _dic = new Dictionary<NodeDescriptor, Dictionary<OntologyDescriptor, Dictionary<CatalogState, NodeOntologyCatalogState>>>();
            private bool _initialized = false;
            private static readonly object CatalogSetLocker = new object();
            private readonly Guid _id = Guid.NewGuid();
            private readonly IAcDomain _acDomain;

            public Guid Id
            {
                get { return _id; }
            }

            internal CatalogSet(IAcDomain acDomain)
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
                var messageDispatcher = acDomain.MessageDispatcher;
                if (messageDispatcher == null)
                {
                    throw new ArgumentNullException("AcDomain对象'{0}'尚未设置MessageDispatcher。".Fmt(acDomain.Name));
                }
                new NodeOntologyCatalogMessageHandler(this).Register();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name = "node"></param>
            /// <param name="ontology"></param>
            /// <returns>key为目录码</returns>
            public Dictionary<CatalogState, NodeOntologyCatalogState> this[NodeDescriptor node, OntologyDescriptor ontology]
            {
                get
                {
                    if (node == null)
                    {
                        throw new ArgumentNullException("node");
                    }
                    if (ontology == null)
                    {
                        throw new ArgumentNullException("ontology");
                    }
                    if (!_initialized)
                    {
                        Init();
                    }
                    if (!_dic.ContainsKey(node))
                    {
                        return new Dictionary<CatalogState, NodeOntologyCatalogState>();
                    }
                    if (!_dic[node].ContainsKey(ontology))
                    {
                        return new Dictionary<CatalogState, NodeOntologyCatalogState>();
                    }

                    return _dic[node][ontology];
                }
            }

            public IEnumerable<NodeOntologyCatalogState> GetNodeOntologyCatalogs()
            {
                if (!_initialized)
                {
                    Init();
                }
                foreach (var gg in _dic.Values)
                {
                    foreach (var g in gg.Values)
                    {
                        foreach (var item in g.Values)
                        {
                            yield return item;
                        }
                    }
                }
            }

            private void Init()
            {
                if (_initialized) return;
                lock (CatalogSetLocker)
                {
                    if (_initialized) return;
                    _dic.Clear();
                    var ontologyOrgs = _acDomain.RetrieveRequiredService<INodeHostBootstrap>().GetNodeOntologyCatalogs();
                    foreach (var nodeOntologyOrg in ontologyOrgs)
                    {
                        CatalogState org;
                        NodeDescriptor node;
                        OntologyDescriptor ontology;
                        _acDomain.NodeHost.Nodes.TryGetNodeById(nodeOntologyOrg.NodeId.ToString(), out node);
                        _acDomain.NodeHost.Ontologies.TryGetOntology(nodeOntologyOrg.OntologyId, out ontology);
                        if (_acDomain.CatalogSet.TryGetCatalog(nodeOntologyOrg.CatalogId, out org))
                        {
                            if (!_dic.ContainsKey(node))
                            {
                                _dic.Add(node, new Dictionary<OntologyDescriptor, Dictionary<CatalogState, NodeOntologyCatalogState>>());
                            }
                            if (!_dic[node].ContainsKey(ontology))
                            {
                                _dic[node].Add(ontology, new Dictionary<CatalogState, NodeOntologyCatalogState>());
                            }
                            var nodeOntologyOrgState = NodeOntologyCatalogState.Create(_acDomain, nodeOntologyOrg);
                            _dic[node][ontology].Add(org, nodeOntologyOrgState);
                        }
                        else
                        {
                            // TODO:移除废弃的目录
                        }
                    }
                    _initialized = true;
                }
            }

            #region NodeOntologyCatalogMessageHandler
            private class NodeOntologyCatalogMessageHandler :
                IHandler<AddNodeOntologyCatalogCommand>,
                IHandler<NodeOntologyCatalogAddedEvent>,
                IHandler<RemoveNodeOntologyCatalogCommand>,
                IHandler<NodeOntologyCatalogRemovedEvent>
            {
                private readonly CatalogSet _set;

                internal NodeOntologyCatalogMessageHandler(CatalogSet set)
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
                    messageDispatcher.Register((IHandler<AddNodeOntologyCatalogCommand>)this);
                    messageDispatcher.Register((IHandler<NodeOntologyCatalogAddedEvent>)this);
                    messageDispatcher.Register((IHandler<RemoveNodeOntologyCatalogCommand>)this);
                    messageDispatcher.Register((IHandler<NodeOntologyCatalogRemovedEvent>)this);
                }

                public void Handle(AddNodeOntologyCatalogCommand message)
                {
                    this.Handle(message.AcSession, message.Input, true);
                }

                public void Handle(NodeOntologyCatalogAddedEvent message)
                {
                    if (message.IsPrivate)
                    {
                        return;
                    }
                    this.Handle(message.AcSession, message.Output, false);
                }

                private void Handle(IAcSession acSession, INodeOntologyCatalogCreateIo input, bool isCommand)
                {
                    var acDomain = _set._acDomain;
                    var dic = _set._dic;
                    var repository = acDomain.RetrieveRequiredService<IRepository<NodeOntologyCatalog, Guid>>();
                    if (!input.Id.HasValue)
                    {
                        throw new ValidationException("标识是必须的");
                    }
                    NodeDescriptor node;
                    if (!acDomain.NodeHost.Nodes.TryGetNodeById(input.NodeId.ToString(), out node))
                    {
                        throw new ValidationException("意外的节点标识" + input.NodeId);
                    }
                    OntologyDescriptor ontology;
                    if (!acDomain.NodeHost.Ontologies.TryGetOntology(input.OntologyId, out ontology))
                    {
                        throw new ValidationException("意外的本体标识" + input.OntologyId);
                    }
                    CatalogState catalog;
                    if (!acDomain.CatalogSet.TryGetCatalog(input.CatalogId, out catalog))
                    {
                        throw new ValidationException("意外的目录标识" + input.CatalogId);
                    }
                    NodeOntologyCatalog entity;
                    lock (CatalogSetLocker)
                    {
                        if (dic.ContainsKey(node) && dic[node].ContainsKey(ontology) && dic[node][ontology].ContainsKey(catalog))
                        {
                            return;
                        }
                        entity = new NodeOntologyCatalog(input.Id.Value)
                        {
                            NodeId = input.NodeId,
                            OntologyId = input.OntologyId,
                            CatalogId = input.CatalogId
                        };
                        try
                        {
                            var state = NodeOntologyCatalogState.Create(acDomain, entity);
                            if (!dic.ContainsKey(node))
                            {
                                dic.Add(node, new Dictionary<OntologyDescriptor, Dictionary<CatalogState, NodeOntologyCatalogState>>());
                            }
                            if (!dic[node].ContainsKey(ontology))
                            {
                                dic[node].Add(ontology, new Dictionary<CatalogState, NodeOntologyCatalogState>());
                            }
                            if (!dic[node][ontology].ContainsKey(catalog))
                            {
                                dic[node][ontology].Add(catalog, state);
                            }
                            repository.Add(entity);
                            repository.Context.Commit();
                        }
                        catch
                        {
                            if (dic.ContainsKey(node) && dic[node].ContainsKey(ontology) && dic[node][ontology].ContainsKey(catalog))
                            {
                                dic[node][ontology].Remove(catalog);
                            }
                            throw;
                        }
                    }
                    if (isCommand)
                    {
                        acDomain.MessageDispatcher.DispatchMessage(new NodeOntologyCatalogAddedEvent(acSession, entity, input, isPrivate: true));
                    }
                }

                public void Handle(RemoveNodeOntologyCatalogCommand message)
                {
                    this.Handle(message.AcSession, message.NodeId, message.OntologyId, message.CatalogId, true);
                }

                public void Handle(NodeOntologyCatalogRemovedEvent message)
                {
                    if (message.IsPrivate)
                    {
                        return;
                    }
                    var entity = message.Source as NodeOntologyCatalogBase;
                    this.Handle(message.AcSession, entity.NodeId, entity.OntologyId, entity.CatalogId, false);
                }

                private void Handle(IAcSession acSession, Guid nodeId, Guid ontologyId, Guid catalogId, bool isCommand)
                {
                    var acDomain = _set._acDomain;
                    var dic = _set._dic;
                    var repository = acDomain.RetrieveRequiredService<IRepository<NodeOntologyCatalog, Guid>>();
                    NodeDescriptor node;
                    if (!acDomain.NodeHost.Nodes.TryGetNodeById(nodeId.ToString(), out node))
                    {
                        throw new ValidationException("意外的节点标识" + nodeId);
                    }
                    OntologyDescriptor ontology;
                    if (!acDomain.NodeHost.Ontologies.TryGetOntology(ontologyId, out ontology))
                    {
                        throw new ValidationException("意外的本体标识" + ontologyId);
                    }
                    CatalogState catalog;
                    if (!acDomain.CatalogSet.TryGetCatalog(catalogId, out catalog))
                    {
                        throw new ValidationException("意外的目录标识" + catalogId);
                    }
                    if (!dic.ContainsKey(node) && !dic[node].ContainsKey(ontology) && !dic[node][ontology].ContainsKey(catalog))
                    {
                        return;
                    }
                    var bkState = dic[node][ontology][catalog];
                    NodeOntologyCatalog entity;
                    lock (bkState)
                    {
                        entity = repository.AsQueryable().FirstOrDefault(a => a.OntologyId == ontologyId && a.NodeId == nodeId && a.CatalogId == catalogId);
                        if (entity == null)
                        {
                            return;
                        }
                        try
                        {
                            dic[node][ontology].Remove(catalog);
                            repository.Remove(entity);
                        }
                        catch
                        {
                            dic[node][ontology].Add(catalog, bkState);
                            throw;
                        }
                    }
                    if (isCommand)
                    {
                        acDomain.MessageDispatcher.DispatchMessage(new NodeOntologyCatalogRemovedEvent(acSession, entity, isPrivate: true));
                    }
                }
            }
            #endregion
        }
        #endregion
    }
}