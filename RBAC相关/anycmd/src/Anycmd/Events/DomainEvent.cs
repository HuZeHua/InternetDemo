
namespace Anycmd.Events
{
    using Model;
    using System;
    using Util;

    /// <summary>
    /// 表示所有领域事件的基类。
    /// 注意：该模型不应被序列化。
    /// </summary>
    public abstract class DomainEvent : IDomainEvent<Guid>
    {
        private readonly DateTime _timestamp;
        private readonly IEntity<Guid> _source;
        private readonly Guid _id;
        private readonly long _branch;
        private readonly long _version;
        private readonly string _assemblyQualifiedEventType;

        #region Ctor
        /// <summary>
        /// 以给定的实体作为事件源初始化一个 <c>DomainEvent</c> 类型的实例。
        /// </summary>
        /// <param name="source">The source entity which raises the domain event.</param>
        protected DomainEvent(IEntity<Guid> source)
            : this(null, source)
        {
        }

        /// <summary>
        /// 以给定的实体作为事件源初始化一个 <c>DomainEvent</c> 类型的实例。
        /// </summary>
        /// <param name="acSession"></param>
        /// <param name="source"></param>
        protected DomainEvent(IAcSession acSession, IEntity<Guid> source)
            : this(acSession, source, Guid.Empty, 0, 0, DateTime.MinValue, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="acSession">当初领域事件发生时，引发当前领域事件的主体。可以为null。</param>
        /// <param name="source">该事件的发生地。</param>
        /// <param name="id">事件标识。</param>
        /// <param name="version">事件版本号。</param>
        /// <param name="branck">事件所在的分枝。</param>
        /// <param name="timestamp">事件发生时的时间戳。</param>
        /// <param name="assemblyQualifiedEventType">该事件的应用程序集唯一.NET类型名称。</param>
        protected DomainEvent(IAcSession acSession, IEntity<Guid> source, Guid id, long version, long branck, DateTime timestamp, string assemblyQualifiedEventType)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            AcSession = acSession;
            _source = source;
            _id = id;
            _version = version;
            _branch = branck;
            _timestamp = timestamp;
            _assemblyQualifiedEventType = assemblyQualifiedEventType;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 返回当前领域事件的hash code。
        /// </summary>
        /// <returns>计算得到的当前领域事件的hash code值。</returns>
        public override int GetHashCode()
        {
            return ReflectionHelper.GetHashCode(Source.GetHashCode(),
                Branch.GetHashCode(),
                Id.GetHashCode(),
                Timestamp.GetHashCode(),
                Version.GetHashCode());
        }

        /// <summary>
        /// 返回一个 <see cref="System.Boolean"/> 类型的值，用来判定当前领域事件是否和给定的领域事件相同。
        /// </summary>
        /// <param name="obj">给定的领域事件。</param>
        /// <returns>True if obj is an instance of the <see cref="ISourcedAggregateRoot&lt;Guid&gt;"/> type and equals the value of this
        /// instance; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (obj == null)
                return false;
            var other = obj as Event<Guid>;
            if ((object)other == (object)null)
                return false;
            return this.Id == other.Id;
        }
        #endregion

        /// <summary>
        /// 当初领域事件发生时，引发当前领域事件的主体。
        /// </summary>
        /// <remarks>
        /// 可以为null
        /// </remarks>
        public IAcSession AcSession { get; private set; }

        #region IDomainEvent Members
        /// <summary>
        /// 读取当前领域事件的事件源。
        /// </summary>
        public IEntity<Guid> Source
        {
            get { return _source; }
        }

        /// <summary>
        /// 读取当前领域事件的版本号。
        /// </summary>
        public long Version
        {
            get { return _version; }
        }

        /// <summary>
        /// 读取当前领域实体所在的分枝。
        /// </summary>
        public long Branch
        {
            get { return _branch; }
        }

        /// <summary>
        /// 读取当前事件的程序集级唯一事件.NET类型名称，名称字符串中包括该事件类型所在的程序集的名称。
        /// </summary>
        public string AssemblyQualifiedEventType
        {
            get { return _assemblyQualifiedEventType; }
        }
        #endregion

        #region IEvent Members

        /// <summary>
        /// 读取当前领域事件的发生时间。
        /// </summary>
        /// <remarks>The format of this date/time value could be various between different
        /// systems. anycmd recommend system designer or architect uses the standard
        /// UTC date/time format.</remarks>
        public DateTime Timestamp
        {
            get { return _timestamp; }
        }
        #endregion

        #region IEntity Members
        /// <summary>
        /// 读取当前领域事件的标识值。
        /// </summary>
        public Guid Id
        {
            get { return _id; }
        }
        #endregion
    }
}
