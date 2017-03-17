
namespace Anycmd.Events.Storage
{
    using Events;
    using Serialization;
    using System;
    using System.IO;
    using Util;

    public static class DomainEventSerializerExtension
    {
        /// <summary>
        /// 根据给定的领域事件对象创建并初始化领域事件数据传输对象。
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="entity">领域事件对象。</param>
        /// <returns>领域事件数据传输对象。</returns>
        public static DomainEventDataObject<TSourceId> FromDomainEvent<TSourceId>(this IDomainEventSerializer serializer, IDomainEvent<TSourceId> entity)
        {
            var obj = new DomainEventDataObject<TSourceId>
            {
                Branch = entity.Branch,
                Data = serializer.Serialize(entity),
                Id = entity.Id,
                AssemblyQualifiedEventType =
                    string.IsNullOrEmpty(entity.AssemblyQualifiedEventType)
                        ? entity.GetType().AssemblyQualifiedName
                        : entity.AssemblyQualifiedEventType,
                Timestamp = entity.Timestamp,
                Version = entity.Version,
                SourceId = entity.Source.Id,
                AssemblyQualifiedSourceType = entity.Source.GetType().AssemblyQualifiedName
            };

            return obj;
        }

        /// <summary>
        /// 将给定的领域事件数据传输对象转化为相应的领域事件对象。
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="from"></param>
        /// <returns>领域事件对象。</returns>
        public static IDomainEvent<TSourceId> ToDomainEvent<TSourceId>(this IDomainEventSerializer serializer, DomainEventDataObject<TSourceId> from)
        {
            if (string.IsNullOrEmpty(from.AssemblyQualifiedEventType))
                throw new InvalidDataException("form.AssemblyQualifiedTypeName");
            if (from.Data == null || from.Data.Length == 0)
                throw new InvalidDataException("Data");

            var type = Type.GetType(from.AssemblyQualifiedEventType);
            var ret = (IDomainEvent<TSourceId>)serializer.Deserialize(type, from.Data);

            return ret;
        }
    }
}
