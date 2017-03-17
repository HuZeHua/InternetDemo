
namespace Anycmd.Snapshots
{
    using System;

    /// <summary>
    /// 表示快照。
    /// </summary>
    [Serializable]
    public abstract class Snapshot<TAggregateRootId> : ISnapshot<TAggregateRootId>
    {
        /// <summary>
        /// 初始化一个 <c>Snapshot&lt;TAggregateRootId&gt;</c> 类型的对象。
        /// </summary>
        protected Snapshot() { }

        /// <summary>
        /// 读取或设置快照版本号。
        /// </summary>
        public long Version { get; set; }
        /// <summary>
        /// 读取或设置快照分枝。
        /// </summary>
        public long Branch { get; set; }
        /// <summary>
        /// 读取或设置快照版本号，通常是聚合根的版本号。
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// 读取或设置快照所拍摄的聚合根的标识。
        /// </summary>
        public TAggregateRootId AggregateRootId { get; set; }
    }
}
