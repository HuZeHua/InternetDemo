using CarManager.Core.Infrastucture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using CarManager.Core.Data;

namespace CarManager.Data
{
    public class RepositoryRgister : IDependencyRegister
    {
        public void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IDbContext, CarDbContext>();
            container.RegisterType(typeof(IRepository<>),typeof(EfRepository<>));
        }
    }
}
