using CarManager.Core.Infrastucture;
using CarManager.Service.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarManager.Web.Mvc
{
    public class ActionAuthorzieAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException(nameof(filterContext));
            }

            bool skip = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);

            if (skip)
            {
                return;
            }

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(filterContext);
                return;
            }

            string controllName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string action = filterContext.ActionDescriptor.ActionName;

            var permissionService = ServiceContainer.Resolve<IPermissionService>();

            var allowCallAttribures = filterContext.ActionDescriptor.GetCustomAttributes(true).OfType<AllowCallAttribute>();

            string[] allowActions = allowCallAttribures.SelectMany(ac => ac.AllowActions).Distinct().ToArray();

            string userName = filterContext.HttpContext.User.Identity.Name;

            if (allowActions.Any(o => permissionService.Authorize(o, userName)))
            {
                return;
            }

            if (permissionService.Authorize(controllName + action, userName))
            {
                return;
            }

            HandleUnauthorizedRequest(filterContext);
        }
    }
}