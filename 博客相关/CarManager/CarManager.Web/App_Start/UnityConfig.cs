using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using CarManager.Core.Infrastucture;
using CarManager.Core.Infrastructure;
using CarManager.WebCore.Infrastucture;
using System.Configuration;
using CarManager.Core.Config;

namespace CarManager.Web.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            RegisterTypes(ServiceContainer.Current);
            return ServiceContainer.Current;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
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
