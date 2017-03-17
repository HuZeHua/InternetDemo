
namespace Anycmd.Engine.Ac.Roles
{
    using Events;
    using Messages;
    using System;

    public sealed class RoleAddedEvent : EntityAddedEvent<IRoleCreateIo>
    {
        public RoleAddedEvent(IAcSession acSession, RoleBase source, IRoleCreateIo output)
            : base(acSession, source, output)
        {
        }

        internal RoleAddedEvent(IAcSession acSession, RoleBase source, IRoleCreateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    public sealed class RoleRemovedEvent : DomainEvent
    {
        public RoleRemovedEvent(IAcSession acSession, RoleBase source)
            : base(acSession, source)
        {
        }

        internal RoleRemovedEvent(IAcSession acSession, RoleBase source, bool isPrivate)
            : this(acSession, source)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    public sealed class RoleRemovingEvent : DomainEvent
    {
        public RoleRemovingEvent(IAcSession acSession, RoleBase source)
            : base(acSession, source)
        {
        }
    }

    public sealed class RoleUpdatedEvent : DomainEvent
    {
        public RoleUpdatedEvent(IAcSession acSession, RoleBase source, IRoleUpdateIo output)
            : base(acSession, source)
        {
            if (output == null)
            {
                throw new ArgumentNullException("output");
            }
            this.Output = output;
        }

        internal RoleUpdatedEvent(IAcSession acSession, RoleBase source, IRoleUpdateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        public IRoleUpdateIo Output { get; private set; }
        internal bool IsPrivate { get; private set; }
    }
}
