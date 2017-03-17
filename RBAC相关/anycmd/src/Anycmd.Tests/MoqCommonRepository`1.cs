
namespace Anycmd.Tests
{
    using Model;
    using Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class MoqCommonRepository<TAggregateRoot, TAggregateRootId> : IRepository<TAggregateRoot, TAggregateRootId>
        where TAggregateRoot : class, IAggregateRoot<TAggregateRootId>
    {
        private readonly MoqRepositoryContext _context;
        private readonly IAcDomain _acDomain;

        public MoqCommonRepository(IAcDomain acDomain)
        {
            this._acDomain = acDomain;
            _context = new MoqRepositoryContext(acDomain);
        }

        public IRepositoryContext Context
        {
            get { return _context; }
        }

        public IQueryable<TAggregateRoot> AsQueryable()
        {
            return Context.Query<TAggregateRoot>();
        }

        public TAggregateRoot GetByKey(TAggregateRootId key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            return Context.Query<TAggregateRoot>().FirstOrDefault(a => a.Id.ToString() == key.ToString());
        }

        public void Add(TAggregateRoot aggregateRoot)
        {
            Context.RegisterNew(aggregateRoot);
        }

        public void Remove(TAggregateRoot aggregateRoot)
        {
            Context.RegisterDeleted(aggregateRoot);
        }

        public void Update(TAggregateRoot aggregateRoot)
        {
            Context.RegisterModified(aggregateRoot);
        }
    }
    public class MoqRepositoryContext : RepositoryContext
    {
        private readonly Guid _id = Guid.NewGuid();

        private static readonly Dictionary<IAcDomain, Dictionary<Type, List<IAggregateRoot<Guid>>>>
            Data = new Dictionary<IAcDomain, Dictionary<Type, List<IAggregateRoot<Guid>>>>();
        private readonly IAcDomain _acDomain;

        public MoqRepositoryContext(IAcDomain acDomain)
        {
            this._acDomain = acDomain;
            if (!Data.ContainsKey(acDomain))
            {
                Data.Add(acDomain, new Dictionary<Type, List<IAggregateRoot<Guid>>>());
            }
        }

        public override void Commit()
        {
            lock (_acDomain)
            {
                foreach (var item in base.NewCollection.Keys)
                {
                    if (!Data[_acDomain].ContainsKey(item.GetType()))
                    {
                        Data[_acDomain].Add(item.GetType(), new List<IAggregateRoot<Guid>>());
                    }
                    if (Data[_acDomain][item.GetType()].Any(a => a.Id == ((IAggregateRoot<Guid>)item).Id))
                    {
                        throw new Exception();
                    }
                    Data[_acDomain][item.GetType()].Add((IAggregateRoot<Guid>)item);
                }
                foreach (var item in base.ModifiedCollection.Keys)
                {
                    Data[_acDomain][item.GetType()].Remove(Data[_acDomain][item.GetType()].First(a => a.Id == ((IAggregateRoot<Guid>)item).Id));
                    Data[_acDomain][item.GetType()].Add((IAggregateRoot<Guid>)item);
                }
                foreach (var item in DeletedCollection.Keys)
                {
                    Data[_acDomain][item.GetType()].Remove(Data[_acDomain][item.GetType()].First(a => a.Id == ((IAggregateRoot<Guid>)item).Id));
                }
                base.Committed = true;
                base.ClearRegistrations();
            }
        }

        public override Task CommitAsync(CancellationToken cancellationToken)
        {
            // TODO: This is a temp solution as the session and transaction
            // will be handled in different thread context, will try to 
            // find out a more robust solution.
            return Task.Factory.StartNew(Commit, cancellationToken);
        }

        public override void Rollback()
        {
            base.ClearRegistrations();
            base.Committed = false;
        }

        public override IQueryable<TEntity> Query<TEntity>()
        {
            return !Data[_acDomain].ContainsKey(typeof(TEntity))
                ? new List<TEntity>().AsQueryable() : Data[_acDomain][typeof(TEntity)].Cast<TEntity>().AsQueryable<TEntity>();
        }
    }
}
