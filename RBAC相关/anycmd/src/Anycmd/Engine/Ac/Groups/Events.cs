
namespace Anycmd.Engine.Ac.Groups
{
    using Events;
    using Messages;

    public sealed class GroupAddedEvent : EntityAddedEvent<IGroupCreateIo>
    {
        public GroupAddedEvent(IAcSession acSession, GroupBase source, IGroupCreateIo output)
            : base(acSession, source, output)
        {
        }

        internal GroupAddedEvent(IAcSession acSession, GroupBase source, IGroupCreateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    public sealed class GroupRemovedEvent : DomainEvent
    {
        public GroupRemovedEvent(IAcSession acSession, GroupBase source)
            : base(acSession, source)
        {
        }

        internal GroupRemovedEvent(IAcSession acSession, GroupBase source, bool isPrivate)
            : this(acSession, source)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    public sealed class GroupRemovingEvent : DomainEvent
    {
        public GroupRemovingEvent(IAcSession acSession, GroupBase source)
            : base(acSession, source)
        {
        }
    }

    public sealed class GroupUpdatedEvent : DomainEvent
    {
        public GroupUpdatedEvent(IAcSession acSession, GroupBase source, IGroupUpdateIo output)
            : base(acSession, source)
        {
            if (output == null)
            {
                throw new System.ArgumentNullException("output");
            }
            this.Output = output;
        }

        internal GroupUpdatedEvent(IAcSession acSession, GroupBase source, IGroupUpdateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        public IGroupUpdateIo Output { get; private set; }
        internal bool IsPrivate { get; private set; }
    }
}
