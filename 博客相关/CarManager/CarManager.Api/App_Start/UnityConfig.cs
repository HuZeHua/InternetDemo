using CarManager.Core.Config;
using CarManager.Core.Infrastructure;
using CarManager.Core.Infrastucture;
using CarManager.WebCore.Infrastucture;
using Microsoft.Practices.Unity;
using System;
using System.Configuration;
using System.Web.Http;
using Unity.WebApi;

namespace CarManager.Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            RegisterTypes(container);

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterInstance(container);

            ITypeFinder typeFinder = new WebTypeFinder();

            var config = ConfigurationManager.GetSection("applicationConfig") as ApplicationConfig;

            container.RegisterInstance(config);

            var registerTypes = typeFinder.FindClassesOfType<IDependencyRegister>();

            foreach (Type registerType in registerTypes)
            {
                var register = (IDependencyRegister)Activator.CreateInstance(registerType);
                register.RegisterTypes(container);
            }
        }
    }
}