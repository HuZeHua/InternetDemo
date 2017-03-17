
namespace Anycmd.Engine.Ac.Privileges
{
    using Events;

    public sealed class PrivilegeAddedEvent : DomainEvent
    {
        public PrivilegeAddedEvent(IAcSession acSession, PrivilegeBase source, IPrivilegeCreateIo output)
            : base(acSession, source)
        {
            if (output == null)
            {
                throw new System.ArgumentNullException("output");
            }
            this.Output = output;
        }

        internal PrivilegeAddedEvent(IAcSession acSession, PrivilegeBase source, IPrivilegeCreateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        public IPrivilegeCreateIo Output { get; private set; }
        internal bool IsPrivate { get; private set; }
    }

    public sealed class PrivilegeRemovedEvent : DomainEvent
    {
        public PrivilegeRemovedEvent(IAcSession acSession, PrivilegeBase source)
            : base(acSession, source)
        {
        }

        internal PrivilegeRemovedEvent(IAcSession acSession, PrivilegeBase source, bool isPrivate)
            : this(acSession, source)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    public sealed class PrivilegeUpdatedEvent : DomainEvent
    {
        public PrivilegeUpdatedEvent(IAcSession acSession, PrivilegeBase source, IPrivilegeUpdateIo output)
            : base(acSession, source)
        {
            if (output == null)
            {
                throw new System.ArgumentNullException("output");
            }
            this.Output = output;
        }

        internal PrivilegeUpdatedEvent(IAcSession acSession, PrivilegeBase source, IPrivilegeUpdateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        public IPrivilegeUpdateIo Output { get; private set; }
        internal bool IsPrivate { get; private set; }
    }

    public sealed class RoleRolePrivilegeAddedEvent : DomainEvent
    {
        public RoleRolePrivilegeAddedEvent(IAcSession acSession, PrivilegeBase source)
            : base(acSession, source)
        {
        }
    }

    public sealed class RoleRolePrivilegeRemovedEvent : DomainEvent
    {
        public RoleRolePrivilegeRemovedEvent(IAcSession acSession, PrivilegeBase source)
            : base(acSession, source)
        {
        }
    }
}
