using FluentValidation;
using XCode.Admin.Models.Catalog;

namespace XCode.Admin.Validators.Catalog
{
    public class ArticleValidator : AbstractValidator<ArticleModel>
    {
        public ArticleValidator()
        {
            RuleFor(t => t.Title).NotEmpty().WithMessage("标题不能为空");
            RuleFor(t => t.ShortDescription).NotEmpty().WithMessage("简短描述不能为空");
            RuleFor(t => t.FullDescription).NotEmpty().WithMessage("正文不能为空");
        }
    }
}