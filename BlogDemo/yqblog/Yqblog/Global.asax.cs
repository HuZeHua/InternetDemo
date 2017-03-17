using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Globalization;
using System.Threading;
using Common;
using Yqblog.General;

namespace Yqblog
{
    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new BlogActionAttributeFilter()); 
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Index",
                "{p}",
                new { controller = "Home", action = "Index", p = UrlParameter.Optional },
                new { p = "[0-9]*" }
            );

            routes.MapRoute(
                "Rss",
                "rss/",
                new { controller = "Home", action = "Rss" }
            );

            routes.MapRoute(
                "CommentRss",
                "commentrss/",
                new { controller = "Home", action = "CommentRss" }
            );

            routes.MapRoute(
                "Category",
                "cate/{id}/{p}",
                new { controller = "Home", action = "Category", p = UrlParameter.Optional },
                new { id = "[0-9]+", p = "[0-9]*" }
            );

            routes.MapRoute(
                "Article",
                "archive/{key}",
                new { controller = "Home", action = "ArticleByKey" },
                new { key = @"^[a-zA-Z0-9\-]+$" }
            );

            routes.MapRoute(
                "Album",
                "album/{key}",
                new { controller = "Home", action = "AlbumByKey" },
                new { key = @"^[a-zA-Z0-9\-]+$" }
            );

            routes.MapRoute(
                "Tag",
                "tag/{key}/{p}",
                new { controller = "Home", action = "Tag", p = UrlParameter.Optional },
                new { p = "[0-9]*" }
            );

            routes.MapRoute(
                "User",
                "u/{user}",
                new { controller = "Account", action = "UView"}
            );

            routes.MapRoute(
                "Search",
                "search/{key}/{p}",
                new { controller = "Home", action = "Search", p = UrlParameter.Optional },
                new { p = "[0-9]*" }
            );


            routes.MapRoute(
                "Author",
                "author/{user}",
                new { controller = "Home", action = "Author" }
            );

            routes.MapRoute(
                "Archives",
                "archives/{year}/{month}/{day}",
                new { controller = "Home", action = "Archives", month = UrlParameter.Optional, day = UrlParameter.Optional },
                new { year = "[0-9]+", month = "[0-9]*", day = "[0-9]*" }
            );


            routes.MapRoute(
                "CategoryByKey",
                "{key}/{p}",
                new { controller = "Home", action = "CategoryByKey", p = UrlParameter.Optional },
                new { key = @"^[a-zA-Z0-9\-]+$", p = "[0-9]*" }
            );

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            var url = Request.Url.AbsoluteUri.ToLower();
            var files = new[] { ".axd", ".asmx", "/upload/", "/content/", ".ashx", ".css", ".js", ".png", ".gif", ".jpg", ".jpeg", ".bmp", ".swf", ".xml" };
            var currentfile = Array.Find(files,
                  element =>
                  url.IndexOf(element, StringComparison.Ordinal) >
                  -1);
            if (!string.IsNullOrEmpty(currentfile))
            {return;} 
            if (HttpContext.Current.Session == null) 
            {return;}

            var customTheme = (HttpContext.Current.Request["theme"] ?? string.Empty).Trim();

            if (!string.IsNullOrEmpty(customTheme) && customTheme != "default")
            {
                Session.Remove("CustomTheme");
                Session.Add("CustomTheme", customTheme);
                WebUtils.ChangeTheme(customTheme);
            }

            //set lang start
            var referer = HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];
            if (string.IsNullOrEmpty(referer))
            {
                DataCache.RemoveCache("CategoriesInfoCache");
                Session["CurrentLanguage"] = null;
            }
            var defaultlang = WebUtils.Configinfo.DefaultLang;
            var currentlang = (Session["CurrentLanguage"] != null
                                  ? Session["CurrentLanguage"].ToString()
                                  : defaultlang).ToLower();

            var lang = currentlang == "" ? defaultlang : currentlang;
            if (!url.Contains("/admin/") || url.Contains("u/admin"))
            {
                lang = Array.Find(WebUtils.Langs,
                                  element =>
                                  url.IndexOf(element, StringComparison.Ordinal) >
                                  -1);
                lang = string.IsNullOrWhiteSpace(lang) ? defaultlang : lang.Trim('/');
            }

            var ci = (CultureInfo)Session["CurrentLanguage"];

            if (ci == null || lang != currentlang || string.IsNullOrEmpty(referer))
            {
                ci = new CultureInfo(lang == "zh-cn" ? "" : lang);
                Session["CurrentLanguage"] = ci;

                if (Session["CustomTheme"]==null)
                {
                    WebUtils.ChangeThemeViaLang(lang);
                }
            }
            //set lang end

            if (customTheme == "default")
            {
                Session.Remove("CustomTheme");
                WebUtils.ChangeThemeViaLang(lang);
            }

            var currentThemeFolder = string.Empty;
            var rve = (BlogViewEngine)ViewEngines.Engines.FirstOrDefault(a => a.GetType() == typeof(BlogViewEngine));
            if (rve != null && rve.PartialViewLocationFormats != null && rve.PartialViewLocationFormats.Any())
            {
                currentThemeFolder = rve.PartialViewLocationFormats[0].StartsWith("~/Views/") ? "" : rve.PartialViewLocationFormats[0].Split('/')[2];
            }

            if (Session["CustomTheme"] != null)
            {
                if (currentThemeFolder != Session["CustomTheme"].ToString())
                {
                    WebUtils.ChangeTheme(Session["CustomTheme"].ToString());
                }
            }
            else
            {
                var theme = WebUtils.GetLangTemplate(lang);
                if (currentThemeFolder != theme)
                {
                    WebUtils.ChangeTheme(theme);
                }
            }

            Thread.CurrentThread.CurrentUICulture = ci;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            WebUtils.ChangeThemeViaLang(WebUtils.Configinfo.DefaultLang);
        }
    }
}