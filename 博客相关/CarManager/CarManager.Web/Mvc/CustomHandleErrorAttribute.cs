using CarManager.Core.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Microsoft.Practices.Unity;
using CarManager.Core.Infrastucture;

namespace CarManager.Web.Mvc
{
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            var ex = filterContext.Exception;
            ILogger logger = ServiceContainer.Resolve<ILogger>();
            logger.Error("发现未处理的异常",ex);
        }
    }
}