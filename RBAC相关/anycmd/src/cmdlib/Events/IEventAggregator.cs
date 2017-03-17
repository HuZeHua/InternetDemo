
namespace Anycmd.Events
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 表示该接口的实现类是事件聚合器。
    /// </summary>
    /// <remarks>
    /// 更多信息参见: http://msdn.microsoft.com/en-us/library/ff921122(v=pandp.20).aspx
    /// </remarks>
    public interface IEventAggregator
    {
        #region Methods
        /// <summary>
        /// 订阅给定事件类型的事件处理程序。
        /// </summary>
        /// <typeparam name="TEvent">事件的.NET类型。</typeparam>
        /// <param name="eventHandler">事件处理器。</param>
        void Subscribe<TEvent>(IEventHandler<TEvent> eventHandler)
            where TEvent : class, IEvent;

        /// <summary>
        /// 订阅给定事件类型的事件处理程序。
        /// </summary>
        /// <typeparam name="TEvent">事件的.NET类型。</typeparam>
        /// <param name="eventHandlers">事件处理器集。</param>
        void Subscribe<TEvent>(IEnumerable<IEventHandler<TEvent>> eventHandlers)
            where TEvent : class, IEvent;

        /// <summary>
        /// 订阅给定事件类型的事件处理程序。
        /// </summary>
        /// <typeparam name="TEvent">事件的.NET类型。</typeparam>
        /// <param name="eventHandlers">事件处理器集。</param>
        void Subscribe<TEvent>(params IEventHandler<TEvent>[] eventHandlers)
            where TEvent : class, IEvent;

        /// <summary>
        /// 订阅给定的<see cref="Action{T}"/> 委托到给定的事件类型。
        /// </summary>
        /// <typeparam name="TEvent">事件的.NET类型。</typeparam>
        /// <param name="eventHandlerAction"><see cref="Action{T}"/> 类型的委托方法，指代事件处理器方法。</param>
        void Subscribe<TEvent>(Action<TEvent> eventHandlerAction)
            where TEvent : class, IEvent;

        /// <summary>
        /// 订阅给定的<see cref="Action{T}"/> 委托到给定的事件类型。
        /// </summary>
        /// <typeparam name="TEvent">事件的.NET类型。</typeparam>
        /// <param name="eventHandlerActions"><see cref="Action{T}"/> 类型的委托方法，指代事件处理器方法。</param>
        void Subscribe<TEvent>(IEnumerable<Action<TEvent>> eventHandlerActions)
            where TEvent : class, IEvent;

        /// <summary>
        /// 订阅给定的<see cref="Action{T}"/> 委托到给定的事件类型。
        /// </summary>
        /// <typeparam name="TEvent">事件的.NET类型。</typeparam>
        /// <param name="eventHandlerActions"><see cref="Action{T}"/> 类型的委托方法集，指代事件处理器方法集。</param>
        void Subscribe<TEvent>(params Action<TEvent>[] eventHandlerActions)
            where TEvent : class, IEvent;

        /// <summary>
        /// 反订阅给定的事件处理程序。
        /// </summary>
        /// <typeparam name="TEvent">事件的.NET类型。</typeparam>
        /// <param name="eventHandler">事件处理器。</param>
        void Unsubscribe<TEvent>(IEventHandler<TEvent> eventHandler)
            where TEvent : class, IEvent;

        /// <summary>
        /// 反订阅给定的事件处理程序。
        /// </summary>
        /// <typeparam name="TEvent">事件的.NET类型。</typeparam>
        /// <param name="eventHandlers">事件处理器。</param>
        void Unsubscribe<TEvent>(IEnumerable<IEventHandler<TEvent>> eventHandlers)
            where TEvent : class, IEvent;

        /// <summary>
        /// 反订阅给定的事件处理程序。
        /// </summary>
        /// <typeparam name="TEvent">事件的.NET类型。</typeparam>
        /// <param name="eventHandlers">事件处理器集。</param>
        void Unsubscribe<TEvent>(params IEventHandler<TEvent>[] eventHandlers)
            where TEvent : class, IEvent;

        /// <summary>
        /// 反订阅给定的 <see cref="Action{T}"/> 委托。
        /// </summary>
        /// <typeparam name="TEvent">事件的.NET类型。</typeparam>
        /// <param name="eventHandlerAction"><see cref="Action{T}"/> 类型的委托方法，指代事件处理器方法。</param>
        void Unsubscribe<TEvent>(Action<TEvent> eventHandlerAction)
            where TEvent : class, IEvent;

        /// <summary>
        /// 反订阅给定的 <see cref="Action{T}"/> 委托。
        /// </summary>
        /// <typeparam name="TEvent">事件的.NET类型。</typeparam>
        /// <param name="eventHandlerActions"><see cref="Action{T}"/> 类型的委托方法集，指代事件处理器方法集。</param>
        void Unsubscribe<TEvent>(IEnumerable<Action<TEvent>> eventHandlerActions)
            where TEvent : class, IEvent;

        /// <summary>
        /// 反订阅给定的 <see cref="Action{T}"/> 委托。
        /// </summary>
        /// <typeparam name="TEvent">事件的.NET类型</typeparam>
        /// <param name="eventHandlerActions"><see cref="Action{T}"/> 类型的委托方法集，指代事件处理器方法集。</param>
        void Unsubscribe<TEvent>(params Action<TEvent>[] eventHandlerActions)
            where TEvent : class, IEvent;

        /// <summary>
        /// 取消给定类型的事件的所有订阅。
        /// </summary>
        /// <typeparam name="TEvent">事件的.NET类型。</typeparam>
        void UnsubscribeAll<TEvent>()
            where TEvent : class, IEvent;

        /// <summary>
        /// 反订阅所有的处理程序。
        /// </summary>
        void UnsubscribeAll();

        /// <summary>
        /// 获取给定的类型的事件的订阅程序。
        /// </summary>
        /// <typeparam name="TEvent">事件的.NET类型。</typeparam>
        /// <returns>一个订阅给定的<see cref="TEvent"/>类型的事件的事件处理器集合。</returns>
        IEnumerable<IEventHandler<TEvent>> GetSubscriptions<TEvent>()
            where TEvent : class, IEvent;

        /// <summary>
        /// 发行给定的事件到它所有的订阅程序。
        /// </summary>
        /// <typeparam name="TEvent">将被发布的事件的.NET类型。</typeparam>
        /// <param name="event">要发布的事件。</param>
        void Publish<TEvent>(TEvent @event)
            where TEvent : class, IEvent;

        /// <summary>
        /// 发行给定的事件到它所有的订阅程序。
        /// </summary>
        /// <typeparam name="TEvent">将被发布的事件的.NET类型。</typeparam>
        /// <param name="event">要发布的事件。</param>
        /// <param name="callback">事件被发布并处理后会回调的回调方法。</param>
        /// <param name="timeout">当事件处理器并行执行时，表示在处理器执行完成前等待的逾期时长。</param>
        void Publish<TEvent>(TEvent @event, Action<TEvent, bool, Exception> callback, TimeSpan? timeout = null)
            where TEvent : class, IEvent;
        #endregion
    }
}
