﻿
namespace Anycmd.Events
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    /// <summary>
    /// 表示一种事件聚合器实现。
    /// </summary>
    /// <remarks>关于事件聚合器的更多信息，请访问：http://msdn.microsoft.com/en-us/library/ff921122(v=pandp.20).aspx
    /// </remarks>
    public class EventAggregator : IEventAggregator
    {
        #region Private Fields
        private readonly object _sync = new object();
        private readonly Dictionary<Type, List<object>> _eventHandlers = new Dictionary<Type, List<object>>();
        private readonly MethodInfo _registerEventHandlerMethod;
        private readonly Func<object, object, bool> _eventHandlerEquals = (o1, o2) =>
        {
            var o1Type = o1.GetType();
            var o2Type = o2.GetType();
            if (o1Type.IsGenericType &&
                o1Type.GetGenericTypeDefinition() == typeof(ActionDelegatedEventHandler<>) &&
                o2Type.IsGenericType &&
                o2Type.GetGenericTypeDefinition() == typeof(ActionDelegatedEventHandler<>))
                return o1.Equals(o2);
            return o1Type == o2Type;
        };
        // checks if the two event handlers are equal. if the event handler is an action-delegated, just simply
        // compare the two with the object.Equals override (since it was overriden by comparing the two delegates. Otherwise,
        // the type of the event handler will be used because we don't need to register the same type of the event handler
        // more than once for each specific event.
        #endregion

        #region Ctor
        /// <summary>
        /// 初始化一个 <c>EventAggregator</c> 类型的对象。
        /// </summary>
        public EventAggregator()
        {
            _registerEventHandlerMethod = (from p in this.GetType().GetMethods()
                                           let methodName = p.Name
                                           let parameters = p.GetParameters()
                                           where methodName == "Subscribe" &&
                                           parameters != null &&
                                           parameters.Length == 1 &&
                                           parameters[0].ParameterType.GetGenericTypeDefinition() == typeof(IEventHandler<>)
                                           select p).First();
        }

        /// <summary>
        /// 初始化一个 <c>EventAggregator</c> 类型的对象。
        /// </summary>
        /// <param name="handlers">The event handlers to be registered to the Event Aggregator.</param>
        /// <remarks>
        /// All the event handlers registered to the Event Aggregator should implement the <see cref="IEventHandler{T}"/>
        /// interface, otherwise, the instance will be ignored. When using IoC containers to register dependencies,
        /// remember to specify not only the name of the dependency, but also the type of the dependency. For example,
        /// in the Unity container configuration section, you should register the handlers by using the following snippet:
        /// <code>
        /// &lt;register type="Anycmd.Events.IEventAggregator, Anycmd" mapTo="Anycmd.Events.EventAggregator, Anycmd"&gt;
        ///  &lt;constructor&gt;
        ///    &lt;param name="handlers"&gt;
        ///      &lt;array&gt;
        ///        &lt;dependency name="orderDispatchedSendEmailHandler" type="Anycmd.Events.IEventHandler`1[[ByteartRetail.Domain.Events.OrderDispatchedEvent, ByteartRetail.Domain]], Anycmd" /&gt;
        ///        &lt;dependency name="orderConfirmedSendEmailHandler" type="Anycmd.Events.IEventHandler`1[[ByteartRetail.Domain.Events.OrderConfirmedEvent, ByteartRetail.Domain]], Anycmd" /&gt;
        ///      &lt;/array&gt;
        ///    &lt;/param&gt;
        ///  &lt;/constructor&gt;
        ///&lt;/register&gt;
        /// </code>
        /// </remarks>
        public EventAggregator(IEnumerable<object> handlers)
            : this()
        {
            foreach (var obj in handlers)
            {
                var type = obj.GetType();
                var implementedInterfaces = type.GetInterfaces();
                foreach (var method in from implementedInterface in implementedInterfaces
                                       where implementedInterface.IsGenericType && implementedInterface.GetGenericTypeDefinition() == typeof(IEventHandler<>)
                                       select implementedInterface.GetGenericArguments().First() into eventType
                                       select _registerEventHandlerMethod.MakeGenericMethod(eventType))
                {
                    method.Invoke(this, new object[] { obj });
                }
            }
        }
        #endregion

        #region IEventAggregator Members
        /// <summary>
        /// Subscribes the event handler to a given event type.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="eventHandler">The event handler.</param>
        public void Subscribe<TEvent>(IEventHandler<TEvent> eventHandler)
            where TEvent : class, IEvent
        {
            lock (_sync)
            {
                var eventType = typeof(TEvent);
                if (_eventHandlers.ContainsKey(eventType))
                {
                    var handlers = _eventHandlers[eventType];
                    if (handlers != null)
                    {
                        if (!handlers.Exists(deh => _eventHandlerEquals(deh, eventHandler)))
                            handlers.Add(eventHandler);
                    }
                    else
                    {
                        _eventHandlers[eventType] = new List<object> { eventHandler };
                    }
                }
                else
                    _eventHandlers.Add(eventType, new List<object> { eventHandler });
            }
        }

        /// <summary>
        /// Subscribes the event handlers to a given event type.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="eventHandlers">The event handlers.</param>
        public void Subscribe<TEvent>(IEnumerable<IEventHandler<TEvent>> eventHandlers)
            where TEvent : class, IEvent
        {
            foreach (var eventHandler in eventHandlers)
                Subscribe<TEvent>(eventHandler);
        }

        /// <summary>
        /// Subscribes the event handlers to a given event type.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="eventHandlers">The event handlers.</param>
        public void Subscribe<TEvent>(params IEventHandler<TEvent>[] eventHandlers)
            where TEvent : class, IEvent
        {
            foreach (var eventHandler in eventHandlers)
                Subscribe<TEvent>(eventHandler);
        }

        /// <summary>
        /// Subscribes the <see cref="Action{T}"/> delegate to a given event type.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="eventHandlerAction">The <see cref="Action{T}"/> delegate.</param>
        public void Subscribe<TEvent>(Action<TEvent> eventHandlerAction)
            where TEvent : class, IEvent
        {
            Subscribe<TEvent>(new ActionDelegatedEventHandler<TEvent>(eventHandlerAction));
        }

        /// <summary>
        /// Subscribes the <see cref="Action{T}"/> delegates to a given event type.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="eventHandlerActions">The <see cref="Action{T}"/> delegates.</param>
        public void Subscribe<TEvent>(IEnumerable<Action<TEvent>> eventHandlerActions)
            where TEvent : class, IEvent
        {
            foreach (var eventHandlerAction in eventHandlerActions)
                Subscribe<TEvent>(eventHandlerAction);
        }

        /// <summary>
        /// Subscribes the <see cref="Action{T}"/> delegates to a given event type.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="eventHandlerActions">The <see cref="Action{T}"/> delegates.</param>
        public void Subscribe<TEvent>(params Action<TEvent>[] eventHandlerActions)
            where TEvent : class, IEvent
        {
            foreach (var eventHandlerAction in eventHandlerActions)
                Subscribe<TEvent>(eventHandlerAction);
        }

        /// <summary>
        /// Unsubscribes the event handler from a given event type.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="eventHandler">The event handler.</param>
        public void Unsubscribe<TEvent>(IEventHandler<TEvent> eventHandler)
            where TEvent : class, IEvent
        {
            lock (_sync)
            {
                var eventType = typeof(TEvent);
                if (!_eventHandlers.ContainsKey(eventType)) return;
                var handlers = _eventHandlers[eventType];
                if (handlers == null || !handlers.Exists(deh => _eventHandlerEquals(deh, eventHandler))) return;
                var handlerToRemove = handlers.First(deh => _eventHandlerEquals(deh, eventHandler));
                handlers.Remove(handlerToRemove);
            }
        }

        /// <summary>
        /// Unsubscribes the event handlers from a given event type.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="eventHandlers">The event handler.</param>
        public void Unsubscribe<TEvent>(IEnumerable<IEventHandler<TEvent>> eventHandlers)
            where TEvent : class, IEvent
        {
            foreach (var eventHandler in eventHandlers)
                Unsubscribe<TEvent>(eventHandler);
        }

        /// <summary>
        /// Unsubscribes the event handlers from a given event type.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="eventHandlers">The event handlers.</param>
        public void Unsubscribe<TEvent>(params IEventHandler<TEvent>[] eventHandlers)
            where TEvent : class, IEvent
        {
            foreach (var eventHandler in eventHandlers)
                Unsubscribe<TEvent>(eventHandler);
        }

        /// <summary>
        /// Unsubscribes the <see cref="Action{T}"/> delegate from a given event type.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="eventHandlerAction">The <see cref="Action{T}"/> delegate.</param>
        public void Unsubscribe<TEvent>(Action<TEvent> eventHandlerAction)
            where TEvent : class, IEvent
        {
            Unsubscribe<TEvent>(new ActionDelegatedEventHandler<TEvent>(eventHandlerAction));
        }

        /// <summary>
        /// Unsubscribes the <see cref="Action{T}"/> delegates from a given event type.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="eventHandlerActions">The <see cref="Action{T}"/> delegates.</param>
        public void Unsubscribe<TEvent>(IEnumerable<Action<TEvent>> eventHandlerActions)
            where TEvent : class, IEvent
        {
            foreach (var eventHandlerAction in eventHandlerActions)
                Unsubscribe<TEvent>(eventHandlerAction);
        }

        /// <summary>
        /// Unsubscribes the <see cref="Action{T}"/> delegates from a given event type.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="eventHandlerActions">The <see cref="Action{T}"/> delegates.</param>
        public void Unsubscribe<TEvent>(params Action<TEvent>[] eventHandlerActions)
            where TEvent : class, IEvent
        {
            foreach (var eventHandlerAction in eventHandlerActions)
                Unsubscribe<TEvent>(eventHandlerAction);
        }

        /// <summary>
        /// Unsubscribes all the subscribed event handlers from a given event type.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        public void UnsubscribeAll<TEvent>()
            where TEvent : class, IEvent
        {
            lock (_sync)
            {
                var eventType = typeof(TEvent);
                if (!_eventHandlers.ContainsKey(eventType)) return;
                var handlers = _eventHandlers[eventType];
                if (handlers != null)
                    handlers.Clear();
            }
        }

        /// <summary>
        /// Unsubscribes all the event handlers from the event aggregator.
        /// </summary>
        public void UnsubscribeAll()
        {
            lock (_sync)
            {
                _eventHandlers.Clear();
            }
        }

        /// <summary>
        /// Gets the subscribed event handlers for a given event type.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <returns>A collection of subscribed event handlers.</returns>
        public IEnumerable<IEventHandler<TEvent>> GetSubscriptions<TEvent>()
            where TEvent : class, IEvent
        {
            var eventType = typeof(TEvent);
            lock (_sync)
            {
                List<object> handlers;
                if (_eventHandlers.TryGetValue(eventType, out handlers))
                {
                    return handlers != null ? handlers.Select(p => p as IEventHandler<TEvent>).ToList() : null;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Publishes the event to all of its subscriptions.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event to be published.</typeparam>
        /// <param name="evnt">The event to be published.</param>
        public void Publish<TEvent>(TEvent evnt)
            where TEvent : class, IEvent
        {
            if (evnt == null)
                throw new ArgumentNullException("evnt");
            var eventType = evnt.GetType();
            List<object> handlers;
            lock (_sync)
            {
                if (!_eventHandlers.ContainsKey(eventType) 
                    || _eventHandlers[eventType] == null 
                    || _eventHandlers[eventType].Count <= 0)
                {
                    return;
                }
                handlers = _eventHandlers[eventType];
            }
            foreach (var eventHandler in handlers.Select(handler => handler as IEventHandler<TEvent>))
            {
                if (eventHandler != null && eventHandler.GetType().IsDefined(typeof(ParallelExecutionAttribute), false))
                {
                    var handler = eventHandler;
                    Task.Factory.StartNew((o) => handler.Handle((TEvent)o), evnt);
                }
                else
                {
                    if (eventHandler != null) eventHandler.Handle(evnt);
                }
            }
        }

        /// <summary>
        /// Publishes the event to all of its subscriptions.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event to be published.</typeparam>
        /// <param name="event">The event to be published.</param>
        /// <param name="callback">The callback method to be executed after the event has been published and processed.</param>
        /// <param name="timeout">When the event handler is executing in parallel, represents the timeout value
        /// for the handler to complete.</param>
        public void Publish<TEvent>(TEvent @event, Action<TEvent, bool, Exception> callback, TimeSpan? timeout = null)
            where TEvent : class, IEvent
        {
            if (@event == null)
                throw new ArgumentNullException("event");
            var eventType = @event.GetType();
            List<object> handlers;
            lock (_sync)
            {
                if (_eventHandlers.ContainsKey(eventType) 
                    && _eventHandlers[eventType] != null 
                    && _eventHandlers[eventType].Count > 0)
                {
                    handlers = _eventHandlers[eventType];
                }
                else
                {
                    callback(@event, false, null);
                    return;
                }
            }
            var tasks = new List<Task>();
            try
            {
                foreach (var eventHandler in handlers.Select(handler => handler as IEventHandler<TEvent>))
                {
                    if (eventHandler != null && eventHandler.GetType().IsDefined(typeof(ParallelExecutionAttribute), false))
                    {
                        var handler = eventHandler;
                        tasks.Add(Task.Factory.StartNew((o) => handler.Handle((TEvent)o), @event));
                    }
                    else
                    {
                        if (eventHandler != null) eventHandler.Handle(@event);
                    }
                }
                if (tasks.Count > 0)
                {
                    if (timeout == null)
                        Task.WaitAll(tasks.ToArray());
                    else
                        Task.WaitAll(tasks.ToArray(), timeout.Value);
                }
                callback(@event, true, null);
            }
            catch (Exception ex)
            {
                callback(@event, false, ex);
            }
        }
        #endregion
    }
}
