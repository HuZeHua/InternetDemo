
namespace Anycmd.Repositories
{
    using Model;
    using System;
    using System.Linq;

    /// <summary>
    /// 表示该接口的实现类是仓储事务上下文。
    /// </summary>
    public interface IRepositoryContext : IUnitOfWork, IDisposable
    {
        /// <summary>
        /// 获取当前仓储上下文对象的唯一标识。
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// 往仓储上下文中注册一个新对象。
        /// </summary>
        /// <param name="obj">将被注册进仓储上下文中让仓储跟踪的对象。</param>
        void RegisterNew(object obj);

        /// <summary>
        /// 向仓储上下文中注册一个被修改的对象。
        /// </summary>
        /// <param name="obj">将被注册进仓储上下文中让仓储跟踪的对象。</param>
        void RegisterModified(object obj);

        /// <summary>
        /// 向仓储上下文中注册一个被删除的东西。
        /// </summary>
        /// <param name="obj">将被注册进仓储上下文中让仓储跟踪的对象。</param>
        void RegisterDeleted(object obj);

        /// <summary>
        /// 返回给定类型的实体对象集的查询器。
        /// <remarks>尽量少用或不用这个方法，“查询”应走专门的查询路径，不要走经过仓储的这条路径。</remarks>
        /// </summary>
        /// <typeparam name="TEntity">指定要查询的实体对象的类型。</typeparam>
        /// <returns>查询器。返回的结果对象流经了仓储上下文，结果对象的状态被仓储跟踪。</returns>
        IQueryable<TEntity> Query<TEntity>() where TEntity : class;
    }
}
