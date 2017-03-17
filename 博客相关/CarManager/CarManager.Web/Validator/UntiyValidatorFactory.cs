using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Microsoft.Practices.Unity;

namespace CarManager.Web.Validator
{
    public class UntiyValidatorFactory : ValidatorFactoryBase
    {
        private readonly IUnityContainer unityContainer;

        public UntiyValidatorFactory(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            IValidator validator = null;

            try
            {
                validator= unityContainer.Resolve(validatorType, validatorType.GetGenericArguments().First().FullName) as IValidator;
            }
            catch 
            {
                validator = null;
            }

            return validator;
        }
    }

}