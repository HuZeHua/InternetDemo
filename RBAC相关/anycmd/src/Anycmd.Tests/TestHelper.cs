
namespace Anycmd.Tests
{
    using Model;
    using Moq;
    using Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class TestHelper
    {
        public static readonly Guid TestCatalogNodeId = new Guid("7C801EA5-05A8-4F16-A92C-BB89DDCA5A3D");

        public static IAcDomain GetAcDomain()
        {
            var acDomain = new MoqAcDomain().Init();

            return acDomain;
        }

        public static Mock<TRepository> GetMoqRepository<TEntity, TRepository>(this IAcDomain acDomain)
            where TEntity : class, IAggregateRoot<Guid>
            where TRepository : class, IRepository<TEntity, Guid>
        {

            var moRepository = new Mock<TRepository>();
            var context = new MoqRepositoryContext(acDomain);
            moRepository.Setup(a => a.Context).Returns(context);
            moRepository.Setup(a => a.Add(It.IsAny<TEntity>()));
            moRepository.Setup(a => a.Remove(It.IsAny<TEntity>()));
            moRepository.Setup(a => a.Update(It.IsAny<TEntity>()));
            moRepository.Setup(a => a.GetByKey(It.IsAny<Guid>())).Returns((TEntity)null);
            moRepository.Setup(a => a.AsQueryable()).Returns(new List<TEntity>().AsQueryable());

            return moRepository;
        }

        public static void RegisterRepository(this IAcDomain acDomain, params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                foreach (var aggregateRootType in assembly.GetTypes())
                {
                    if (aggregateRootType.IsClass && !aggregateRootType.IsAbstract)
                    {
                        if (typeof(IAggregateRoot<Guid>).IsAssignableFrom(aggregateRootType))
                        {
                            AddService(acDomain, aggregateRootType, typeof(Guid));
                        }
                        else if (typeof(IAggregateRoot<Int32>).IsAssignableFrom(aggregateRootType))
                        {
                            AddService(acDomain, aggregateRootType, typeof(Int32));
                        }
                        else if (typeof(IAggregateRoot<Int64>).IsAssignableFrom(aggregateRootType))
                        {
                            AddService(acDomain, aggregateRootType, typeof(Int64));
                        }
                        else if (typeof(IAggregateRoot<string>).IsAssignableFrom(aggregateRootType))
                        {
                            AddService(acDomain, aggregateRootType, typeof(string));
                        }
                    }
                }
            }
        }

        private static void AddService(IAcDomain acDomain, Type aggregateRootType, Type aggregateRootIdType)
        {
            var repositoryType = typeof(MoqCommonRepository<,>);
            var genericInterface = typeof(IRepository<,>);
            repositoryType = repositoryType.MakeGenericType(aggregateRootType, aggregateRootIdType);
            genericInterface = genericInterface.MakeGenericType(aggregateRootType, aggregateRootIdType);
            var repository = Activator.CreateInstance(repositoryType, acDomain);
            acDomain.AddService(genericInterface, repository);
        }
    }
}
