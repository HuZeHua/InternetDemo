
namespace Anycmd.Engine.Ac.Functions
{
    using Events;
    using Messages;

    public sealed class FunctionAddedEvent : EntityAddedEvent<IFunctionCreateIo>
    {
        public FunctionAddedEvent(IAcSession acSession, FunctionBase source, IFunctionCreateIo input)
            : base(acSession, source, input)
        {
        }

        internal bool IsPriviate { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class FunctionRemovedEvent : DomainEvent
    {
        public FunctionRemovedEvent(IAcSession acSession, FunctionBase source)
            : base(acSession, source)
        {
        }

        internal bool IsPriviate { get; set; }
    }

    public sealed class FunctionRemovingEvent : DomainEvent
    {
        public FunctionRemovingEvent(IAcSession acSession, FunctionBase source)
            : base(acSession, source)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class FunctionUpdatedEvent : DomainEvent
    {
        public FunctionUpdatedEvent(IAcSession acSession, FunctionBase source, IFunctionUpdateIo input)
            : base(acSession, source)
        {
            if (input == null)
            {
                throw new System.ArgumentNullException("input");
            }
            this.Input = input;
        }

        public IFunctionUpdateIo Input { get; private set; }
        internal bool IsPriviate { get; set; }
    }
}
