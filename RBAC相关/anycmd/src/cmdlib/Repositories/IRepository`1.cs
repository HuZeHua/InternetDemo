﻿
namespace Anycmd.Repositories
{
    using Model;
    using System.Linq;

    /// <summary>
    /// 表示该接口的实现类是领域聚合根实体仓储。
    /// <remarks>一次操作至多影响一个实体的状态。</remarks>
    /// </summary>
    /// <typeparam name="TAggregateRoot">聚合根.NET对象类型。</typeparam>
    /// <typeparam name="TAggregateRootId">聚合根标识类型</typeparam>
    public interface IRepository<TAggregateRoot, in TAggregateRootId>
        where TAggregateRoot : class, IAggregateRoot<TAggregateRootId>
    {
        /// <summary>
        /// 获取该领域聚合根实体仓储依附的仓储上下文对象。
        /// </summary>
        IRepositoryContext Context { get; }

        /// <summary>
        /// 查询当前聚合根实体数据源。
        /// <returns>尽量不要使用这个方法，查询应使用Query命名空间下的事物。</returns>
        /// </summary>
        /// <returns>对聚合根实体集进行查询计算的接口</returns>
        IQueryable<TAggregateRoot> AsQueryable();

        /// <summary>
        /// 根据给定的标识从仓储中获取聚合根实体。
        /// </summary>
        /// <param name="key">聚合根实体的标识</param>
        /// <returns>聚合根实体对象。</returns>
        TAggregateRoot GetByKey(TAggregateRootId key);

        /// <summary>
        /// 添加一个聚合根实体对象到仓储。
        /// </summary>
        /// <param name="aggregateRoot">被添加到聚合根实体仓储的聚合根实体对象。</param>
        void Add(TAggregateRoot aggregateRoot);

        /// <summary>
        /// 更新当前聚合根实体仓储中的给定的聚合根对象。
        /// </summary>
        /// <param name="aggregateRoot">将被更新的聚合根实体对象。</param>
        void Update(TAggregateRoot aggregateRoot);

        /// <summary>
        /// 从仓储中移除给定的聚合根实体。
        /// </summary>
        /// <param name="aggregateRoot">将被移除的聚合根实体。</param>
        void Remove(TAggregateRoot aggregateRoot);
    }
}
