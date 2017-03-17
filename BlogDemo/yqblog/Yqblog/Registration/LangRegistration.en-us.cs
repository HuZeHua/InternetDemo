using System.Web.Mvc;

namespace Yqblog
{
    public class EnUsRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "LangEnUS";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "en-us_Index",
                "en-us/{p}",
                new { controller = "Home", action = "Index", p = UrlParameter.Optional },
                new { p = "[0-9]*" }
            );

            context.MapRoute(
                "en-us_Rss",
                "en-us/rss/",
                new { controller = "Home", action = "Rss" }
            );

            context.MapRoute(
                "en-us_CommentRss",
                "en-us/commentrss/",
                new { controller = "Home", action = "CommentRss" }
            );

            context.MapRoute(
                "en-us_Category",
                "en-us/cate/{id}/{p}",
                new { controller = "Home", action = "Category", p = UrlParameter.Optional },
                new { id = "[0-9]+", p = "[0-9]*" }
            );

            context.MapRoute(
                "en-us_Article",
                "en-us/archive/{key}",
                new { controller = "Home", action = "ArticleByKey" },
                new { key = @"^[a-zA-Z0-9\-]+$" }
            );

            context.MapRoute(
                "en-us_Album",
                "en-us/album/{key}",
                new { controller = "Home", action = "AlbumByKey" },
                new { key = @"^[a-zA-Z0-9\-]+$" }
            );

            context.MapRoute(
                "en-us_Tag",
                "en-us/tag/{key}/{p}",
                new { controller = "Home", action = "Tag", p = UrlParameter.Optional },
                new { p = "[0-9]*" }
            );

            context.MapRoute(
                "en-us_User",
                "en-us/u/{user}",
                new { controller = "Account", action = "UView" }
            );

            context.MapRoute(
                "en-us_Search",
                "en-us/search/{key}/{p}",
                new { controller = "Home", action = "Search", p = UrlParameter.Optional },
                new { p = "[0-9]*" }
            );

            context.MapRoute(
                "en-us_Author",
                "en-us/author/{user}",
                new { controller = "Home", action = "Author" }
            );

            context.MapRoute(
                "en-us_Archives",
                "en-us/archives/{year}/{month}/{day}",
                new { controller = "Home", action = "Archives", month = UrlParameter.Optional, day = UrlParameter.Optional },
                new { year = "[0-9]+", month = "[0-9]*", day = "[0-9]*" }
            );

            context.MapRoute(
                "en-us_CategoryByKey",
                "en-us/{key}/{p}",
                new { controller = "Home", action = "CategoryByKey", p = UrlParameter.Optional },
                new { key = @"^[a-zA-Z0-9\-]+$", p = "[0-9]*" }
            );

            context.MapRoute(
                "en-us_Default",
                "en-us/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }

}