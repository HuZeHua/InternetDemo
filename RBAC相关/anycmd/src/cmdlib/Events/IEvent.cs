
namespace Anycmd.Events
{
    using Bus;
    using Model;
    using System;

    /// <summary>
    /// 表示该接口的实现类是事件。
    /// <remarks>事件是一种穿梭在总线上的消息。</remarks>
    /// </summary>
    public interface IEvent : IMessage, IEntity<Guid>
    {
        /// <summary>
        /// 读取该事件发生的事件。
        /// </summary>
        /// <remarks>系统间的时间格式可能是不同的，系统间推荐使用UTC时间。</remarks>
        DateTime Timestamp { get; }

        /// <summary>
        /// 读取当前事件的程序集级唯一事件.NET类型名称，名称字符串中包括该事件类型所在的程序集的名称。
        /// </summary>
        string AssemblyQualifiedEventType { get; }
    }
}
