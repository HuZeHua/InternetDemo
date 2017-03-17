using FluentValidation.Attributes;
using XCode.Admin.Validators.Authen;
using XCode.Web.Core.Mvc;

namespace XCode.Admin.Models.Authen
{
    [Validator(typeof(RoleValidator))]
    public class RoleModel : BaseNextCMSModel
    {
        public RoleModel()
        {
            this.Active = true;
        }

        public string Name { get; set; }
        public string SystemName { get; set; }
        public bool Active { get; set; }
    }
}