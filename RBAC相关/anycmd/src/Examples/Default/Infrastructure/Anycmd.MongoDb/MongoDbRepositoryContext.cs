
namespace Anycmd.MongoDb
{
    using Conventions;
    using MongoDB.Bson.Serialization.Conventions;
    using MongoDB.Driver;
    using MongoDB.Driver.Linq;
    using Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    public class MongoDbRepositoryContext : RepositoryContext, IMongoDbRepositoryContext
    {
        #region Private Fields
        private readonly Guid id = Guid.NewGuid();
        private readonly MongoServer server;
        private readonly MongoDatabase database;
        private readonly IMongoDbRepositoryContextSettings settings;
        private readonly object syncObj = new object();
        private readonly Dictionary<Type, MongoCollection> mongoCollections = new Dictionary<Type, MongoCollection>();
        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance of <c>MongoDbRepositoryContext</c> class.
        /// </summary>
        /// <param name="settings">The <see cref="IMongoDbRepositoryContextSettings"/> instance which contains
        /// the setting information for initializing the repository context.</param>
        public MongoDbRepositoryContext(IMongoDbRepositoryContextSettings settings)
        {
            this.settings = settings;
            server = new MongoServer(settings.ServerSettings);
            database = server.GetDatabase(settings.DatabaseName, settings.GetDatabaseSettings(server));
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
            if (disposing)
            {
                server.Disconnect();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Public Static Methods
        /// <summary>
        /// Registers the MongoDB Bson serialization conventions.
        /// </summary>
        /// <param name="autoGenerateId">A <see cref="Boolean"/> value which indicates whether
        /// the ID value should be automatically generated when a new document is inserting.</param>
        /// <param name="localDateTime">A <see cref="Boolean"/> value which indicates whether
        /// the local date/time should be used when serializing/deserializing <see cref="DateTime"/> values.</param>
        public static void RegisterConventions(bool autoGenerateId = true, bool localDateTime = true)
        {
            RegisterConventions(autoGenerateId, localDateTime, null);
        }
        /// <summary>
        /// Registers the MongoDB Bson serialization conventions.
        /// </summary>
        /// <param name="autoGenerateId">A <see cref="Boolean"/> value which indicates whether
        /// the ID value should be automatically generated when a new document is inserting.</param>
        /// <param name="localDateTime">A <see cref="Boolean"/> value which indicates whether
        /// the local date/time should be used when serializing/deserializing <see cref="DateTime"/> values.</param>
        /// <param name="additionConventions">Additional conventions that needs to be registered.</param>
        public static void RegisterConventions(bool autoGenerateId, bool localDateTime, IEnumerable<IConvention> additionConventions)
        {
            var conventionPack = new ConventionPack();
            conventionPack.Add(new NamedIdMemberConvention("id", "Id", "ID", "iD"));
            if (autoGenerateId)
                conventionPack.Add(new GuidIdGeneratorConvention());
            if (localDateTime)
                conventionPack.Add(new UseLocalDateTimeConvention());
            if (additionConventions != null)
                conventionPack.AddRange(additionConventions);

            ConventionRegistry.Register("DefaultConvention", conventionPack, t => true);
        }

        #endregion

        #region IMongoDbRepositoryContext Members
        /// <summary>
        /// Gets a <see cref="IMongoDbRepositoryContextSettings"/> instance which contains the settings
        /// information used by current context.
        /// </summary>
        public IMongoDbRepositoryContextSettings Settings
        {
            get { return settings; }
        }
        /// <summary>
        /// Gets the <see cref="MongoCollection"/> instance by the given <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> object.</param>
        /// <returns>The <see cref="MongoCollection"/> instance.</returns>
        public MongoCollection GetCollectionForType(Type type)
        {
            lock (syncObj)
            {
                if (this.mongoCollections.ContainsKey(type))
                    return this.mongoCollections[type];
                else
                {
                    MongoCollection mongoCollection = null;
                    if (settings.MapTypeToCollectionName != null)
                        mongoCollection = this.database.GetCollection(settings.MapTypeToCollectionName(type));
                    else
                        mongoCollection = this.database.GetCollection(type.Name);
                    this.mongoCollections.Add(type, mongoCollection);
                    return mongoCollection;
                }
            }
        }
        #endregion

        #region IUnitOfWork Members
        /// <summary>
        /// Commits the transaction.
        /// </summary>
        public override void Commit()
        {
            lock (syncObj)
            {
                foreach (var newObj in this.NewCollection.Keys)
                {
                    MongoCollection collection = this.GetCollectionForType(newObj.GetType());
                    collection.Insert(newObj);
                }
                foreach (var modifiedObj in this.ModifiedCollection.Keys)
                {
                    MongoCollection collection = this.GetCollectionForType(modifiedObj.GetType());
                    collection.Save(modifiedObj);
                }
                foreach (var delObj in this.DeletedCollection.Keys)
                {
                    Type objType = delObj.GetType();
                    PropertyInfo propertyInfo = objType.GetProperty("ID", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                    if (propertyInfo == null)
                        throw new InvalidOperationException("Cannot delete an object which doesn't contain an ID property.");
                    Guid id = (Guid)propertyInfo.GetValue(delObj, null);
                    MongoCollection collection = this.GetCollectionForType(objType);
                    IMongoQuery query = MongoDB.Driver.Builders.Query.EQ("_id", id);
                    collection.Remove(query);
                }
                this.ClearRegistrations();
                this.Committed = true;
            }
        }

        public override Task CommitAsync(CancellationToken cancellationToken)
        {
            // TODO: This is a temp solution as the session and transaction
            // will be handled in different thread context, will try to 
            // find out a more robust solution.
            return Task.Factory.StartNew(Commit, cancellationToken);
        }
        /// <summary>
        /// Rollback the transaction.
        /// </summary>
        public override void Rollback()
        {
            this.Committed = false;
        }

        #endregion

        public override IQueryable<TEntity> Query<TEntity>()
        {
            MongoCollection collection = this.GetCollectionForType(typeof(TEntity));
            return collection.AsQueryable<TEntity>();
        }
    }
}
