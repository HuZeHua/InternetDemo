﻿
namespace Anycmd.Ef
{
    using Model;
    using Repositories;
    using System;
    using System.Data.Entity;
    using System.Linq;

    /// <summary>
    /// <returns>这一个泛型的实现就够了。因为IRepository接口是一个非常简单的接口，其中的每一次操作都至多影响一个实体，没有复杂的方法。</returns>
    /// </summary>
    /// <typeparam name="TAggregateRoot"></typeparam>
    /// <typeparam name="TAggregateRootId"></typeparam>
    public class GenericRepository<TAggregateRoot, TAggregateRootId> : Repository<TAggregateRoot, TAggregateRootId>
        where TAggregateRoot : class, IAggregateRoot<TAggregateRootId>
    {
        private readonly string _efDbContextName;
        private readonly IAcDomain _acDomain;

        public GenericRepository(IAcDomain acDomain, string efDbContextName)
        {
            if (acDomain == null)
            {
                throw new ArgumentNullException("acDomain");
            }
            this._acDomain = acDomain;
            this._efDbContextName = efDbContextName;
        }

        private EfRepositoryContext EfContext
        {
            get
            {
                var repositoryContext = Ef.EfContext.Storage.GetRepositoryContext(_efDbContextName);
                if (repositoryContext != null) return repositoryContext;
                repositoryContext = new EfRepositoryContext(_acDomain, _efDbContextName);
                Ef.EfContext.Storage.SetRepositoryContext(repositoryContext);
                return repositoryContext;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override IRepositoryContext Context
        {
            get
            {
                return EfContext;
            }
        }

        public RdbContext DbContext
        {
            get
            {
                return EfContext.DbContext;
            }
        }

        #region Protected Methods

        /// <summary>
        /// 查询当前聚合根实体数据源。
        /// </summary>
        /// <returns>对聚合根实体集进行查询计算的接口</returns>
        protected override IQueryable<TAggregateRoot> DoAsQueryable()
        {
            return DbContext.Set<TAggregateRoot>();
        }

        /// <summary>
        /// 根据给定的标识从仓储中获取聚合根实体。
        /// </summary>
        /// <param name="key">聚合根实体的标识</param>
        /// <returns>聚合根实体对象。</returns>
        protected override TAggregateRoot DoGetByKey(TAggregateRootId key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            return DbContext.Set<TAggregateRoot>().FirstOrDefault(p => p.Id.ToString() == key.ToString());
        }

        /// <summary>
        /// 添加一个聚合根实体对象到仓储。
        /// </summary>
        /// <param name="aggregateRoot">被添加到聚合根实体仓储的聚合根实体对象。</param>
        protected override void DoAdd(TAggregateRoot aggregateRoot)
        {
            EfContext.RegisterNew(aggregateRoot);
        }

        /// <summary>
        /// 更新当前聚合根实体仓储中的给定的聚合根对象。
        /// </summary>
        /// <param name="aggregateRoot">将被更新的聚合根实体对象。</param>
        protected override void DoUpdate(TAggregateRoot aggregateRoot)
        {
            EfContext.RegisterModified(aggregateRoot);
        }

        /// <summary>
        /// 从仓储中移除给定的聚合根实体。
        /// </summary>
        /// <param name="aggregateRoot">将被移除的聚合根实体。</param>
        protected override void DoRemove(TAggregateRoot aggregateRoot)
        {
            // TODO:区分标记删除
            EfContext.RegisterDeleted(aggregateRoot);
        }
        #endregion
    }
}
