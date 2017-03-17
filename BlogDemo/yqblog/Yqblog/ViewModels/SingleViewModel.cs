using Yqblog.Data;
using Yqblog.Models;

namespace Yqblog.ViewModels
{
    public class SingleViewModel
    {
        public string WebTitle { get; set; }
        public string WebPath { get; set; }
        public string CurrentUrl { get; set; }
        public blog_varticle ArticleInfo { get; set; }
        public CategoryModel Category { get; set; }
    }
}
