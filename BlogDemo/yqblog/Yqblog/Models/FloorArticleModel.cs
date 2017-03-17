using System.Collections.Generic;
using Yqblog.Data;

namespace Yqblog.Models
{
    public class FloorArticleModel : blog_article
    {
        public IEnumerable<blog_article> FloorArticles { get; set; }
    }
}