
namespace Anycmd.Engine.Ac.Dsd
{
    using Events;
    using Messages;

    public sealed class DsdRoleAddedEvent : EntityAddedEvent<IDsdRoleCreateIo>
    {
        public DsdRoleAddedEvent(IAcSession acSession, DsdRoleBase source, IDsdRoleCreateIo output)
            : base(acSession, source, output)
        {
        }

        internal DsdRoleAddedEvent(IAcSession acSession, DsdRoleBase source, IDsdRoleCreateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    public sealed class DsdRoleRemovedEvent : DomainEvent
    {
        public DsdRoleRemovedEvent(IAcSession acSession, DsdRoleBase source)
            : base(acSession, source)
        {
        }

        internal DsdRoleRemovedEvent(IAcSession acSession, DsdRoleBase source, bool isPrivate)
            : this(acSession, source)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    public sealed class DsdSetAddedEvent : EntityAddedEvent<IDsdSetCreateIo>
    {
        public DsdSetAddedEvent(IAcSession acSession, DsdSetBase source, IDsdSetCreateIo output)
            : base(acSession, source, output)
        {
        }

        internal DsdSetAddedEvent(IAcSession acSession, DsdSetBase source, IDsdSetCreateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    public sealed class DsdSetRemovedEvent : DomainEvent
    {
        public DsdSetRemovedEvent(IAcSession acSession, DsdSetBase source)
            : base(acSession, source)
        {
        }

        internal DsdSetRemovedEvent(IAcSession acSession, DsdSetBase source, bool isPrivate)
            : this(acSession, source)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    public sealed class DsdSetUpdatedEvent : DomainEvent
    {
        public DsdSetUpdatedEvent(IAcSession acSession, DsdSetBase source, IDsdSetUpdateIo output)
            : base(acSession, source)
        {
            if (output == null)
            {
                throw new System.ArgumentNullException("output");
            }
            this.Output = output;
        }

        internal DsdSetUpdatedEvent(IAcSession acSession, DsdSetBase source, IDsdSetUpdateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        public IDsdSetUpdateIo Output { get; private set; }
        internal bool IsPrivate { get; private set; }
    }
}
