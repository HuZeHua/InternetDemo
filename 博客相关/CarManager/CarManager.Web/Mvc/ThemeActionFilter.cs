using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarManager.Web.Mvc
{
    public class ThemeActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
          
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ThemeHelper.ChanageTheme(filterContext.RouteData.Values["theme"].ToString());
        }
    }
}