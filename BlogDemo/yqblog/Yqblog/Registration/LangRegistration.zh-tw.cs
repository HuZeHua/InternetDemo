using System.Web.Mvc;

namespace Yqblog
{
    public class ZhTwRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "LangZhTW";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "zh-tw_Index",
                "zh-tw/{p}",
                new { controller = "Home", action = "Index", p = UrlParameter.Optional },
                new { p = "[0-9]*" }
            );

            context.MapRoute(
                "zh-tw_Rss",
                "zh-tw/rss/",
                new { controller = "Home", action = "Rss" }
            );

            context.MapRoute(
                "zh-tw_CommentRss",
                "zh-tw/commentrss/",
                new { controller = "Home", action = "CommentRss" }
            );

            context.MapRoute(
                "zh-tw_Category",
                "zh-tw/cate/{id}/{p}",
                new { controller = "Home", action = "Category", p = UrlParameter.Optional },
                new { id = "[0-9]+", p = "[0-9]*" }
            );

            context.MapRoute(
                "zh-tw_Article",
                "zh-tw/archive/{key}",
                new { controller = "Home", action = "ArticleByKey" },
                new { key = @"^[a-zA-Z0-9\-]+$" }
            );

            context.MapRoute(
                "zh-tw_Album",
                "zh-tw/album/{key}",
                new { controller = "Home", action = "AlbumByKey" },
                new { key = @"^[a-zA-Z0-9\-]+$" }
            );

            context.MapRoute(
                "zh-tw_Tag",
                "zh-tw/tag/{key}/{p}",
                new { controller = "Home", action = "Tag", p = UrlParameter.Optional },
                new { p = "[0-9]*" }
            );

            context.MapRoute(
                "zh-tw_User",
                "zh-tw/u/{user}",
                new { controller = "Account", action = "UView" }
            );

            context.MapRoute(
                "zh-tw_Search",
                "zh-tw/search/{key}/{p}",
                new { controller = "Home", action = "Search", p = UrlParameter.Optional },
                new { p = "[0-9]*" }
            );

            context.MapRoute(
                "zh-tw_Author",
                "zh-tw/author/{user}",
                new { controller = "Home", action = "Author" }
            );

            context.MapRoute(
                "zh-tw_Archives",
                "zh-tw/archives/{year}/{month}/{day}",
                new { controller = "Home", action = "Archives", month = UrlParameter.Optional, day = UrlParameter.Optional },
                new { year = "[0-9]+", month = "[0-9]*", day = "[0-9]*" }
            );

            context.MapRoute(
                "zh-tw_CategoryByKey",
                "zh-tw/{key}/{p}",
                new { controller = "Home", action = "CategoryByKey", p = UrlParameter.Optional },
                new { key = @"^[a-zA-Z0-9\-]+$", p = "[0-9]*" }
            );

            context.MapRoute(
                "zh-tw_Default",
                "zh-tw/{controller}/{action}/{id}", 
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } 
            );
        }
    }

}