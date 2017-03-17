using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CarManager.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{lg}/{controller}/{action}/{id}",
            //    defaults: new { lg = "zh-cn", controller = "Car", action = "Index", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{page}",
                defaults: new { controller = "Car", action = "Index", page = UrlParameter.Optional }
            );


            //routes.MapRoute(
            //    name: "Default",
            //    url: "{theme}/{controller}/{action}/{id}",
            //    defaults: new { theme="red", controller = "Car", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
