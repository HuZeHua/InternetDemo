
namespace Anycmd.Snapshots
{
    using Model;
    using Serialization;
    using System;
    using Util;

    public static class SnapshotSerializerExtension
    {
        /// <summary>
        /// 从给定的快照数据对象萃取得到快照对象。
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="dataObject">快照数据对象</param>
        /// <returns>快照对象</returns>
        public static ISnapshot<TAggregateRootId> ExtractSnapshot<TAggregateRootId>(this ISnapshotSerializer serializer, SnapshotDataObject<TAggregateRootId> dataObject)
        {
            var snapshotType = Type.GetType(dataObject.SnapshotType);
            if (snapshotType == null)
                return null;

            return (ISnapshot<TAggregateRootId>)serializer.Deserialize(snapshotType, dataObject.SnapshotData);
        }

        /// <summary>
        /// 为给定的聚合根实体创建快照数据对象。
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="aggregateRoot">快照为该聚合根对象创建。</param>
        /// <returns>快照数据对象。</returns>
        public static SnapshotDataObject<TAggregateRootId> CreateFromAggregateRoot<TAggregateRootId>(this ISnapshotSerializer serializer, ISourcedAggregateRoot<TAggregateRootId> aggregateRoot)
        {
            var snapshot = aggregateRoot.CreateSnapshot();

            return new SnapshotDataObject<TAggregateRootId>
            {
                AggregateRootId = aggregateRoot.Id,
                AggregateRootType = aggregateRoot.GetType().AssemblyQualifiedName,
                Version = aggregateRoot.Version,
                Branch = Constants.ApplicationRuntime.DefaultBranch,
                SnapshotType = snapshot.GetType().AssemblyQualifiedName,
                Timestamp = snapshot.Timestamp,
                SnapshotData = serializer.Serialize(snapshot)
            };
        }
    }
}
