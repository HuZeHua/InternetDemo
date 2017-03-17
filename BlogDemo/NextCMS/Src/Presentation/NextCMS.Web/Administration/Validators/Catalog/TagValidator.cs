using FluentValidation;
using XCode.Admin.Models.Catalog;

namespace XCode.Admin.Validators.Catalog
{
    public class TagValidator: AbstractValidator<TagModel>
    {
        public TagValidator()
        {
            RuleFor(t => t.Name).NotEmpty().WithMessage("标签名称不能为空");
        }
    }
}