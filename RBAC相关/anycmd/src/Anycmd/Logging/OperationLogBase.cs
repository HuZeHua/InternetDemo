
namespace Anycmd.Logging
{
    using Model;
    using System;

    /// <summary>
    /// 操作日志
    /// </summary>
    public abstract class OperationLogBase : EntityObject<Guid>, IAggregateRoot<Guid>
    {
        public OperationLogBase(Guid id)
            : base(id)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public virtual Guid TargetId { get; set; }
    }
}
