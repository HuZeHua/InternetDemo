﻿
namespace Anycmd
{
    using Engine.Host.Ac.Infra;
    using Engine.Messages;
    using Events;
    using Exceptions;
    using System;
    using System.Collections.Generic;

    public static class AcDomainExtension
    {
        private static readonly HashSet<EntityTypeMap> EntityTypeMaps = new HashSet<EntityTypeMap>();

        public static void Map(this IAcDomain acDomain, EntityTypeMap map)
        {
            EntityTypeMaps.Add(map);
        }

        public static IEnumerable<EntityTypeMap> GetEntityTypeMaps(this IAcDomain acDomain)
        {
            return EntityTypeMaps;
        }

        /// <summary>
        /// 将给定的事件发布到事件总线
        /// </summary>
        /// <typeparam name="TEvent">事件.NET类型</typeparam>
        /// <param name="acDomain"></param>
        /// <param name="evnt">事件</param>
        public static void PublishEvent<TEvent>(this IAcDomain acDomain, TEvent evnt) where TEvent : class, IEvent
        {
            acDomain.EventBus.Publish(evnt);
        }

        /// <summary>
        /// 提交命令总线
        /// </summary>
        public static void CommitEventBus(this IAcDomain acDomain)
        {
            acDomain.EventBus.Commit();
        }

        /// <summary>
        /// 处理命令，发布并提交命令。
        /// </summary>
        /// <param name="acDomain"></param>
        /// <param name="command"></param>
        public static void Handle(this IAcDomain acDomain, IAnycmdCommand command)
        {
            acDomain.CommandBus.Publish(command);
            acDomain.CommandBus.Commit();
        }
        /// <summary>
        /// 从对象容器中返回给定.NET类型 <c>T</c> 的服务对象。
        /// 如果未找到将返回 <c>null</c>值.
        /// </summary>
        public static T GetService<T>(this IAcDomain container)
        {
            return (T)container.GetService(typeof(T));
        }

        /// <summary>
        /// 从对象容器中返回给定.NET类型 <c>T</c> 的服务对象。
        /// 如果未找到将抛出 <see cref="ServiceNotFoundException"/> 类型的异常。
        /// </summary>
        public static T GetRequiredService<T>(this IAcDomain container)
        {
            return (T)GetRequiredService(container, typeof(T));
        }

        /// <summary>
        /// 从对象容器中返回给定.NET类型 <paramref name="serviceType"/>  的服务对象。
        /// 如果未找到将抛出 <see cref="ServiceNotFoundException"/> 类型的异常。
        /// </summary>
        public static object GetRequiredService(this IAcDomain container, Type serviceType)
        {
            var service = container.GetService(serviceType);
            if (service == null)
                throw new ServiceNotFoundException(serviceType);

            return service;
        }

        /// <summary>
        /// 从容器中取出给定类型的服务。
        /// 如果服务未找到将返回null值。
        /// </summary>
        public static T RetrieveService<T>(this IAcDomain container)
        {
            return (T)container.GetService(typeof(T));
        }

        /// <summary>
        /// 从容器中取出给定类型的服务。
        /// 如果服务未找到将抛出<see cref="ServiceNotFoundException"/>类型异常。
        /// </summary>
        public static T RetrieveRequiredService<T>(this IAcDomain container)
        {
            return (T)RetrieveRequiredService(container, typeof(T));
        }

        /// <summary>
        /// 从容器中取出给定类型的服务。
        /// 如果服务未找到将抛出<see cref="ServiceNotFoundException"/>类型异常。
        /// </summary>
        public static object RetrieveRequiredService(this IAcDomain container, Type serviceType)
        {
            var service = container.GetService(serviceType);
            if (service == null)
                throw new ServiceNotFoundException(serviceType);

            return service;
        }
    }
}
