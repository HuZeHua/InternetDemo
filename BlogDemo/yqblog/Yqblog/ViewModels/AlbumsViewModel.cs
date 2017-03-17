using System.Collections.Generic;
using Yqblog.Models;

namespace Yqblog.ViewModels
{
    public class AlbumsViewModel
    {
        public string WebTitle { get; set; }
        public string WebPath { get; set; }
        public string CurrentUrl { get; set; }
        public CategoryModel Category { get; set; }
        public IEnumerable<AlbumModel> Albums { get; set; }
    }
}
