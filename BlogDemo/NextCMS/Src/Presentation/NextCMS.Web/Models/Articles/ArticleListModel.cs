using MvcPaging;
using XCode.Core.Domain.Catalog;

namespace XCode.Web.Models.Articles
{
    public class ArticleListModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public IPagedList<Article> Articles { get; set; }
    }
}