using CarManager.Core.Infrastucture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using CarManager.Core.Cache;
using CarManager.Core.Log;
using CarManager.Service.Security;

namespace CarManager.Service
{
    public class InfrastructureRegister : IDependencyRegister
    {
        public void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ICacheManager, NullCacheManager>();
            container.RegisterType<ILogger, NullLogger>();

            container.RegisterType<IPermissionService, PermissionService>();
        }
    }
}
