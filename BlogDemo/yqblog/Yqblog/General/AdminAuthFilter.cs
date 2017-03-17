using System.Web;
using System.Web.Mvc;
using Yqblog.Data;

namespace Yqblog.General
{
    public class AdminAuthFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["user"] as blog_users == null || (HttpContext.Current.Session["user"] as blog_users).blog_roles.rolename != "admin")
            {
                filterContext.Result = new RedirectResult("~/account/logon", true);
            }
            base.OnActionExecuting(filterContext);
        }
    }
}