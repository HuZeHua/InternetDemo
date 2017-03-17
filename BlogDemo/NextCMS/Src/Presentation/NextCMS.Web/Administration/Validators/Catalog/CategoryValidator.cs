using FluentValidation;
using XCode.Admin.Models.Catalog;

namespace XCode.Admin.Validators.Catalog
{
    public class CategoryValidator : AbstractValidator<CategoryModel>
    {
        public CategoryValidator()
        {
            RuleFor(t => t.Name).NotEmpty().WithMessage("分类名称不能为空");
        }
    }
}