
namespace Anycmd.Repositories
{
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 表示领域仓储，该领域仓储使用 <see cref="Anycmd.Repositories.IRepositoryContext"/>
    /// 和 <see cref="Anycmd.Repositories.IRepository{T1,T2}"/> 实例完成领域仓储的操作。
    /// </summary>
    public class RegularDomainRepository<TAggregateRootId> : DomainRepository<TAggregateRootId>
    {
        #region Private Fields
        private readonly IRepositoryContext _context;
        private readonly HashSet<ISourcedAggregateRoot<TAggregateRootId>> _dirtyHash = new HashSet<ISourcedAggregateRoot<TAggregateRootId>>();
        #endregion

        #region Ctor
        /// <summary>
        /// 实例化一个 <c>RegularDomainRepository</c> 类的实例。
        /// </summary>
        /// <param name="context"><see cref="Anycmd.Repositories.IRepositoryContext"/> 实例，
        /// <c>RegularDomainRepository</c> 使用该对象完成领域仓储的操作。</param>
        public RegularDomainRepository(IRepositoryContext context)
        {
            this._context = context;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// 读取当前领域仓储内部使用的 <see cref="Anycmd.Repositories.IRepositoryContext"/> 实例。
        /// </summary>
        public IRepositoryContext Context
        {
            get { return this._context; }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// 提交领域仓储内注册的更新。
        /// </summary>
        protected override void DoCommit()
        {
            foreach (var aggregateRootObj in this.SaveHash)
            {
                this._context.RegisterNew(aggregateRootObj);
            }
            foreach (var aggregateRootObj in this._dirtyHash)
            {
                this._context.RegisterModified(aggregateRootObj);
            }

            this._context.Commit();

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

        #region IDomainRepository Members
        /// <summary>
        /// 获取给定标识的聚合根对象。
        /// </summary>
        /// <param name="id">聚合根对象的标识。</param>
        /// <returns>具有给定的标识值的聚合根对象。</returns>
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
        /// 保存给定的聚合根对象到仓储。
        /// </summary>
        /// <param name="aggregateRoot">将被保存的聚合根对象。</param>
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
            get { return _context.DistributedTransactionSupported; }
        }
        /// <summary>
        /// 回滚事务。
        /// </summary>
        public override void Rollback()
        {
            this._context.Rollback();
        }
        #endregion
    }
}
