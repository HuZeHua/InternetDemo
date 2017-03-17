using System.Collections.Generic;
using Yqblog.Data;
using Yqblog.General;
using Yqblog.Models;

namespace Yqblog.ViewModels
{
    public class IndexViewModel
    {
        public string WebTitle { get; set; }
        public IEnumerable<string> ArticleDates { get; set; }
        public IEnumerable<ArticleArchives> ArticleArchivesInfo { get; set; }
        public IEnumerable<blog_varticle> NewBbsTopics { get; set; }
        public IEnumerable<blog_varticle> MostViewArticles { get; set; }
        public IEnumerable<blog_varticle> MostCommendArticles { get; set; }
        public IEnumerable<blog_varticle> MostReplyArticles { get; set; }
        public IEnumerable<blog_varticle> NewReplyArticles { get; set; }
        public IEnumerable<blog_varticle> NewArticleReplies { get; set; }
        public IEnumerable<TagInfo> Tags { get; set; }
        public int ArticleCount { get; set; }
        public int AlbumCount { get; set; }
        public int NoteCount { get; set; }
        public int ArticleReplyCount { get; set; }
        public int AlbumReplyCount { get; set; }
        public Pager ArticlePagerInfo { get; set; }
    }
}
