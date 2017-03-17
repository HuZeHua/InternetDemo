
namespace Anycmd.Events
{
    using Model;
    using System;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;
    using Util;

    /// <summary>
    /// 表示所有事件的基类。
    /// </summary>
    [Serializable]
    public abstract class Event<TSourceId> : IDomainEvent<TSourceId>
    {
        private DateTime _timestamp;

        #region Ctor
        /// <summary>
        /// 初始化一个 <c>Event</c> 类型的对象。
        /// </summary>
        protected Event()
        {
            this._timestamp = DateTime.Now;
        }

        /// <summary>
        /// 初始化一个 <c>Event</c> 类型的对象。
        /// </summary>
        /// <param name="source">引发当前事件的实体对象。</param>
        protected Event(IEntity<TSourceId> source)
            : this()
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            this.Source = source;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Returns the hash code for current domain event.
        /// </summary>
        /// <returns>The calculated hash code for the current domain event.</returns>
        public override int GetHashCode()
        {
            return ReflectionHelper.GetHashCode(this.Source.GetHashCode(),
                this.Branch.GetHashCode(),
                this.Id.GetHashCode(),
                this.Timestamp.GetHashCode(),
                this.Version.GetHashCode());
        }

        /// <summary>
        /// Returns a <see cref="System.Boolean"/> value indicating whether this instance is equal to a specified
        /// entity.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>True if obj is an instance of the <see cref="ISourcedAggregateRoot{T}"/> type and equals the value of this
        /// instance; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (obj == null)
                return false;
            var other = obj as Event<TSourceId>;
            if ((object)other == (object)null)
                return false;
            return this.Id == other.Id;
        }
        #endregion

        #region IDomainEvent Members
        /// <summary>
        /// 读取或内部设置引发当前事件的实体对象。
        /// </summary>
        [XmlIgnore]
        [SoapIgnore]
        [IgnoreDataMember]
        public IEntity<TSourceId> Source { get; internal set; }

        /// <summary>
        ///读取或内部设置当前事件的版本。
        /// </summary>
        public virtual long Version { get; internal set; }

        /// <summary>
        /// 读取或内部设置当前事件所在的分支。
        /// </summary>
        public virtual long Branch { get; internal set; }

        /// <summary>
        /// 读取或设置当前事件的程序集级唯一事件类型。
        /// </summary>
        public virtual string AssemblyQualifiedEventType { get; internal set; }
        #endregion

        #region IEvent Members

        /// <summary>
        /// 读取或设置事件引发时的时间戳。
        /// </summary>
        /// <remarks>DateTime值的格式在不同的系统中可能不同。Anycmd推荐系统设计或架构者使用UTC标准时间。</remarks>
        public virtual DateTime Timestamp
        {
            get { return _timestamp; }
            internal set { _timestamp = value; }
        }
        #endregion

        #region IEntity Members
        /// <summary>
        /// 读取或内部设置当前事件的标识。
        /// </summary>
        public virtual Guid Id { get; internal set; }
        #endregion
    }
}
