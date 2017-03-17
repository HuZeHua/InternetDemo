
namespace Anycmd.Repositories
{
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// 表示领域仓储的基类。
    /// </summary>
    public abstract class DomainRepository<TAggregateRootId> : DisposableObject, IDomainRepository<TAggregateRootId>
    {
        #region Private Fields
        private volatile bool _committed;
        private readonly HashSet<ISourcedAggregateRoot<TAggregateRootId>> _saveHash = new HashSet<ISourcedAggregateRoot<TAggregateRootId>>();
        private readonly Action<ISourcedAggregateRoot<TAggregateRootId>> _delegatedUpdateAndClearAggregateRoot = 
            ar => ar.GetType().GetMethod(SourcedAggregateRoot<TAggregateRootId>.UpdateVersionAndClearUncommittedEventsMethodName, BindingFlags.NonPublic | BindingFlags.Instance).Invoke(ar, null);
        #endregion

        #region Ctor
        /// <summary>
        /// 初始化一个 <c>DomainRepository</c> 类型的对象。
        /// </summary>
        protected DomainRepository()
        {
            this._committed = false;
        }
        #endregion

        #region Protected Properties
        /// <summary>
        /// 查看被保存的聚合根对象列表。
        /// </summary>
        protected HashSet<ISourcedAggregateRoot<TAggregateRootId>> SaveHash
        {
            get { return this._saveHash; }
        }

        /// <summary>
        /// 查看更新聚合根的版本号并清空它的未提交事件列表的委托方法。
        /// </summary>
        protected Action<ISourcedAggregateRoot<TAggregateRootId>> DelegatedUpdateAndClearAggregateRoot
        {
            get { return this._delegatedUpdateAndClearAggregateRoot; }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// 提交领域仓储中的变化。
        /// </summary>
        protected abstract void DoCommit();

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">A <see cref="System.Boolean"/> value which indicates whether
        /// the object should be disposed explicitly.</param>
        protected override void Dispose(bool disposing) { }

        /// <summary>
        /// 创建并返回一个给定类型的聚合根对象。注意：该类型的聚合根必须有无参构造函数。
        /// </summary>
        /// <typeparam name="TAggregateRoot">聚合根对象类型。</typeparam>
        /// <returns>给定类型的聚合根对象。</returns>
        /// <exception cref="RepositoryException">当给定类型的聚合根没有无参构造函数时引发。</exception>
        protected TAggregateRoot CreateAggregateRootInstance<TAggregateRoot>()
            where TAggregateRoot : class, ISourcedAggregateRoot<TAggregateRootId>
        {
            Type aggregateRootType = typeof(TAggregateRoot);
            ConstructorInfo constructor = aggregateRootType
                .GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(p =>
                {
                    var parameters = p.GetParameters();
                    return parameters.Length == 0;
                }).FirstOrDefault();
            if (constructor != null)
                return constructor.Invoke(null) as TAggregateRoot;
            throw new RepositoryException("'{0}'聚合根类必须至少定义一个无参构造函数。", typeof(TAggregateRoot));
        }
        #endregion

        #region IDomainRepository Members
        /// <summary>
        /// 获取给定标识的聚合根对象。
        /// </summary>
        /// <param name="id">聚合根标识。</param>
        /// <returns>给定类型的聚合根对象。</returns>
        public abstract TAggregateRoot Get<TAggregateRoot>(TAggregateRootId id)
            where TAggregateRoot : class, ISourcedAggregateRoot<TAggregateRootId>;

        /// <summary>
        /// 保存给定的聚合根对象进领域仓储。
        /// </summary>
        /// <param name="aggregateRoot">将被保存的聚合根对象。</param>
        public virtual void Save<TAggregateRoot>(TAggregateRoot aggregateRoot)
            where TAggregateRoot : class, ISourcedAggregateRoot<TAggregateRootId>
        {
            if (!_saveHash.Contains(aggregateRoot))
                _saveHash.Add(aggregateRoot);
            _committed = false;
        }
        #endregion

        #region IUnitOfWork Members
        /// <summary>
        /// 读取一个 <see cref="System.Boolean"/> 类型的值，这个值表示当前工作单元是否支持微软分布式事务协调器(MS-DTC)。
        /// </summary>
        public abstract bool DistributedTransactionSupported { get; }
        /// <summary>
        /// 读取一个 <see cref="System.Boolean"/> 类型的值，这个值表示当前工作单元是否已经成功提交。
        /// <remarks>True表示成功提交；False未成功提交。</remarks>
        /// </summary>
        public bool Committed
        {
            get { return this._committed; }
            protected set { this._committed = value; }
        }
        /// <summary>
        /// 提交事务。
        /// </summary>
        public void Commit()
        {
            this.DoCommit();
            this._saveHash.ToList().ForEach(this._delegatedUpdateAndClearAggregateRoot);
            this._saveHash.Clear();
            this._committed = true;
        }
        /// <summary>
        /// 回滚事务。
        /// </summary>
        public abstract void Rollback();
        #endregion
    }
}
