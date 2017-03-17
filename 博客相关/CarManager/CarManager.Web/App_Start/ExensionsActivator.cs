using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FluentValidation;
using FluentValidation.Mvc;
using CarManager.Web.Validator;
using CarManager.Web.Mvc;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(CarManager.Web.App_Start.ExensionsActivator), "Start")]

namespace CarManager.Web.App_Start
{
    public static class ExensionsActivator
    {
        public static void Start()
        {
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            UntiyValidatorFactory untiyValidatorFactory = new UntiyValidatorFactory(UnityConfig.GetConfiguredContainer());
            ModelValidatorProviders.Providers.Insert(0,new FluentValidationModelValidatorProvider(untiyValidatorFactory));
            ModelMetadataProviders.Current = new CustomModelMetadataProvider();
        }
    }
}