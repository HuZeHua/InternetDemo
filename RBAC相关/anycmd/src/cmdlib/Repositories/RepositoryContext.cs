
namespace Anycmd.Repositories
{
    using Model;
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the repository context.
    /// </summary>
    public abstract class RepositoryContext : DisposableObject, IRepositoryContext
    {
        #region Private Fields
        private readonly Guid _id = Guid.NewGuid();
        private readonly ConcurrentDictionary<object, byte> _newCollection = new ConcurrentDictionary<object, byte>();
        private readonly ConcurrentDictionary<object, byte> _modifiedCollection = new ConcurrentDictionary<object, byte>();
        private readonly ConcurrentDictionary<object, byte> _deletedCollection = new ConcurrentDictionary<object, byte>();
        private volatile bool _committed = true;
        #endregion

        #region Protected Properties
        /// <summary>
        /// Gets an enumerator which iterates over the collection that contains all the objects need to be added to the repository.
        /// </summary>
        protected ConcurrentDictionary<object, byte> NewCollection
        {
            get { return _newCollection; }
        }
        /// <summary>
        /// Gets an enumerator which iterates over the collection that contains all the objects need to be modified in the repository.
        /// </summary>
        protected ConcurrentDictionary<object, byte> ModifiedCollection
        {
            get { return _modifiedCollection; }
        }
        /// <summary>
        /// Gets an enumerator which iterates over the collection that contains all the objects need to be deleted from the repository.
        /// </summary>
        protected ConcurrentDictionary<object, byte> DeletedCollection
        {
            get { return _deletedCollection; }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Clears all the registration in the repository context.
        /// </summary>
        /// <remarks>Note that this can only be called after the repository context has successfully committed.</remarks>
        protected void ClearRegistrations()
        {
            _newCollection.Clear();
            _modifiedCollection.Clear();
            _deletedCollection.Clear();
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
                ClearRegistrations();
            }
        }
        #endregion

        #region IRepositoryContext Members
        /// <summary>
        /// Gets the Id of the repository context.
        /// </summary>
        public Guid Id
        {
            get { return _id; }
        }
        /// <summary>
        /// Registers a new object to the repository context.
        /// </summary>
        /// <param name="obj">The object to be registered.</param>
        public virtual void RegisterNew(object obj)
        {
            //if (localModifiedCollection.Value.Contains(obj))
            //   throw new InvalidOperationException("The object cannot be registered as a new object since it was marked as modified.");
            //if (localNewCollection.Value.Contains(obj))
            //    throw new InvalidOperationException("The object has already been registered as a new object.");

            //localNewCollection.Value.Add(obj);

            _newCollection.AddOrUpdate(obj, byte.MinValue, (o, b) => byte.MinValue);
            Committed = false;
        }
        /// <summary>
        /// Registers a modified object to the repository context.
        /// </summary>
        /// <param name="obj">The object to be registered.</param>
        public virtual void RegisterModified(object obj)
        {
            //if (localDeletedCollection.Value.Contains(obj))
            //    throw new InvalidOperationException("The object cannot be registered as a modified object since it was marked as deleted.");
            //if (!localModifiedCollection.Value.Contains(obj) && !localNewCollection.Value.Contains(obj))
            //    localModifiedCollection.Value.Add(obj);

            if (_deletedCollection.ContainsKey(obj))
                throw new InvalidOperationException("The object cannot be registered as a modified object since it was marked as deleted.");
            if (!_modifiedCollection.ContainsKey(obj) && !(_newCollection.ContainsKey(obj)))
                _modifiedCollection.AddOrUpdate(obj, byte.MinValue, (o, b) => byte.MinValue);

            Committed = false;
        }
        /// <summary>
        /// Registers a deleted object to the repository context.
        /// </summary>
        /// <param name="obj">The object to be registered.</param>
        public virtual void RegisterDeleted(object obj)
        {
            //if (localNewCollection.Value.Contains(obj))
            //{
            //    if (localNewCollection.Value.Remove(obj))
            //        return;
            //}
            //bool removedFromModified = localModifiedCollection.Value.Remove(obj);
            //bool addedToDeleted = false;
            //if (!localDeletedCollection.Value.Contains(obj))
            //{
            //    localDeletedCollection.Value.Add(obj);
            //    addedToDeleted = true;
            //}
            //localCommitted.Value = !(removedFromModified || addedToDeleted);
            var @byte = byte.MinValue;
            if (_newCollection.ContainsKey(obj))
            {
                _newCollection.TryRemove(obj, out @byte);
                return;
            }
            var removedFromModified = _modifiedCollection.TryRemove(obj, out @byte);
            var addedToDeleted = false;
            if (!_deletedCollection.ContainsKey(obj))
            {
                _deletedCollection.AddOrUpdate(obj, byte.MinValue, (o, b) => byte.MinValue);
                addedToDeleted = true;
            }
            _committed = !(removedFromModified || addedToDeleted);
        }
        #endregion

        #region IUnitOfWork Members
        /// <summary>
        /// 读取一个 <see cref="System.Boolean"/> 类型的值，这个值表示当前工作单元是否支持微软分布式事务协调器(MS-DTC)。
        /// </summary>
        public virtual bool DistributedTransactionSupported
        {
            get { return false; }
        }
        /// <summary>
        /// Gets a <see cref="System.Boolean"/> value which indicates
        /// whether the 工作单元 was successfully committed.
        /// </summary>
        public virtual bool Committed
        {
            get { return _committed; }
            protected set { _committed = value; }
        }
        /// <summary>
        /// Commits the transaction.
        /// </summary>
        public abstract void Commit();

        public Task CommitAsync()
        {
            return CommitAsync(CancellationToken.None);
        }

        public abstract Task CommitAsync(CancellationToken cancellationToken);
        /// <summary>
        /// Rollback the transaction.
        /// </summary>
        public abstract void Rollback();
        #endregion

        public abstract IQueryable<TEntity> Query<TEntity>() where TEntity : class;
    }
}
