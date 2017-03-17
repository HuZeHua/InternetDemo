
namespace Anycmd.Repositories
{
    using Model;
    using System;

    /// <summary>
    /// 表示该接口的实现类是领域仓储。
    /// </summary>
    public interface IDomainRepository<TAggregateRootId> : IUnitOfWork, IDisposable
    {
        /// <summary>
        /// 获取给定标识的聚合根对象。
        /// </summary>
        /// <param name="id">聚合根标识。</param>
        /// <returns>具有给定的标识的聚合根对象。</returns>
        TAggregateRoot Get<TAggregateRoot>(TAggregateRootId id)
            where TAggregateRoot : class, ISourcedAggregateRoot<TAggregateRootId>;

        /// <summary>
        /// 保存给定的聚合根对象到仓储。
        /// </summary>
        /// <param name="aggregateRoot">将被保存的聚合根对象。</param>
        void Save<TAggregateRoot>(TAggregateRoot aggregateRoot)
            where TAggregateRoot : class, ISourcedAggregateRoot<TAggregateRootId>;
    }
}
