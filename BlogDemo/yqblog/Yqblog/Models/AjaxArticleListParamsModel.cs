namespace Yqblog.Models
{
    public class AjaxArticleListParamsModel
    {
        public string ArticleListType { get; set; }
        public int PageId { get; set; }
        public int CategoryId { get; set; }
        public string AuthorName { get; set; }
        public string Tag { get; set; }
        public string SearchKey { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Commend { get; set; }
        public string Order { get; set; }
    }
}