using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using CarManager.Core.Domain;
using CarManager.Web.Models.Cars;

namespace CarManager.Web.Mvc
{
    public class AutoMapperProfile : Profile
    {
        private const string MvcViewModelSuffixName = "ViewModel";

        public AutoMapperProfile()
        {
            var viewModelTypes = this.GetType().Assembly.GetTypes().Where(t => t.Name.EndsWith(MvcViewModelSuffixName));
            var domainTypes = typeof(BaseEntity).Assembly.GetTypes();

            foreach (Type modelType in viewModelTypes)
            {
                var viewModelTypeRelateDomainType = domainTypes.SingleOrDefault(dt => dt.Name + MvcViewModelSuffixName == modelType.Name);
                if (viewModelTypeRelateDomainType != null)
                {
                    this.CreateMap(modelType, viewModelTypeRelateDomainType);
                    this.CreateMap(viewModelTypeRelateDomainType, modelType).MaxDepth(10);
                }
            }
        }
    }
}