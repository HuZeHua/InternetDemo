using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace CarManager.Web.Mvc
{
    public class LanuageActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
           
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string lg = filterContext.RouteData.Values["lg"].ToString();
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lg);
            Thread.CurrentThread.CurrentCulture = new CultureInfo(lg);
        }
    }
}