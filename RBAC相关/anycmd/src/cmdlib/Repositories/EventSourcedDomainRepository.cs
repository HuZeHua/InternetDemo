
namespace Anycmd.Repositories
{
    using Bus;
    using Events;
    using Events.Storage;
    using Snapshots.Providers;
    using System.Linq;
    using System.Transactions;

    /// <summary>
    /// 表示支持事件溯源的领域仓储。
    /// </summary>
    public class EventSourcedDomainRepository<TAggregateRootId> : EventPublisherDomainRepository<TAggregateRootId>
    {
        #region Private Fields
        private readonly IDomainEventStorage<TAggregateRootId> _domainEventStorage;
        private readonly ISnapshotProvider<TAggregateRootId> _snapshotProvider;
        #endregion

        #region Ctor
        /// <summary>
        /// 初始化一个 <c>EventSourcedDomainRepository</c> 类实例。
        /// </summary>
        /// <param name="domainEventStorage">一个 <see cref="Anycmd.Events.Storage.IDomainEventStorage{T}"/> 
        /// 类型的实例，该实例是一种储存领域事件的装置。</param>
        /// <param name="eventBus">一个 <see cref="Anycmd.Bus.IEventBus"/> 类型的事件总线对象，领域事件发向该总线。</param>
        /// <param name="snapshotProvider"><see cref="Anycmd.Snapshots.Providers.ISnapshotProvider{T}"/> 
        /// 处理快照操作的快照提供程序。</param>
        public EventSourcedDomainRepository(IDomainEventStorage<TAggregateRootId> domainEventStorage, IEventBus eventBus, ISnapshotProvider<TAggregateRootId> snapshotProvider)
            : base(eventBus)
        {
            this._domainEventStorage = domainEventStorage;
            this._snapshotProvider = snapshotProvider;
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// 提交领域仓储工作单元中注册的所有变化。
        /// </summary>
        protected override void DoCommit()
        {
            // 首先我们通过领域事件仓储和事件总线保存并发布事件。
            foreach (var aggregateRoot in this.SaveHash)
            {
                if (this._snapshotProvider != null && this._snapshotProvider.Option == SnapshotProviderOption.Immediate)
                {
                    if (this._snapshotProvider.CanCreateOrUpdateSnapshot(aggregateRoot))
                    {
                        this._snapshotProvider.CreateOrUpdateSnapshot(aggregateRoot);
                    }
                }
                var events = aggregateRoot.UncommittedEvents;
                foreach (var evt in events)
                {
                    _domainEventStorage.SaveEvent(evt);
                    this.EventBus.Publish(evt);
                }
            }
            // // 然后提交工作单元，完成事件的保存和向事件总线的提交。
            if (this.DistributedTransactionSupported)
            {
                // the distributed transaction is supported either by domain event storage
                // or by the event bus. use the MS-DTC (Distributed Transaction Coordinator)
                // to commit the transaction. This solves the 2PC for deivces that are
                // distributed transaction compatible.
                using (var ts = new TransactionScope())
                {
                    _domainEventStorage.Commit();
                    this.EventBus.Commit();
                    if (this._snapshotProvider != null && this._snapshotProvider.Option == SnapshotProviderOption.Immediate)
                    {
                        this._snapshotProvider.Commit();
                    }
                    ts.Complete();
                }
            }
            else
            {
                _domainEventStorage.Commit();
                this.EventBus.Commit();
                if (this._snapshotProvider != null && this._snapshotProvider.Option == SnapshotProviderOption.Immediate)
                {
                    this._snapshotProvider.Commit();
                }
            }
        }
        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">A <see cref="System.Boolean"/> value which indicates whether
        /// the object should be disposed explicitly.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!this.Committed)
                {
                    try
                    {
                        this.Commit();
                    }
                    catch
                    {
                        this.Rollback();
                        throw;
                    }
                }
                _domainEventStorage.Dispose();
                if (_snapshotProvider != null)
                    _snapshotProvider.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// 读取用于存储事件的事件仓储对象。
        /// </summary>
        public IDomainEventStorage<TAggregateRootId> DomainEventStorage
        {
            get { return this._domainEventStorage; }
        }
        /// <summary>
        /// 读取用于处理快照操作的 <see cref="Anycmd.Snapshots.Providers.ISnapshotProvider{T}"/> 类型的快照对象。
        /// </summary>
        public ISnapshotProvider<TAggregateRootId> SnapshotProvider
        {
            get { return this._snapshotProvider; }
        }

        #endregion

        #region IDomainRepository Members
        /// <summary>
        /// 获取给定标识的聚合根对象。
        /// </summary>
        /// <param name="id">聚合根对象的标识。</param>
        /// <returns>具有更新标识值的聚合根对象。</returns>
        public override TAggregateRoot Get<TAggregateRoot>(TAggregateRootId id)
        {
            var aggregateRoot = this.CreateAggregateRootInstance<TAggregateRoot>();
            if (this._snapshotProvider != null && this._snapshotProvider.HasSnapshot(typeof(TAggregateRoot), id))
            {
                var snapshot = _snapshotProvider.GetSnapshot(typeof(TAggregateRoot), id);
                aggregateRoot.BuildFromSnapshot(snapshot);
                var eventsAfterSnapshot = this._domainEventStorage.LoadEvents(typeof(TAggregateRoot), id, snapshot.Version);
                var historicalEvents = eventsAfterSnapshot as IDomainEvent<TAggregateRootId>[] ?? eventsAfterSnapshot.ToArray();
                if (eventsAfterSnapshot != null && historicalEvents.Any())
                    aggregateRoot.BuildFromHistory(historicalEvents);
            }
            else
            {
                var evnts = this._domainEventStorage.LoadEvents(typeof(TAggregateRoot), id);
                var historicalEvents = evnts as IDomainEvent<TAggregateRootId>[] ?? evnts.ToArray();
                if (evnts != null && historicalEvents.Any())
                    aggregateRoot.BuildFromHistory(historicalEvents);
                else
                    throw new RepositoryException("没有找到给定标识的聚合根对象 (id={0})。", id);
            }
            return aggregateRoot;
        }
        #endregion

        #region IUnitOfWork Members
        /// <summary>
        /// 读取一个 <see cref="System.Boolean"/> 类型的值，这个值表示当前工作单元是否支持微软分布式事务协调器(MS-DTC)。
        /// <remarks>当前的领域事件仓储和事件总线都支持事务时才会返回true。</remarks>
        /// </summary>
        public override bool DistributedTransactionSupported
        {
            get { return _domainEventStorage.DistributedTransactionSupported && base.DistributedTransactionSupported; }
        }
        /// <summary>
        /// 回滚事务。
        /// </summary>
        public override void Rollback()
        {
            base.Rollback();
            _domainEventStorage.Rollback();
            if (this._snapshotProvider != null && this._snapshotProvider.Option == SnapshotProviderOption.Immediate)
            {
                this._snapshotProvider.Rollback();
            }
        }
        #endregion
    }
}
