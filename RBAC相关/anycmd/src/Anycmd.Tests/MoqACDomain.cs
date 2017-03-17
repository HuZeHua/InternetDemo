
namespace Anycmd.Tests
{
    using Engine.Ac.Accounts;
    using Engine.Host;
    using Engine.Host.Ac;
    using Engine.Host.Ac.Identity;
    using Engine.Host.Ac.Infra;
    using Engine.Host.Ac.Rbac;
    using Engine.Host.Impl;
    using Engine.Rdb;
    using Logging;
    using Moq;
    using Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MoqAcDomain : DefaultAcDomain
    {
        public override void Configure()
        {
            var acMethod = new DefaultAcSessionMethod();
            base.AcSessionFunctions = new DefaultAcSessionMethod(
                acMethod.SignIn, acMethod.SignOut, OnSignOuted, GetAccountById, GetAccountByLoginName
                , GetAcSessionEntity, acMethod.GetAcSession, AddAcSession, UpdateAcSession, DeleteAcSession);
            base.Configure();
            this.RegisterRepository(typeof(AcDomain).Assembly);
            AddService(typeof(ILoggingService), new Log4NetLoggingService(this));
            AddService(typeof(IAcSessionStorage), new SimpleAcSessionStorage());
            this.GetRequiredService<IRepository<Catalog, Guid>>().Add(new Catalog(Guid.NewGuid())
            {
                CategoryCode = "anycmd.catalog.Category.dic",
                Code = "anycmd.account.auditStatus",
                Name = "auditStatus1"
            });
            this.GetRequiredService<IRepository<Catalog, Guid>>().Add(new Catalog(Guid.NewGuid())
            {
                IsEnabled = 1,
                CategoryCode = "anycmd.catalog.Category.dicitem",
                ParentCode = "anycmd.account.auditStatus",
                SortCode = 0,
                Description = string.Empty,
                Code = "anycmd.account.auditStatus.auditPass",
                Name = "auditPass"
            });
            this.GetRequiredService<IRepository<Catalog, Guid>>().Add(new Catalog(TestHelper.TestCatalogNodeId)
            {
                Code = "test.Resource1",
                CategoryCode = "anycmd.catalog.Category.resourceType",
                Description = string.Empty,
                Name = "test.Resource1",
                SortCode = 10
            });
            this.GetRequiredService<IRepository<Catalog, Guid>>().Add(new Catalog(Guid.NewGuid())
            {
                CategoryCode = "anycmd.catalog.Category.dic",
                Code = "anycmd.rdatabase.rdbms",
                Name = "rdbms"
            });
            this.GetRequiredService<IRepository<Catalog, Guid>>().Add(new Catalog(Guid.NewGuid())
            {
                IsEnabled = 1,
                CategoryCode = "anycmd.catalog.Category.dicitem",
                ParentCode = "anycmd.rdatabase.rdbms",
                SortCode = 0,
                Description = string.Empty,
                Code = "anycmd.rdatabase.rdbms.sqlserver",
                Name = "sqlserver"
            });
            this.GetRequiredService<IRepository<Catalog, Guid>>().Context.Commit();
            var accountId = Guid.NewGuid();
            var passwordEncryptionService = this.RetrieveRequiredService<IPasswordEncryptionService>();
            this.GetRequiredService<IRepository<Account, Guid>>().Add(new Account(accountId)
            {
                LoginName = "test",
                Password = passwordEncryptionService.Encrypt("111111"),
                AuditState = "anycmd.account.auditStatus.auditPass",
                BackColor = string.Empty,
                AllowEndTime = null,
                AllowStartTime = null,
                AnswerQuestion = string.Empty,
                Description = string.Empty,
                FirstLoginOn = null,
                DeletionStateCode = 0,
                IpAddress = string.Empty,
                Lang = string.Empty,
                IsEnabled = 1,
                LastPasswordChangeOn = null,
                LockEndTime = null,
                LockStartTime = null,
                LoginCount = null,
                MacAddress = string.Empty,
                OpenId = string.Empty,
                PreviousLoginOn = null,
                NumberId = 10,
                Question = string.Empty,
                Theme = string.Empty,
                Wallpaper = string.Empty,
                SecurityLevel = 0,
                Code = "user1",
                CommunicationPassword = string.Empty,
                Email = string.Empty,
                Mobile = string.Empty,
                PublicKey = string.Empty,
                Qq = string.Empty,
                Name = "user1",
                QuickQuery = string.Empty,
                QuickQuery1 = string.Empty,
                QuickQuery2 = string.Empty,
                SignedPassword = string.Empty,
                Telephone = string.Empty,
                CatalogCode = string.Empty
            });
            this.GetRequiredService<IRepository<Account, Guid>>().Context.Commit();
            var appSystemId = Guid.NewGuid();
            this.GetRequiredService<IRepository<AppSystem, Guid>>().Add(new AppSystem(appSystemId)
            {
                Name = "test",
                Code = "test",
                PrincipalId = this.GetRequiredService<IRepository<Account, Guid>>().AsQueryable().First().Id
            });
            this.GetRequiredService<IRepository<AppSystem, Guid>>().Context.Commit();
            RemoveService(typeof(IOriginalHostStateReader));
            var moAcDomainBootstrap = new Mock<IOriginalHostStateReader>();
            moAcDomainBootstrap.Setup<IList<RDatabase>>(a => a.GetAllRDatabases()).Returns(new List<RDatabase>
            {
                new RDatabase
                {
                    Id=Guid.NewGuid(),
                    CatalogName="test",
                    DataSource=".",
                    Description="test",
                    IsTemplate=false,
                    Password=string.Empty,
                    Profile=string.Empty,
                    UserId=string.Empty,
                    RdbmsType="anycmd.rdatabase.rdbms.SqlServer",
                    ProviderName=string.Empty
                }
            });
            moAcDomainBootstrap.Setup<IList<DbTableColumn>>(a => a.GetTableColumns(It.IsAny<RdbDescriptor>())).Returns(new List<DbTableColumn>());
            moAcDomainBootstrap.Setup<IList<DbTable>>(a => a.GetDbTables(It.IsAny<RdbDescriptor>())).Returns(new List<DbTable>());
            moAcDomainBootstrap.Setup<IList<DbViewColumn>>(a => a.GetViewColumns(It.IsAny<RdbDescriptor>())).Returns(new List<DbViewColumn>());
            moAcDomainBootstrap.Setup<IList<DbView>>(a => a.GetDbViews(It.IsAny<RdbDescriptor>())).Returns(new List<DbView>());
            moAcDomainBootstrap.Setup<IList<Catalog>>(a => a.GetCatalogs()).Returns(this.GetRequiredService<IRepository<Catalog, Guid>>().AsQueryable().ToList());
            moAcDomainBootstrap.Setup<IList<AppSystem>>(a => a.GetAllAppSystems()).Returns(this.GetRequiredService<IRepository<AppSystem, Guid>>().AsQueryable().ToList());
            moAcDomainBootstrap.Setup<IList<Button>>(a => a.GetAllButtons()).Returns(this.GetRequiredService<IRepository<Button, Guid>>().AsQueryable().ToList());
            moAcDomainBootstrap.Setup<IList<EntityType>>(a => a.GetAllEntityTypes()).Returns(this.GetRequiredService<IRepository<EntityType, Guid>>().AsQueryable().ToList());
            moAcDomainBootstrap.Setup<IList<Property>>(a => a.GetAllProperties()).Returns(this.GetRequiredService<IRepository<Property, Guid>>().AsQueryable().ToList());
            moAcDomainBootstrap.Setup<IList<Function>>(a => a.GetAllFunctions()).Returns(this.GetRequiredService<IRepository<Function, Guid>>().AsQueryable().ToList());
            moAcDomainBootstrap.Setup<IList<Group>>(a => a.GetAllGroups()).Returns(this.GetRequiredService<IRepository<Group, Guid>>().AsQueryable().ToList());
            moAcDomainBootstrap.Setup<IList<Menu>>(a => a.GetAllMenus()).Returns(this.GetRequiredService<IRepository<Menu, Guid>>().AsQueryable().ToList());
            moAcDomainBootstrap.Setup<IList<UiView>>(a => a.GetAllUiViews()).Returns(this.GetRequiredService<IRepository<UiView, Guid>>().AsQueryable().ToList());
            moAcDomainBootstrap.Setup<IList<UiViewButton>>(a => a.GetAllUiViewButtons()).Returns(this.GetRequiredService<IRepository<UiViewButton, Guid>>().AsQueryable().ToList());
            moAcDomainBootstrap.Setup<IList<Privilege>>(a => a.GetPrivileges()).Returns(this.GetRequiredService<IRepository<Privilege, Guid>>().AsQueryable().ToList());
            moAcDomainBootstrap.Setup<IList<Role>>(a => a.GetAllRoles()).Returns(this.GetRequiredService<IRepository<Role, Guid>>().AsQueryable().ToList());
            moAcDomainBootstrap.Setup<IList<SsdSet>>(a => a.GetAllSsdSets()).Returns(this.GetRequiredService<IRepository<SsdSet, Guid>>().AsQueryable().ToList());
            moAcDomainBootstrap.Setup<IList<DsdSet>>(a => a.GetAllDsdSets()).Returns(this.GetRequiredService<IRepository<DsdSet, Guid>>().AsQueryable().ToList());
            moAcDomainBootstrap.Setup<IList<SsdRole>>(a => a.GetAllSsdRoles()).Returns(this.GetRequiredService<IRepository<SsdRole, Guid>>().AsQueryable().ToList());
            moAcDomainBootstrap.Setup<IList<DsdRole>>(a => a.GetAllDsdRoles()).Returns(this.GetRequiredService<IRepository<DsdRole, Guid>>().AsQueryable().ToList());
            moAcDomainBootstrap.Setup<IList<Account>>(a => a.GetAllDevAccounts()).Returns(this.GetRequiredService<IRepository<Account, Guid>>().AsQueryable().ToList());
            AddService(typeof(IOriginalHostStateReader), moAcDomainBootstrap.Object);
        }

        private static void OnSignOuted(IAcDomain acDomain, Guid sessionId)
        {
            if (EmptyAcDomain.SingleInstance.Equals(acDomain))
            {
                return;
            }
            var repository = acDomain.GetRequiredService<IRepository<AcSession, Guid>>();
            var entity = repository.GetByKey(sessionId);
            if (entity == null) return;
            entity.IsAuthenticated = false;
            repository.Update(entity);
        }

        private static IAcSessionEntity GetAcSessionEntity(IAcDomain acDomain, Guid acSessionId)
        {
            if (EmptyAcDomain.SingleInstance.Equals(acDomain))
            {
                return null;
            }
            return acDomain.GetRequiredService<IRepository<AcSession, Guid>>().GetByKey(acSessionId);
        }

        private static void AddAcSession(IAcDomain acDomain, IAcSessionEntity acSessionEntity)
        {
            if (EmptyAcDomain.SingleInstance.Equals(acDomain))
            {
                return;
            }
            var repository = acDomain.GetRequiredService<IRepository<AcSession, Guid>>();
            repository.Add((AcSession)acSessionEntity);
            repository.Context.Commit();
        }

        private static void UpdateAcSession(IAcDomain acDomain, IAcSessionEntity acSessionEntity)
        {
            if (EmptyAcDomain.SingleInstance.Equals(acDomain))
            {
                return;
            }
            var repository = acDomain.GetRequiredService<IRepository<AcSession, Guid>>();
            repository.Update(new AcSession(acSessionEntity.Id)
            {
                AuthenticationType = acSessionEntity.AuthenticationType,
                IsAuthenticated = acSessionEntity.IsAuthenticated,
                LoginName = acSessionEntity.LoginName,
                IsEnabled = acSessionEntity.IsEnabled,
                AccountId = acSessionEntity.AccountId,
                Description = acSessionEntity.Description
            });
            repository.Context.Commit();
        }

        private static void DeleteAcSession(IAcDomain acDomain, Guid acSessionId)
        {
            if (EmptyAcDomain.SingleInstance.Equals(acDomain))
            {
                return;
            }
            var repository = acDomain.GetRequiredService<IRepository<AcSession, Guid>>();
            var entity = repository.GetByKey(acSessionId);
            if (entity == null)
            {
                return;
            }
            repository.Remove(entity);
            repository.Context.Commit();
        }

        private static Account GetAccountById(IAcDomain acDomain, Guid accountId)
        {
            if (EmptyAcDomain.SingleInstance.Equals(acDomain))
            {
                return null;
            }
            var repository = acDomain.GetRequiredService<IRepository<Account, Guid>>();
            return repository.GetByKey(accountId);
        }

        private static Account GetAccountByLoginName(IAcDomain acDomain, string loginName)
        {
            if (EmptyAcDomain.SingleInstance.Equals(acDomain))
            {
                return null;
            }
            var repository = acDomain.GetRequiredService<IRepository<Account, Guid>>();
            return repository.AsQueryable().FirstOrDefault(a => a.LoginName == loginName);
        }
    }
}
