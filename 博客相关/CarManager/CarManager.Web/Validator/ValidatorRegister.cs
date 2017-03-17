using CarManager.Core.Infrastucture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using FluentValidation;
using CarManager.Web.Properties;

namespace CarManager.Web.Validator
{
    public class ValidatorRegister : IDependencyRegister
    {
        public void RegisterTypes(IUnityContainer container)
        {
            var validatorTypes = this.GetType().Assembly.GetTypes().Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>)));

            //FluentValidation.Mvc.FluentValidationModelValidatorProvider.Configure();

            ValidatorOptions.ResourceProviderType = typeof(Resources);

            ValidatorOptions.DisplayNameResolver = (type, memberInfo, lambdaExpression) =>
            {
                string key = type.Name + memberInfo.Name + "DisplayName";
                string displayName = Resources.ResourceManager.GetString(key);

                return displayName;
            };

            foreach (Type type in validatorTypes)
            {
                container.RegisterType(typeof(IValidator<>),type,type.BaseType.GetGenericArguments().First().FullName,new ContainerControlledLifetimeManager());
            }
        }
    }
}