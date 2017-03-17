using System.Web.Mvc;

namespace Yqblog.ViewModels
{
    public class ArticleAjaxInfoViewModel
    {
        public MvcHtmlString PreviousLink { get; set; }
        public MvcHtmlString NextLink { get; set; }
        public int FavorCount { get; set; }
        public int AgainstCount { get; set; }
        public int ViewCount { get; set; }
        public int CommentCount { get; set; }
        public CommentListViewModel CommentsInfo { get; set; }
    }
}
