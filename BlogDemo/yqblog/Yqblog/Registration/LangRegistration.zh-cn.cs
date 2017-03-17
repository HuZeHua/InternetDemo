using System.Web.Mvc;

namespace Yqblog
{
    public class ZhCnRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "LangZhCN";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "zh-cn_Index",
                "zh-cn/{p}",
                new { controller = "Home", action = "Index", p = UrlParameter.Optional },
                new { p = "[0-9]*" }
            );

            context.MapRoute(
                "zh-cn_Rss",
                "zh-cn/rss/",
                new { controller = "Home", action = "Rss" }
            );

            context.MapRoute(
                "zh-cn_CommentRss",
                "zh-cn/commentrss/",
                new { controller = "Home", action = "CommentRss" }
            );

            context.MapRoute(
                "zh-cn_Category",
                "zh-cn/cate/{id}/{p}",
                new { controller = "Home", action = "Category", p = UrlParameter.Optional },
                new { id = "[0-9]+", p = "[0-9]*" }
            );

            context.MapRoute(
                "zh-cn_Article",
                "zh-cn/archive/{key}",
                new { controller = "Home", action = "ArticleByKey" },
                new { key = @"^[a-zA-Z0-9\-]+$" }
            );

            context.MapRoute(
                "zh-cn_Album",
                "zh-cn/album/{key}",
                new { controller = "Home", action = "AlbumByKey" },
                new { key = @"^[a-zA-Z0-9\-]+$" }
            );

            context.MapRoute(
                "zh-cn_Tag",
                "zh-cn/tag/{key}/{p}",
                new { controller = "Home", action = "Tag", p = UrlParameter.Optional },
                new { p = "[0-9]*" }
            );

            context.MapRoute(
                "zh-us_User",
                "zh-us/u/{user}",
                new { controller = "Account", action = "UView" }
            );

            context.MapRoute(
                "zh-cn_Search",
                "zh-cn/search/{key}/{p}",
                new { controller = "Home", action = "Search", p = UrlParameter.Optional },
                new { p = "[0-9]*" }
            );

            context.MapRoute(
                "zh-cn_Author",
                "zh-cn/author/{user}",
                new { controller = "Home", action = "Author" }
            );

            context.MapRoute(
                "zh-cn_Archives",
                "zh-cn/archives/{year}/{month}/{day}",
                new { controller = "Home", action = "Archives", month = UrlParameter.Optional, day = UrlParameter.Optional },
                new { year = "[0-9]+", month = "[0-9]*", day = "[0-9]*" }
            );

            context.MapRoute(
                "zh-cn_CategoryByKey",
                "zh-cn/{key}/{p}",
                new { controller = "Home", action = "CategoryByKey", p = UrlParameter.Optional },
                new { key = @"^[a-zA-Z0-9\-]+$", p = "[0-9]*" }
            );

            context.MapRoute(
                "zh-cn_Default",
                "zh-cn/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }

}