
namespace Anycmd.Engine.Ac.Ssd
{
    using Events;
    using Messages;

    public sealed class SsdRoleAddedEvent : EntityAddedEvent<ISsdRoleCreateIo>
    {
        public SsdRoleAddedEvent(IAcSession acSession, SsdRoleBase source, ISsdRoleCreateIo output)
            : base(acSession, source, output)
        {
        }

        internal SsdRoleAddedEvent(IAcSession acSession, SsdRoleBase source, ISsdRoleCreateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    public sealed class SsdRoleRemovedEvent : DomainEvent
    {
        public SsdRoleRemovedEvent(IAcSession acSession, SsdRoleBase source)
            : base(acSession, source)
        {
        }

        internal SsdRoleRemovedEvent(IAcSession acSession, SsdRoleBase source, bool isPrivate)
            : this(acSession, source)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    public sealed class SsdSetAddedEvent : EntityAddedEvent<ISsdSetCreateIo>
    {
        public SsdSetAddedEvent(IAcSession acSession, SsdSetBase source, ISsdSetCreateIo output)
            : base(acSession, source, output)
        {
        }

        internal SsdSetAddedEvent(IAcSession acSession, SsdSetBase source, ISsdSetCreateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    public sealed class SsdSetRemovedEvent : DomainEvent
    {
        public SsdSetRemovedEvent(IAcSession acSession, SsdSetBase source)
            : base(acSession, source)
        {
        }

        internal SsdSetRemovedEvent(IAcSession acSession, SsdSetBase source, bool isPrivate)
            : this(acSession, source)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    public sealed class SsdSetUpdatedEvent : DomainEvent
    {
        public SsdSetUpdatedEvent(IAcSession acSession, SsdSetBase source, ISsdSetUpdateIo output)
            : base(acSession, source)
        {
            if (output == null)
            {
                throw new System.ArgumentNullException("output");
            }
            this.Output = output;
        }

        internal SsdSetUpdatedEvent(IAcSession acSession, SsdSetBase source, ISsdSetUpdateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        public ISsdSetUpdateIo Output { get; private set; }
        internal bool IsPrivate { get; private set; }
    }
}
