namespace XCode.Core.Domain.Settings
{

    public class ArticleSettings : ISettings
    {
        /// <summary>
        /// 文章列表数
        /// </summary>
        public int ArticlePageSize { get; set; }

        /// <summary>
        /// 文章评论列表数
        /// </summary>
        public int CommentPageSize { get; set; }

        /// <summary>
        /// 最新评论列表数
        /// </summary>
        public int LatestCommentPageSize { get; set; }

        /// <summary>
        /// 评论排行榜列表数
        /// </summary>
        public int HotCommentPageSize { get; set; }

        /// <summary>
        /// 阅读排行榜列表数
        /// </summary>
        public int HotArticlePageSize { get; set; }

        /// <summary>
        /// 允许评论
        /// </summary>
        public bool AllowComment { get; set; }

    }
}
