
namespace Anycmd.Engine.Ac.Catalogs
{
    using Events;
    using Messages;
    using System;

    public sealed class CatalogAddedEvent : EntityAddedEvent<ICatalogCreateIo>
    {
        public CatalogAddedEvent(IAcSession acSession, CatalogBase source, ICatalogCreateIo input)
            : base(acSession, source, input)
        {
        }

        internal CatalogAddedEvent(IAcSession acSession, CatalogBase source, ICatalogCreateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class CatalogRemovedEvent : DomainEvent
    {
        public CatalogRemovedEvent(IAcSession acSession, CatalogBase source)
            : base(acSession, source)
        {
            if (source == null)
            {
                throw new ArgumentException("source");
            }
            this.CatalogCode = source.Code;
        }

        internal CatalogRemovedEvent(IAcSession acSession, CatalogBase source, bool isPrivate)
            : this(acSession, source)
        {
            this.IsPrivate = isPrivate;
        }

        public string CatalogCode { get; private set; }
        internal bool IsPrivate { get; private set; }
    }

    public sealed class CatalogRemovingEvent : DomainEvent
    {
        public CatalogRemovingEvent(IAcSession acSession, CatalogBase source)
            : base(acSession, source)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class CatalogUpdatedEvent : DomainEvent
    {
        public CatalogUpdatedEvent(IAcSession acSession, CatalogBase source, ICatalogUpdateIo input)
            : base(acSession, source)
        {
            if (input == null)
            {
                throw new System.ArgumentNullException("input");
            }
            this.Input = input;
        }

        internal CatalogUpdatedEvent(IAcSession acSession, CatalogBase source, ICatalogUpdateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        public ICatalogUpdateIo Input { get; private set; }
        internal bool IsPrivate { get; private set; }
    }
}
