using System.Collections.Generic;
using System.Linq;
using Common;
using Yqblog.Data;
using Yqblog.Models;

namespace Yqblog.Services.ServiceImpl
{
    public partial class ServiceImpl
    {
        public AlbumModel GetAlbum(long id)
        {
            return GetAlbum(GetVArticleById(id));
        }

        public AlbumModel GetAlbum(blog_varticle varticle)
        {
            var album = new AlbumModel();
            if (varticle != null)
            {
                album.Varticle = varticle;
                AlbumPhotoModel cover;
                album.ImageList = GetAlbumPhotoList(varticle.content, out cover);
                album.Cover = cover;
                album.ImgCount = album.ImageList.Count();
                album.Category = GetCategoryById(varticle.cateid);
            }
            return album;
        }

        public List<AlbumModel> GetAlbums(int cid,int count=0)
        {
            var varticles = cid==0 ? GetVArticles(4, 0, count) : GetVArticles(4, GetCategoryIds(cid), count);
            var albums = new List<AlbumModel>();
            foreach (var varticle in varticles)
            {
                albums.Add(GetAlbum(varticle));
            }
            return albums.OrderBy(m=>m.Category.OrderId).ThenBy(m=>m.Varticle.istop).ToList();
        }

        private static List<AlbumPhotoModel> GetAlbumPhotoList(string content, out AlbumPhotoModel cover)
        {
            var lst = new List<AlbumPhotoModel>();
            cover = new AlbumPhotoModel();
            try
            {
                var jsonnav = Utils.RemoveHtml(content).Replace("\n", "").Replace("&nbsp;", "");
                if (jsonnav != "")
                {
                    lst = Utils.ParseFromJson<List<AlbumPhotoModel>>(jsonnav);
                    cover = lst.OrderByDescending(m => m.IsCover).FirstOrDefault();
                }
            }
            catch { }
            return lst;
        }
    }
}