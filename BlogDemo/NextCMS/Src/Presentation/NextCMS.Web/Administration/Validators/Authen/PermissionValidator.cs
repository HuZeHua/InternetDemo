using FluentValidation;
using XCode.Admin.Models.Authen;

namespace XCode.Admin.Validators.Authen
{
    public class PermissionValidator : AbstractValidator<PermissionModel>
    {
        public PermissionValidator()
        {
            RuleFor(t => t.Name).NotEmpty().WithMessage("权限名称");
            RuleFor(t => t.DisplayOrder).NotEmpty().WithMessage("排序不能为空");
        }
    }
}