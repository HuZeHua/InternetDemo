
namespace Anycmd.Logging
{
    using Events;
    using System;

    /// <summary>
    /// 操作完成事件
    /// </summary>
    public class OperatedEvent : DomainEvent
    {
        public OperatedEvent(OperationLogBase source) : base(source) { }

        /// <summary>
        /// 
        /// </summary>
        public DateTime OperatedOn { get; set; }
    }
}