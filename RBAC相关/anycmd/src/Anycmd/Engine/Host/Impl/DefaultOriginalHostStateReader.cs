﻿
namespace Anycmd.Engine.Host.Impl
{
    using Ac;
    using Ac.Identity;
    using Ac.Infra;
    using Ac.Rbac;
    using Engine.Ac.Privileges;
    using Engine.Rdb;
    using Repositories;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using Util;

    public class DefaultOriginalHostStateReader : IOriginalHostStateReader
    {
        /// <summary>
        /// 数据库连接字符串引导库连接字符串
        /// </summary>
        private readonly string _bootConnString = ConfigurationManager.AppSettings["BootDbConnString"];

        /// <summary>
        /// 数据库连接字符串引导库连接字符串
        /// </summary>
        public string BootConnString { get { return _bootConnString; } }

        private readonly IAcDomain _acDomain;

        public DefaultOriginalHostStateReader(IAcDomain acDomain)
        {
            this._acDomain = acDomain;
        }

        public IList<RDatabase> GetAllRDatabases()
        {
            return _acDomain.RetrieveRequiredService<IRdbMetaDataService>().GetDatabases();
        }

        public IList<DbTableColumn> GetTableColumns(RdbDescriptor db)
        {
            return _acDomain.RetrieveRequiredService<IRdbMetaDataService>().GetTableColumns(db);
        }

        public IList<DbTable> GetDbTables(RdbDescriptor db)
        {
            return _acDomain.RetrieveRequiredService<IRdbMetaDataService>().GetDbTables(db);
        }

        public IList<DbViewColumn> GetViewColumns(RdbDescriptor db)
        {
            return _acDomain.RetrieveRequiredService<IRdbMetaDataService>().GetViewColumns(db);
        }

        public IList<DbView> GetDbViews(RdbDescriptor db)
        {
            return _acDomain.RetrieveRequiredService<IRdbMetaDataService>().GetDbViews(db);
        }

        public IList<Catalog> GetCatalogs()
        {
            var repository = _acDomain.RetrieveRequiredService<IRepository<Catalog, Guid>>();
            using (var context = repository.Context)
            {
                return repository.AsQueryable().ToList();
            }
        }

        public IList<AppSystem> GetAllAppSystems()
        {
            var repository = _acDomain.RetrieveRequiredService<IRepository<AppSystem, Guid>>();
            using (var context = repository.Context)
            {
                return repository.AsQueryable().ToList();
            }
        }

        public IList<Button> GetAllButtons()
        {
            var repository = _acDomain.RetrieveRequiredService<IRepository<Button, Guid>>();
            using (var context = repository.Context)
            {
                return repository.AsQueryable().ToList();
            }
        }

        public IList<EntityType> GetAllEntityTypes()
        {
            var repository = _acDomain.RetrieveRequiredService<IRepository<EntityType, Guid>>();
            using (var context = repository.Context)
            {
                return repository.AsQueryable().ToList();
            }
        }

        public IList<Property> GetAllProperties()
        {
            var repository = _acDomain.RetrieveRequiredService<IRepository<Property, Guid>>();
            using (var context = repository.Context)
            {
                return repository.AsQueryable().ToList();
            }
        }

        public IList<Function> GetAllFunctions()
        {
            var repository = _acDomain.RetrieveRequiredService<IRepository<Function, Guid>>();
            using (var context = repository.Context)
            {
                return repository.AsQueryable().ToList();
            }
        }

        public IList<Group> GetAllGroups()
        {
            var repository = _acDomain.RetrieveRequiredService<IRepository<Group, Guid>>();
            using (var context = repository.Context)
            {
                return repository.AsQueryable().ToList();
            }
        }

        public IList<Menu> GetAllMenus()
        {
            var repository = _acDomain.RetrieveRequiredService<IRepository<Menu, Guid>>();
            using (var context = repository.Context)
            {
                return repository.AsQueryable().ToList();
            }
        }

        public IList<UiView> GetAllUiViews()
        {
            var repository = _acDomain.RetrieveRequiredService<IRepository<UiView, Guid>>();
            using (var context = repository.Context)
            {
                return repository.AsQueryable().ToList();
            }
        }

        public IList<UiViewButton> GetAllUiViewButtons()
        {
            var repository = _acDomain.RetrieveRequiredService<IRepository<UiViewButton, Guid>>();
            using (var context = repository.Context)
            {
                return repository.AsQueryable().ToList();
            }
        }

        public IList<Privilege> GetPrivileges()
        {
            var repository = _acDomain.RetrieveRequiredService<IRepository<Privilege, Guid>>();
            using (var context = repository.Context)
            {
                var subjectType = UserAcSubjectType.Account.ToName();
                return repository.AsQueryable().Where(a=>a.SubjectType != subjectType).ToList();
            }
        }

        public IList<Role> GetAllRoles()
        {
            var repository = _acDomain.RetrieveRequiredService<IRepository<Role, Guid>>();
            using (var context = repository.Context)
            {
                return repository.AsQueryable().ToList();
            }
        }

        public IList<SsdSet> GetAllSsdSets()
        {
            var repository = _acDomain.RetrieveRequiredService<IRepository<SsdSet, Guid>>();
            using (var context = repository.Context)
            {
                return repository.AsQueryable().ToList();
            }
        }

        public IList<DsdSet> GetAllDsdSets()
        {
            var repository = _acDomain.RetrieveRequiredService<IRepository<DsdSet, Guid>>();
            using (var context = repository.Context)
            {
                return repository.AsQueryable().ToList();
            }
        }

        public IList<SsdRole> GetAllSsdRoles()
        {
            var repository = _acDomain.RetrieveRequiredService<IRepository<SsdRole, Guid>>();
            using (var context = repository.Context)
            {
                return repository.AsQueryable().ToList();
            }
        }

        public IList<DsdRole> GetAllDsdRoles()
        {
            var repository = _acDomain.RetrieveRequiredService<IRepository<DsdRole, Guid>>();
            using (var context = repository.Context)
            {
                return repository.AsQueryable().ToList();
            }
        }

        public IList<Account> GetAllDevAccounts()
        {
            var repository = _acDomain.RetrieveRequiredService<IRepository<Account, Guid>>();
            using (var context = repository.Context)
            {
                return repository.Context.Query<DeveloperId>().Join(repository.Context.Query<Account>(), d => d.Id, a => a.Id, (d, a) => a).ToList();
            }
        }
    }
}
