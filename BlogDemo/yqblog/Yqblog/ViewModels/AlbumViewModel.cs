using System.Collections.Generic;
using Yqblog.Data;
using Yqblog.Models;

namespace Yqblog.ViewModels
{
    public class AlbumViewModel
    {
        public string WebTitle { get; set; }
        public string WebPath { get; set; }
        public string Seo { get; set; }
        public CategoryModel Category { get; set; }
        public blog_varticle ArticleInfo { get; set; }
        public CommentModel Comment { get; set; }
        public blog_varticle Varticle { get; set; }
        public int ImgCount { get; set; }
        public List<AlbumPhotoModel> PhotoList { get; set; }
        public AlbumPhotoModel Cover { get; set; }
        public string AlbumPath { get; set; }
    }
}
