
namespace Anycmd.Events.Storage
{
    using Anycmd.Storage;
    using Model;
    using Serialization;
    using Specifications;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 表示基于关系数据库构建的领域仓库。
    /// </summary>
    /// <typeparam name="TRdbmsStorage">The type of the <c>RdbmsStorage</c> which provides
    /// the required operations on the external storage mechanism.</typeparam>
    /// <typeparam name="TSourceId"></typeparam>
    public abstract class RdbmsDomainEventStorage<TRdbmsStorage, TSourceId> : DisposableObject, IDomainEventStorage<TSourceId>
        where TRdbmsStorage : RdbmsStorage
    {
        #region Private Fields
        private readonly TRdbmsStorage _storage;
        private readonly string _connectionString;
        private readonly IStorageMappingResolver _mappingResolver;
        private readonly IDomainEventSerializer _domainEventSerializer;
        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <c>RdbmsDomainEventStorage&lt;TRdbmsStorage&gt;</c> class.
        /// </summary>
        /// <param name="domainEventSerializer"></param>
        /// <param name="connectionString">The connection string which is used when connecting
        /// to the relational database system. For more information about the connection strings
        /// for different database providers, please refer to http://www.connectionstrings.com.
        /// </param>
        /// <param name="mappingResolver">The instance of the mapping resolver which resolves the table and column mappings
        /// between data objects and the relational database system.</param>
        protected RdbmsDomainEventStorage(IDomainEventSerializer domainEventSerializer, string connectionString, IStorageMappingResolver mappingResolver)
        {
            try
            {
                this._domainEventSerializer = domainEventSerializer;
                this._connectionString = connectionString;
                this._mappingResolver = mappingResolver;
                Type storageType = typeof(TRdbmsStorage);
                _storage = (TRdbmsStorage)Activator.CreateInstance(storageType, new object[] { connectionString, mappingResolver });
            }
            catch
            {
                GC.SuppressFinalize(this);
                throw;
            }
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the connection string which is used when connecting
        /// to the relational database system. For more information about the connection strings
        /// for different database providers, please refer to http://www.connectionstrings.com.
        /// </summary>
        public string ConnectionString
        {
            get { return this._connectionString; }
        }
        /// <summary>
        /// Gets the instance of the mapping resolver which resolves the table and column mappings
        /// between data objects and relational database system.
        /// </summary>
        public IStorageMappingResolver MappingResolver
        {
            get { return this._mappingResolver; }
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
            if (!this.Committed)
                this.Commit();
            _storage.Dispose();
        }
        #endregion

        #region IDomainEventStorage Members
        /// <summary>
        /// Saves the specified domain event to the event storage.
        /// </summary>
        /// <param name="domainEvent">The domain event to be saved.</param>
        public void SaveEvent(IDomainEvent<TSourceId> domainEvent)
        {
            try
            {
                DomainEventDataObject<TSourceId> dataObject = _domainEventSerializer.FromDomainEvent(domainEvent);
                _storage.Insert<DomainEventDataObject<TSourceId>>(new PropertyBag(dataObject));
            }
            catch { throw; }
        }
        /// <summary>
        /// Loads all the domain events for the specific aggregate root from the storage.
        /// </summary>
        /// <param name="aggregateRootType">The type of the aggregate root.</param>
        /// <param name="id">The identifier of the aggregate root.</param>
        /// <returns>A list of domain events for the specific aggregate root.</returns>
        public IEnumerable<IDomainEvent<TSourceId>> LoadEvents(Type aggregateRootType, TSourceId id)
        {
            try
            {
                var sort = new PropertyBag();
                sort.AddSort<long>("Version");
                var aggregateRootTypeName = aggregateRootType.AssemblyQualifiedName;
                ISpecification<DomainEventDataObject<TSourceId>> specification = Specification<DomainEventDataObject<TSourceId>>.Eval(p => p.SourceId.Equals(id) && p.AssemblyQualifiedSourceType == aggregateRootTypeName);
                return _storage.Select<DomainEventDataObject<TSourceId>>(specification, sort, SortOrder.Ascending).Select(p => _domainEventSerializer.ToDomainEvent(p));
            }
            catch { throw; }
        }
        /// <summary>
        /// Loads all the domain events for the specific aggregate root from the storage.
        /// </summary>
        /// <param name="aggregateRootType">The type of the aggregate root.</param>
        /// <param name="id">The identifier of the aggregate root.</param>
        /// <param name="version">The version number.</param>
        /// <returns>A list of domain events for the specific aggregate root which occur just after
        /// the given version number.</returns>
        public IEnumerable<IDomainEvent<TSourceId>> LoadEvents(Type aggregateRootType, TSourceId id, long version)
        {
            var sort = new PropertyBag();
            sort.AddSort<long>("Version");
            var aggregateRootTypeName = aggregateRootType.AssemblyQualifiedName;
            ISpecification<DomainEventDataObject<TSourceId>> specification = Specification<DomainEventDataObject<TSourceId>>
                .Eval(p => p.SourceId.Equals(id) && p.AssemblyQualifiedSourceType == aggregateRootTypeName && p.Version > version);
            return _storage.Select<DomainEventDataObject<TSourceId>>(specification, sort, SortOrder.Ascending).Select(p => _domainEventSerializer.ToDomainEvent(p));
        }

        #endregion

        #region IUnitOfWork Members
        /// <summary>
        /// 读取一个 <see cref="System.Boolean"/> 类型的值，这个值表示当前工作单元是否支持微软分布式事务协调器(MS-DTC)。
        /// </summary>
        public virtual bool DistributedTransactionSupported
        {
            get
            {
                return _storage.DistributedTransactionSupported;
            }
        }
        /// <summary>
        /// Gets a <see cref="System.Boolean"/> value which indicates
        /// whether the 工作单元 was successfully committed.
        /// </summary>
        public bool Committed
        {
            get { return this._storage.Committed; }
        }
        /// <summary>
        /// Commits the transaction.
        /// </summary>
        public virtual void Commit()
        {
            _storage.Commit();
        }
        /// <summary>
        /// Rollback the transaction.
        /// </summary>
        public virtual void Rollback()
        {
            _storage.Rollback();
        }

        #endregion
    }
}
