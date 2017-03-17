using System.Linq;
using System.Web.Mvc;
using Ninject;
using Yqblog.Services;

namespace Yqblog.General
{
    public class BlogActionAttributeFilter: ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
            if (filterContext.Result is PartialViewResult || filterContext.Result is ViewResult)
            {
                filterContext.Controller.ViewBag.CI = WebUtils.Configinfo;
            }
        }
    }

    public class WebFilter : ActionFilterAttribute
    {
        [Inject]
        public IServices MyService { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            filterContext.Controller.ViewData["CategoriesInfo"] = MyService.GetCategories().Where(x => x.Status == 1).ToList();
            if (!(filterContext.Result is ViewResult)) return;
            var lst = MyService.GetFCategoryList("8", "");

            foreach (var varticle in lst.Select(category => MyService.GetVArticles(0, category.CateId, 0)).Where(re => re.Any()).SelectMany(re => re))
            {
                filterContext.Controller.ViewData["Global_" + varticle.title] = varticle.content;
            }
            filterContext.Controller.ViewBag.NavInfo = MyService.GetCategories().Where(x => x.Status == 1 && x.IsNav).ToList();
        }
    }
}