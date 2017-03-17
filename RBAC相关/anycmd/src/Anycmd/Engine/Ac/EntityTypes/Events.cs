
namespace Anycmd.Engine.Ac.EntityTypes
{
    using Events;
    using Messages;

    public sealed class EntityTypeAddedEvent : EntityAddedEvent<IEntityTypeCreateIo>
    {
        public EntityTypeAddedEvent(IAcSession acSession, EntityTypeBase source, IEntityTypeCreateIo input)
            : base(acSession, source, input)
        {
        }

        internal EntityTypeAddedEvent(IAcSession acSession, EntityTypeBase source, IEntityTypeCreateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class EntityTypeRemovedEvent : DomainEvent
    {
        public EntityTypeRemovedEvent(IAcSession acSession, EntityTypeBase source)
            : base(acSession, source)
        {
        }

        internal EntityTypeRemovedEvent(IAcSession acSession, EntityTypeBase source, bool isPrivate)
            : this(acSession, source)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    public sealed class EntityTypeRemovingEvent : DomainEvent
    {
        public EntityTypeRemovingEvent(IAcSession acSession, EntityTypeBase source)
            : base(acSession, source)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class EntityTypeUpdatedEvent : DomainEvent
    {
        public EntityTypeUpdatedEvent(IAcSession acSession, EntityTypeBase source, IEntityTypeUpdateIo input)
            : base(acSession, source)
        {
            if (input == null)
            {
                throw new System.ArgumentNullException("input");
            }
            this.Input = input;
        }

        internal EntityTypeUpdatedEvent(IAcSession acSession, EntityTypeBase source, IEntityTypeUpdateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        public IEntityTypeUpdateIo Input { get; private set; }
        internal bool IsPrivate { get; private set; }
    }

    public sealed class PropertyAddedEvent : EntityAddedEvent<IPropertyCreateIo>
    {
        public PropertyAddedEvent(IAcSession acSession, PropertyBase source, IPropertyCreateIo input)
            : base(acSession, source, input)
        {
        }

        internal PropertyAddedEvent(IAcSession acSession, PropertyBase source, IPropertyCreateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class PropertyRemovedEvent : DomainEvent
    {
        public PropertyRemovedEvent(IAcSession acSession, PropertyBase source)
            : base(acSession, source)
        {
        }

        internal PropertyRemovedEvent(IAcSession acSession, PropertyBase source, bool isPrivate)
            : this(acSession, source)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class PropertyUpdatedEvent : DomainEvent
    {
        public PropertyUpdatedEvent(IAcSession acSession, PropertyBase source, IPropertyUpdateIo input)
            : base(acSession, source)
        {
            if (input == null)
            {
                throw new System.ArgumentNullException("input");
            }
            this.Input = input;
        }

        internal PropertyUpdatedEvent(IAcSession acSession, PropertyBase source, IPropertyUpdateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        public IPropertyUpdateIo Input { get; private set; }
        internal bool IsPrivate { get; private set; }
    }
}
