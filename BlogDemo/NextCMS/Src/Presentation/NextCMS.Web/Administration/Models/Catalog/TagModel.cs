using FluentValidation.Attributes;
using XCode.Admin.Validators.Catalog;
using XCode.Web.Core.Mvc;

namespace XCode.Admin.Models.Catalog
{
    [Validator(typeof(TagValidator))]
    public partial class TagModel : BaseNextCMSModel
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}