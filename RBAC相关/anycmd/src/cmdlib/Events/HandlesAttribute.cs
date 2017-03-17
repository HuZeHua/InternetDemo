
namespace Anycmd.Events
{
    using System;

    /// <summary>
    /// 表示被装饰的方法是内联的领域事件处理器。
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class HandlesAttribute : Attribute
    {
        #region Public Properties
        /// <summary>
        /// 读取被该装饰特性装饰的领取事件处理器方法可以处理的事件的.NET类型。
        /// </summary>
        public Type DomainEventType { get; private set; }
        #endregion

        #region Ctor
        /// <summary>
        /// 初始化一个 <c>HandlesAttribute</c> 类的新实例。
        /// </summary>
        /// <param name="domainEventType">被该装饰特性装饰的领取事件处理器方法可以处理的事件的.NET类型。</param>
        public HandlesAttribute(Type domainEventType)
        {
            this.DomainEventType = domainEventType;
        }
        #endregion
    }
}
