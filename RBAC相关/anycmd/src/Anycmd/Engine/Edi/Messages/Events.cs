
namespace Anycmd.Engine.Edi.Messages
{
    using Abstractions;
    using Events;
    using Info;
    using InOuts;

    /// <summary>
    /// 
    /// </summary>
    public sealed class ActionAddedEvent : DomainEvent
    {
        public ActionAddedEvent(IAcSession acSession, ActionBase source) : base(acSession, source) { }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class ActionRemovedEvent : DomainEvent
    {
        public ActionRemovedEvent(IAcSession acSession, ActionBase source) : base(acSession, source) { }
    }

    public sealed class ActionUpdatedEvent : DomainEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public ActionUpdatedEvent(IAcSession acSession, ActionBase source)
            : base(acSession, source)
        {
            this.Verb = source.Verb;
            this.Name = source.Name;
            this.IsAllowed = source.IsAllowed;
            this.IsAudit = source.IsAudit;
            this.IsPersist = source.IsPersist;
            this.SortCode = source.SortCode;
        }

        public string Verb { get; private set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 排序
        /// </summary>
        public string IsAllowed { get; private set; }
        public string IsAudit { get; private set; }
        public bool IsPersist { get; private set; }
        public int SortCode { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class ArchiveDeletedEvent : DomainEvent
    {
        public ArchiveDeletedEvent(IAcSession acSession, ArchiveBase source) : base(acSession, source) { }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class ArchivedEvent : DomainEvent
    {
        public ArchivedEvent(IAcSession acSession, ArchiveBase source)
            : base(acSession, source)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class ArchiveUpdatedEvent : DomainEvent
    {
        public ArchiveUpdatedEvent(IAcSession acSession, ArchiveBase source)
            : base(acSession, source)
        {
            this.DataSource = source.DataSource;
            this.FilePath = source.FilePath;
            this.NumberId = source.NumberId;
            this.UserId = source.UserId;
            this.Password = source.Password;
        }

        /// <summary>
        /// 源
        /// </summary>
        public string DataSource { get; private set; }
        /// <summary>
        /// 归档库路径
        /// </summary>
        public string FilePath { get; private set; }
        /// <summary>
        /// 数字标识
        /// </summary>
        public int NumberId { get; private set; }
        /// <summary>
        /// 数据库用户名
        /// </summary>
        public string UserId { get; private set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; private set; }
    }

    public sealed class BatchAddedEvent : DomainEvent
    {
        public BatchAddedEvent(IAcSession acSession, IBatch source)
            : base(acSession, source)
        {
        }
    }

    public sealed class BatchRemovedEvent : DomainEvent
    {
        public BatchRemovedEvent(IAcSession acSession, IBatch source)
            : base(acSession, source)
        {
        }
    }

    public sealed class BatchUpdatedEvent : DomainEvent
    {
        public BatchUpdatedEvent(IAcSession acSession, IBatch source)
            : base(acSession, source)
        {
        }
    }

    public sealed class CatalogActionAddedEvent : DomainEvent
    {
        public CatalogActionAddedEvent(IAcSession acSession, CatalogAction source) : base(acSession, source) { }
    }

    public sealed class CatalogActionRemovedEvent : DomainEvent
    {
        public CatalogActionRemovedEvent(IAcSession acSession, CatalogAction source) : base(acSession, source) { }
    }

    public sealed class CatalogActionUpdatedEvent : DomainEvent
    {
        public CatalogActionUpdatedEvent(IAcSession acSession, CatalogAction source)
            : base(acSession, source)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class ElementActionAddedEvent : DomainEvent
    {
        public ElementActionAddedEvent(IAcSession acSession, ElementAction source)
            : base(acSession, source)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class ElementActionRemovedEvent : DomainEvent
    {
        public ElementActionRemovedEvent(IAcSession acSession, ElementAction source)
            : base(acSession, source)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class ElementActionUpdatedEvent : DomainEvent
    {
        public ElementActionUpdatedEvent(IAcSession acSession, ElementAction source)
            : base(acSession, source)
        {
            this.IsAllowed = source.IsAllowed;
            this.IsAudit = source.IsAudit;
        }

        public string IsAllowed { get; private set; }
        public string IsAudit { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class ElementAddedEvent : DomainEvent
    {
        public ElementAddedEvent(IAcSession acSession, ElementBase source) : base(acSession, source) { }
    }

    public sealed class ElementInfoRuleAddedEvent : DomainEvent
    {
        public ElementInfoRuleAddedEvent(IAcSession acSession, ElementInfoRule source) : base(acSession, source) { }
    }

    public sealed class ElementInfoRuleRemovedEvent : DomainEvent
    {
        public ElementInfoRuleRemovedEvent(IAcSession acSession, ElementInfoRule source) : base(acSession, source) { }
    }

    public sealed class ElementInfoRuleUpdatedEvent : DomainEvent
    {
        public ElementInfoRuleUpdatedEvent(IAcSession acSession, ElementInfoRule source) : base(acSession, source) { }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class ElementRemovedEvent : DomainEvent
    {
        public ElementRemovedEvent(IAcSession acSession, ElementBase source) : base(acSession, source) { }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class ElementUpdatedEvent : DomainEvent
    {
        public ElementUpdatedEvent(IAcSession acSession, ElementBase source)
            : base(acSession, source)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class InfoDicAddedEvent : DomainEvent
    {
        public InfoDicAddedEvent(IAcSession acSession, InfoDicBase source, IInfoDicCreateIo output)
            : base(acSession, source)
        {
            if (output == null)
            {
                throw new System.ArgumentNullException("output");
            }
            this.Output = output;
        }

        internal InfoDicAddedEvent(IAcSession acSession, InfoDicBase source, IInfoDicCreateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        public IInfoDicCreateIo Output { get; private set; }
        internal bool IsPrivate { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class InfoDicItemAddedEvent : DomainEvent
    {
        public InfoDicItemAddedEvent(IAcSession acSession, InfoDicItemBase source, IInfoDicItemCreateIo output)
            : base(acSession, source)
        {
            if (output == null)
            {
                throw new System.ArgumentNullException("output");
            }
            this.Output = output;
        }

        internal InfoDicItemAddedEvent(IAcSession acSession, InfoDicItemBase source, IInfoDicItemCreateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        public IInfoDicItemCreateIo Output { get; private set; }
        internal bool IsPrivate { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class InfoDicItemRemovedEvent : DomainEvent
    {
        public InfoDicItemRemovedEvent(IAcSession acSession, InfoDicItemBase source) : base(acSession, source) { }

        internal InfoDicItemRemovedEvent(IAcSession acSession, InfoDicItemBase source, bool isPrivate)
            : this(acSession, source)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class InfoDicItemUpdatedEvent : DomainEvent
    {
        public InfoDicItemUpdatedEvent(IAcSession acSession, InfoDicItemBase source, IInfoDicItemUpdateIo output)
            : base(acSession, source)
        {
            if (output == null)
            {
                throw new System.ArgumentNullException("output");
            }
            this.Output = output;
        }

        internal InfoDicItemUpdatedEvent(IAcSession acSession, InfoDicItemBase source, IInfoDicItemUpdateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        public IInfoDicItemUpdateIo Output { get; private set; }
        internal bool IsPrivate { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class InfoDicRemovedEvent : DomainEvent
    {
        public InfoDicRemovedEvent(IAcSession acSession, InfoDicBase source) : base(acSession, source) { }

        internal InfoDicRemovedEvent(IAcSession acSession, InfoDicBase source, bool isPrivate)
            : this(acSession, source)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class InfoDicUpdatedEvent : DomainEvent
    {
        public InfoDicUpdatedEvent(IAcSession acSession, InfoDicBase source, IInfoDicUpdateIo output)
            : base(acSession, source)
        {
            if (output == null)
            {
                throw new System.ArgumentNullException("output");
            }
            this.Output = output;
        }

        internal InfoDicUpdatedEvent(IAcSession acSession, InfoDicBase source, IInfoDicUpdateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        public IInfoDicUpdateIo Output { get; private set; }
        internal bool IsPrivate { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class InfoGroupAddedEvent : DomainEvent
    {
        public InfoGroupAddedEvent(IAcSession acSession, InfoGroupBase source) : base(acSession, source) { }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class InfoGroupRemovedEvent : DomainEvent
    {
        public InfoGroupRemovedEvent(IAcSession acSession, InfoGroupBase source) : base(acSession, source) { }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class InfoGroupUpdatedEvent : DomainEvent
    {
        public InfoGroupUpdatedEvent(IAcSession acSession, InfoGroupBase source)
            : base(acSession, source)
        {
        }
    }

    public sealed class InfoRuleUpdatedEvent : DomainEvent
    {
        public InfoRuleUpdatedEvent(IAcSession acSession, InfoRuleEntityBase source, IInfoRuleUpdateIo output)
            : base(acSession, source)
        {
            if (output == null)
            {
                throw new System.ArgumentNullException("output");
            }
            this.Output = output;
        }

        public IInfoRuleUpdateIo Output { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class NodeActionAddedEvent : DomainEvent
    {
        public NodeActionAddedEvent(IAcSession acSession, NodeAction source) : base(acSession, source) { }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class NodeActionRemovedEvent : DomainEvent
    {
        public NodeActionRemovedEvent(IAcSession acSession, NodeAction source) : base(acSession, source) { }
    }

    public sealed class NodeActionUpdatedEvent : DomainEvent
    {
        public NodeActionUpdatedEvent(IAcSession acSession, NodeAction source)
            : base(acSession, source)
        {
            this.IsAllowed = source.IsAllowed;
            this.IsAudit = source.IsAudit;
        }

        public string IsAllowed { get; private set; }
        public string IsAudit { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class NodeAddedEvent : DomainEvent
    {
        public NodeAddedEvent(IAcSession acSession, NodeBase source, INodeCreateIo output)
            : base(acSession, source)
        {
            if (output == null)
            {
                throw new System.ArgumentNullException("output");
            }
            this.Output = output;
        }

        internal NodeAddedEvent(IAcSession acSession, NodeBase source, INodeCreateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        public INodeCreateIo Output { get; private set; }
        internal bool IsPrivate { get; private set; }
    }

    public sealed class NodeCatalogActionAddedEvent : DomainEvent
    {
        public NodeCatalogActionAddedEvent(IAcSession acSession, NodeCatalogAction source) : base(acSession, source) { }
    }

    public sealed class NodeCatalogActionRemovedEvent : DomainEvent
    {
        public NodeCatalogActionRemovedEvent(IAcSession acSession, NodeCatalogAction source) : base(acSession, source) { }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class NodeElementActionAddedEvent : DomainEvent
    {
        public NodeElementActionAddedEvent(IAcSession acSession, NodeElementActionBase source, INodeElementActionCreateIo output)
            : base(acSession, source)
        {
            if (output == null)
            {
                throw new System.ArgumentNullException("output");
            }
            this.Output = output;
        }

        internal NodeElementActionAddedEvent(IAcSession acSession, NodeElementActionBase source, INodeElementActionCreateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        public INodeElementActionCreateIo Output { get; private set; }
        internal bool IsPrivate { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class NodeElementActionRemovedEvent : DomainEvent
    {
        public NodeElementActionRemovedEvent(IAcSession acSession, NodeElementActionBase source) : base(acSession, source) { }

        internal NodeElementActionRemovedEvent(IAcSession acSession, NodeElementActionBase source, bool isPrivate)
            : this(acSession, source)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class NodeElementCareAddedEvent : DomainEvent
    {
        public NodeElementCareAddedEvent(IAcSession acSession, NodeElementCareBase source, INodeElementCareCreateIo output)
            : base(acSession, source)
        {
            if (output == null)
            {
                throw new System.ArgumentNullException("output");
            }
            this.Output = output;
        }

        internal NodeElementCareAddedEvent(IAcSession acSession, NodeElementCareBase source, INodeElementCareCreateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        public INodeElementCareCreateIo Output { get; private set; }
        internal bool IsPrivate { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class NodeElementCareRemovedEvent : DomainEvent
    {
        public NodeElementCareRemovedEvent(IAcSession acSession, NodeElementCareBase source) : base(acSession, source) { }

        internal NodeElementCareRemovedEvent(IAcSession acSession, NodeElementCareBase source, bool isPrivate)
            : this(acSession, source)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    public sealed class NodeElementCareUpdatedEvent : DomainEvent
    {
        public NodeElementCareUpdatedEvent(IAcSession acSession, NodeElementCareBase source)
            : base(acSession, source)
        {
            this.IsInfoIdItem = source.IsInfoIdItem;
        }

        internal NodeElementCareUpdatedEvent(IAcSession acSession, NodeElementCareBase source, bool isPrivate)
            : this(acSession, source)
        {
            this.IsPrivate = isPrivate;
        }

        public bool IsInfoIdItem { get; private set; }
        internal bool IsPrivate { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class NodeOntologyCareAddedEvent : DomainEvent
    {
        public NodeOntologyCareAddedEvent(IAcSession acSession, NodeOntologyCareBase source, INodeOntologyCareCreateIo output)
            : base(acSession, source)
        {
            if (output == null)
            {
                throw new System.ArgumentNullException("output");
            }
            this.Output = output;
        }

        internal NodeOntologyCareAddedEvent(IAcSession acSession, NodeOntologyCareBase source, INodeOntologyCareCreateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        public INodeOntologyCareCreateIo Output { get; private set; }
        internal bool IsPrivate { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class NodeOntologyCareRemovedEvent : DomainEvent
    {
        public NodeOntologyCareRemovedEvent(IAcSession acSession, NodeOntologyCareBase source) : base(acSession, source) { }

        internal NodeOntologyCareRemovedEvent(IAcSession acSession, NodeOntologyCareBase source, bool isPrivate)
            : this(acSession, source)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    public sealed class NodeOntologyCatalogAddedEvent : DomainEvent
    {
        public NodeOntologyCatalogAddedEvent(IAcSession acSession, NodeOntologyCatalogBase source, INodeOntologyCatalogCreateIo output)
            : base(acSession, source)
        {
            if (output == null)
            {
                throw new System.ArgumentNullException("output");
            }
            this.Output = output;
        }

        internal NodeOntologyCatalogAddedEvent(IAcSession acSession, NodeOntologyCatalogBase source, INodeOntologyCatalogCreateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        public INodeOntologyCatalogCreateIo Output { get; private set; }
        internal bool IsPrivate { get; private set; }
    }

    public sealed class NodeOntologyCatalogRemovedEvent : DomainEvent
    {
        public NodeOntologyCatalogRemovedEvent(IAcSession acSession, NodeOntologyCatalogBase source) : base(acSession, source) { }

        internal NodeOntologyCatalogRemovedEvent(IAcSession acSession, NodeOntologyCatalogBase source, bool isPrivate)
            : this(acSession, source)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class NodeRemovedEvent : DomainEvent
    {
        public NodeRemovedEvent(IAcSession acSession, NodeBase source) : base(acSession, source) { }

        internal NodeRemovedEvent(IAcSession acSession, NodeBase source, bool isPrivate)
            : this(acSession, source)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class NodeUpdatedEvent : DomainEvent
    {
        public NodeUpdatedEvent(IAcSession acSession, NodeBase source, INodeUpdateIo output)
            : base(acSession, source)
        {
            if (output == null)
            {
                throw new System.ArgumentNullException("output");
            }
            this.Output = output;
        }

        internal NodeUpdatedEvent(IAcSession acSession, NodeBase source, INodeUpdateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        public INodeUpdateIo Output { get; private set; }
        internal bool IsPrivate { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class OntologyAddedEvent : DomainEvent
    {
        public OntologyAddedEvent(IAcSession acSession, OntologyBase source, IOntologyCreateIo output)
            : base(acSession, source)
        {
            if (output == null)
            {
                throw new System.ArgumentNullException("output");
            }
            this.Output = output;
        }

        internal OntologyAddedEvent(IAcSession acSession, OntologyBase source, IOntologyCreateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        public IOntologyCreateIo Output { get; private set; }
        internal bool IsPrivate { get; private set; }
    }

    public sealed class OntologyCatalogAddedEvent : DomainEvent
    {
        public OntologyCatalogAddedEvent(IAcSession acSession, OntologyCatalogBase source, IOntologyCatalogCreateIo output)
            : base(acSession, source)
        {
            if (output == null)
            {
                throw new System.ArgumentNullException("output");
            }
            this.Output = output;
        }

        internal OntologyCatalogAddedEvent(IAcSession acSession, OntologyCatalogBase source, IOntologyCatalogCreateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        public IOntologyCatalogCreateIo Output { get; private set; }
        internal bool IsPrivate { get; private set; }
    }

    public sealed class OntologyCatalogRemovedEvent : DomainEvent
    {
        public OntologyCatalogRemovedEvent(IAcSession acSession, OntologyCatalogBase source) : base(acSession, source) { }

        internal OntologyCatalogRemovedEvent(IAcSession acSession, OntologyCatalogBase source, bool isPrivate)
            : this(acSession, source)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class OntologyRemovedEvent : DomainEvent
    {
        public OntologyRemovedEvent(IAcSession acSession, OntologyBase source) : base(acSession, source) { }

        internal OntologyRemovedEvent(IAcSession acSession, OntologyBase source, bool isPrivate)
            : this(acSession, source)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class OntologyUpdatedEvent : DomainEvent
    {
        public OntologyUpdatedEvent(IAcSession acSession, OntologyBase source, IOntologyUpdateIo output)
            : base(acSession, source)
        {
            if (output == null)
            {
                throw new System.ArgumentNullException("output");
            }
            this.Output = output;
        }

        internal OntologyUpdatedEvent(IAcSession acSession, OntologyBase source, IOntologyUpdateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        public IOntologyUpdateIo Output { get; private set; }
        internal bool IsPrivate { get; private set; }
    }

    public sealed class ProcessAddedEvent : DomainEvent
    {
        public ProcessAddedEvent(IAcSession acSession, ProcessBase source)
            : base(acSession, source)
        {
        }
    }

    public sealed class ProcessRemovedEvent : DomainEvent
    {
        public ProcessRemovedEvent(IAcSession acSession, ProcessBase source) : base(acSession, source) { }
    }

    public sealed class ProcessUpdatedEvent : DomainEvent
    {
        public ProcessUpdatedEvent(IAcSession acSession, ProcessBase source)
            : base(acSession, source)
        {
        }
    }

    public sealed class TopicAddedEvent : DomainEvent
    {
        public TopicAddedEvent(IAcSession acSession, TopicBase source) : base(acSession, source) { }
    }

    public sealed class TopicRemovedEvent : DomainEvent
    {
        public TopicRemovedEvent(IAcSession acSession, TopicBase source) : base(acSession, source) { }
    }

    public sealed class TopicUpdatedEvent : DomainEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public TopicUpdatedEvent(IAcSession acSession, TopicBase source)
            : base(acSession, source)
        {
        }
    }
}
