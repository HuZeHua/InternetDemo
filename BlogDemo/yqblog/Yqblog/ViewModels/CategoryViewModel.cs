using System.Collections.Generic;
using Yqblog.General;
using Yqblog.Models;

namespace Yqblog.ViewModels
{
    public class CategoryViewModel
    {
        public string WebTitle { get; set; }
        public string WebPath { get; set; }
        public string CurrentUrl { get; set; }
        public int IsCommend { get; set; }
        public int CateId { get; set; }
        public Pager ArticlePagerInfo { get; set; }
        public CategoryModel Category { get; set; }
    }
}
