using CarManager.Core.Infrastucture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using AutoMapper;

namespace CarManager.Web.Mvc
{
    public class AtuoMapperRegister : IDependencyRegister
    {
        public void RegisterTypes(IUnityContainer container)
        {
            var profileTypes = this.GetType().Assembly.GetTypes().Where(t=>typeof(Profile).IsAssignableFrom(t));

            var profileInstances = profileTypes.Select(t=>(Profile)Activator.CreateInstance(t));

            var config = new MapperConfiguration(cfg=> { profileInstances.ToList().ForEach(p => cfg.AddProfile(p)); });

            container.RegisterInstance<MapperConfiguration>(config);
            container.RegisterInstance<IMapper>(config.CreateMapper());
        }
    }
}