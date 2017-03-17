
namespace Anycmd.Repositories
{
    using Bus;

    /// <summary>
    /// 表示保存聚合时会向事件总线发布事件的领域仓储的基类。
    /// </summary>
    public abstract class EventPublisherDomainRepository<TAggregateRootId> : DomainRepository<TAggregateRootId>
    {
        #region Private Fields
        private readonly IEventBus _eventBus;
        #endregion

        #region Ctor
        /// <summary>
        /// 初始化一个 <c>EventPublisherDomainRepository</c> 类型的对象。
        /// </summary>
        /// <param name="eventBus">表示领域事件发布去向的 <see cref="Anycmd.Bus.IEventBus"/> 类型的事件总线。</param>
        protected EventPublisherDomainRepository(IEventBus eventBus)
        {
            this._eventBus = eventBus;
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">A <see cref="System.Boolean"/> value which indicates whether
        /// the object should be disposed explicitly.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _eventBus.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// 读取事件发布在的 <see cref="Anycmd.Bus.IEventBus"/> 类型的事件总线对象。
        /// </summary>
        public IEventBus EventBus
        {
            get { return this._eventBus; }
        }
        #endregion

        #region IUnitOfWork Members
        /// <summary>
        /// 读取一个 <see cref="System.Boolean"/> 类型的值，这个值表示当前工作单元是否支持微软分布式事务协调器(MS-DTC)。
        /// <remarks>当前的事件总线支持事务时返回true。</remarks>
        /// </summary>
        public override bool DistributedTransactionSupported
        {
            get { return this._eventBus.DistributedTransactionSupported; }
        }
        /// <summary>
        /// 回滚当前事务。
        /// </summary>
        public override void Rollback()
        {
            _eventBus.Rollback();
        }
        #endregion
    }
}
