
namespace Anycmd.Snapshots
{
    using System;

    /// <summary>
    /// 表示该接口的实现类是快照。
    /// </summary>
    public interface ISnapshot<TAggregateRootId>
    {
        /// <summary>
        /// 读取或设置快照拍摄时间戳。
        /// </summary>
        DateTime Timestamp { get; set; }
        /// <summary>
        /// 读取或设置快照所拍摄的聚合根的标识。
        /// </summary>
        TAggregateRootId AggregateRootId { get; set; }
        /// <summary>
        /// 读取或设置快照版本号，通常是聚合根的版本号。
        /// </summary>
        long Version { get; set; }
        /// <summary>
        /// 读取或设置快照所在的分枝。
        /// </summary>
        long Branch { get; set; }
    }
}
