using FluentValidation;
using XCode.Admin.Models.Authen;

namespace XCode.Admin.Validators.Authen
{
    public class RoleValidator: AbstractValidator<RoleModel>
    {
        public RoleValidator()
        {
            RuleFor(t => t.Name).NotEmpty().WithMessage("角色名称不能为空");
            RuleFor(t => t.SystemName).NotEmpty().WithMessage("系统角色不能为空");
        }
    }
}