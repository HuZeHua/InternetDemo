using System.Collections.Generic;
using MvcPaging;
using XCode.Core.Domain.Catalog;
using XCode.Web.Models.Articles;

namespace XCode.Web.Models.Home
{
    public partial class HomeModel
    {
        public HomeModel()
        {
            this.HotArticles = new List<ArticleModel>();
            this.CommentArticles = new List<ArticleModel>();
        }

        public IPagedList<Article> Articles { get; set; }
        public IEnumerable<ArticleModel> RecommandArticles { get; set; }
        public IEnumerable<ArticleModel> HotArticles { get; set; }
        public IEnumerable<ArticleModel> CommentArticles { get; set; }
    }
}