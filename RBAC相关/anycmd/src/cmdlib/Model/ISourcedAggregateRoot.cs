
namespace Anycmd.Model
{
    using Events;
    using Snapshots;
    using System.Collections.Generic;

    /// <summary>
    /// 表示该接口的实现类是支持事件溯源机制的聚合根实体。
    /// </summary>
    public interface ISourcedAggregateRoot<TAggregateRootId> : IAggregateRoot<TAggregateRootId>, ISnapshotOriginator<TAggregateRootId>
    {
        /// <summary>
        /// 根据历史事件建造当前聚合根实体。
        /// </summary>
        /// <param name="historicalEvents">历史事件集。</param>
        void BuildFromHistory(IEnumerable<IDomainEvent<TAggregateRootId>> historicalEvents);

        /// <summary>
        /// 读取所有未提交的事件。
        /// </summary>
        IEnumerable<IDomainEvent<TAggregateRootId>> UncommittedEvents { get; }

        /// <summary>
        /// 读取当前聚合根实体的版本号。
        /// </summary>
        long Version { get; }

        /// <summary>
        /// 读取当前聚合根实体所在的分支。
        /// </summary>
        long Branch { get; }
    }
}
