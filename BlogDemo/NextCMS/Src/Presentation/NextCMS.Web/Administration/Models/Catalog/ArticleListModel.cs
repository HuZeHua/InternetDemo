using XCode.Web.Core.Mvc;

namespace XCode.Admin.Models.Catalog
{
    public class ArticleListModel : BaseNextCMSModel
    {
        public string Title { get; set; }

        public int Views { get; set; }

        public int CommentCount { get; set; }

        public bool Published { get; set; }

        public string CreateDate { get; set; }
    }
}