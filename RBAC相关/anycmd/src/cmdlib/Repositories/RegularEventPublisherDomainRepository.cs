
namespace Anycmd.Repositories
{
    using Bus;
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Transactions;

    /// <summary>
    /// Represents the domain repository that uses the <see cref="Anycmd.Repositories.IRepositoryContext"/>
    /// and <see cref="Anycmd.Repositories.IRepository{T1,T2}"/> instances to perform aggregate
    /// operations and publishes the domain events to the specified event bus.
    /// </summary>
    public class RegularEventPublisherDomainRepository<TAggregateRootId> : EventPublisherDomainRepository<TAggregateRootId>
    {
        #region Private Fields
        private readonly IRepositoryContext _context;
        private readonly HashSet<ISourcedAggregateRoot<TAggregateRootId>> _dirtyHash = new HashSet<ISourcedAggregateRoot<TAggregateRootId>>();
        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance of <c>RegularEventPublisherDomainRepository</c> class.
        /// </summary>
        /// <param name="context">The <see cref="Anycmd.Repositories.IRepositoryContext"/>instance
        /// that is used by the current domain repository to perform aggregate operations.</param>
        /// <param name="eventBus">The event bus to which the domain events are published.</param>
        public RegularEventPublisherDomainRepository(IRepositoryContext context, IEventBus eventBus)
            : base(eventBus)
        {
            this._context = context;
        }
        #endregion

        #region Private Methods
        private void PublishAggregateRootEvents(ISourcedAggregateRoot<TAggregateRootId> aggregateRoot)
        {
            foreach (var evt in aggregateRoot.UncommittedEvents)
            {
                this.EventBus.Publish(evt);
            }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Commits the changes registered in the domain repository.
        /// </summary>
        protected override void DoCommit()
        {
            foreach (var aggregateRootObj in this.SaveHash)
            {
                this._context.RegisterNew(aggregateRootObj);
                this.PublishAggregateRootEvents(aggregateRootObj);
            }
            foreach (var aggregateRootObj in this._dirtyHash)
            {
                this._context.RegisterModified(aggregateRootObj);
                this.PublishAggregateRootEvents(aggregateRootObj);
            }
            if (this.DistributedTransactionSupported)
            {
                using (var ts = new TransactionScope())
                {
                    this._context.Commit();
                    this.EventBus.Commit();
                    ts.Complete();
                }
            }
            else
            {
                this._context.Commit();
                this.EventBus.Commit();
            }
            this._dirtyHash.ToList().ForEach(this.DelegatedUpdateAndClearAggregateRoot);
            this._dirtyHash.Clear();
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
                this._context.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the <see cref="Anycmd.Repositories.IRepositoryContext"/>instance
        /// that is used by the current domain repository to perform aggregate operations.
        /// </summary>
        public IRepositoryContext Context
        {
            get { return this._context; }
        }
        #endregion

        #region IDomainRepository Members
        /// <summary>
        /// Gets the instance of the aggregate root with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier of the aggregate root.</param>
        /// <returns>The instance of the aggregate root with the specified identifier.</returns>
        public override TAggregateRoot Get<TAggregateRoot>(TAggregateRootId id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }
            var querySaveHash = from p in this.SaveHash
                                where p.Id.ToString() == id.ToString()
                                select p;
            var queryDirtyHash = from p in this._dirtyHash
                                 where p.Id.ToString() == id.ToString()
                                 select p;
            var sourcedAggregateRoots = querySaveHash as ISourcedAggregateRoot<TAggregateRootId>[] ?? querySaveHash.ToArray();
            if (sourcedAggregateRoots.Any())
                return sourcedAggregateRoots.FirstOrDefault() as TAggregateRoot;
            var aggregateRoots = queryDirtyHash as ISourcedAggregateRoot<TAggregateRootId>[] ?? queryDirtyHash.ToArray();
            if (aggregateRoots.Any())
                return aggregateRoots.FirstOrDefault() as TAggregateRoot;

            var result = _context.Query<TAggregateRoot>().FirstOrDefault(ar => ar.Id.ToString() == id.ToString());
            // Clears the aggregate root since version info is not needed in regular repositories.
            this.DelegatedUpdateAndClearAggregateRoot(result);
            return result;
        }
        /// <summary>
        /// Saves the aggregate represented by the specified aggregate root to the repository.
        /// </summary>
        /// <param name="aggregateRoot">The aggregate root that is going to be saved.</param>
        public override void Save<TAggregateRoot>(TAggregateRoot aggregateRoot)
        {
            if (_context.Query<TAggregateRoot>().Any(ar => ar.Id.ToString() == aggregateRoot.Id.ToString()))
            {
                if (!this._dirtyHash.Contains(aggregateRoot))
                    this._dirtyHash.Add(aggregateRoot);
                this.Committed = false;
            }
            else
            {
                base.Save(aggregateRoot);
            }
        }
        #endregion

        #region IUnitOfWork Members
        /// <summary>
        /// 读取一个 <see cref="System.Boolean"/> 类型的值，这个值表示当前工作单元是否支持微软分布式事务协调器(MS-DTC)。
        /// </summary>
        public override bool DistributedTransactionSupported
        {
            get
            {
                return this._context.DistributedTransactionSupported && base.DistributedTransactionSupported;
            }
        }
        /// <summary>
        /// 回滚事务。
        /// </summary>
        public override void Rollback()
        {
            base.Rollback();
            this._context.Rollback();
        }
        #endregion
    }
}
