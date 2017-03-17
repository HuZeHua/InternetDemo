
namespace Anycmd.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 该类型的异常在ServiceManager找不到一个必须的服务对象时抛出。
    /// <remarks>
    /// ServiceManager通常是IoC框架。在Anycmd中是IServiceContainer，<see cref="IAcDomain"/>从IServiceContainer继承而来。
    /// </remarks>
    /// </summary>
    [Serializable()]
    public class ServiceNotFoundException : GeneralException
    {
        public ServiceNotFoundException()
            : base()
        {
        }

        public ServiceNotFoundException(Type serviceType)
            : base("Required service not found: " + serviceType.FullName)
        {
        }

        public ServiceNotFoundException(string message)
            : base(message)
        {
        }

        public ServiceNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ServiceNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
