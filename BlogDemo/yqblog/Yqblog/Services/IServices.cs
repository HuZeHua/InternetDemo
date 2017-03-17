using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using Yqblog.Data;
using Yqblog.General;
using Yqblog.Models;

namespace Yqblog.Services
{
    public interface IServices
    {
        string GetCategoryStr();

        string GetCategoryLangStr();

        CategoryModel GetCategoryById(int cid);

        int GetMaxCategoryId();

        CategoryModel GetCategoryByReName(string rename);

        List<CategoryModel> GetFCategoryList(string space = "");

        List<CategoryModel> GetFCategoryList(string tids, string cids, string space="");

        string GetCategoryPathUrl(string path);

        string GetCategoryPathUrl2(string path);

        List<CategoryModel> GetIndexCategoryList();

        IQueryable<CategoryModel> GetCategories();

        IQueryable<CategoryModel> GetSubCategoryList(int cid);

        IQueryable<CategoryModel> GetSonCategoryList(int cid);

        string GetCategoryIds(int cid);

        IQueryable<blog_varticle> GetVArticles(int tid, int cid, int count, int iscomment = 0,string field="",long user=0);

        blog_article GetArticleById(long id);

        blog_varticle GetVArticleById(long id);

        blog_varticle GetVArticleByReName(string rename);

        blog_varticle GetPreviewVArticle(blog_varticle varticle);

        blog_varticle GetPreviewVArticle(blog_varticle varticle, int tid, int cid = 0, string field = "");

        blog_varticle GetNextVArticle(blog_varticle varticle);

        blog_varticle GetNextVArticle(blog_varticle varticle, int tid, int cid = 0, string field = "");

        Pager GetArticlePaging(Pager pager, int tid, int cid, int iscommend = 0,string field="",long user=0,string order="desc",string articleListType="index");

        Pager GetNotesPagingByOrderId(Pager pager, int cid, long orderid, string order = "asc");

        Pager GetFloorNotePaging(Pager pager, int cid, string order = "asc");

        Pager GetFloorNoteByOrderId(Pager pager, int cid, long orderid, string order = "asc");

        Pager GetTagArticlePaging(Pager pager, int tid, string tag, int iscommend, string order = "desc");

        IQueryable<blog_varticle> GetVArticles(int tid, string cids, int count, int iscommend = 0);

        Pager GetArticlePaging(Pager pager, int tid, int cid,string cids, int iscommend = 0, string order = "desc");

        Pager GetCommentPaging(Pager pager, long articleid, string order = "asc");

        Pager GetCommentsPagingByOrderId(Pager pager, long articleid, long orderid, string order = "asc");

        Pager GetFloorCommentPaging(Pager pager, long articleid, string order = "asc");

        Pager GetFloorCommentPagingByOrderId(Pager pager, long articleid, long orderid, string order = "asc");

        Pager GetReplyPaging(Pager pager, int tid, int cid, string sort = "", string order = "desc", long user = 0);

        Pager GetReplyPaging(Pager pager, int tid, int cid, int layer, string sort = "", string order = "desc", string user = "");

        Pager GetReplyPaging(Pager pager, int tid, string cids, int layer, string sort = "", string order = "desc", string user = "");

        long AddArticle(blog_varticle varticle);

        void UpdateVArticle(blog_varticle varticle);

        void UpdateArticle(blog_varticle varticle);

        void UpdateArticle(blog_article article);

        void DelArticle(blog_article article);

        List<AlbumModel> GetAlbums(int cid, int count = 0);

        AlbumModel GetAlbum(long id);

        AlbumModel GetAlbum(blog_varticle varticle);

        int GetTodayArticleCountByCategory(int cid);

        int GetTodayArticleCountByType(int tid);

        int GetArticleCountByCategory(int cid);

        int GetArticleCountByType(int tid,int layer=0);

        int GetDataCountByCategory(int cid);

        int GetDataCountByType(int tid);

        blog_varticle GetLatestArticle(int cid);

        blog_article GetLatestReply(long id);

        IQueryable<ArticleArchives> GetArticleArchives();

        Pager GetArchivesArticlePaging(Pager pager, int tid, int year, int month, int day, int iscommend, string order = "desc");

        IQueryable<blog_varticle> GetReplyArticles(int tid, int cid, int count,long user = 0);

        IQueryable<string> GetArticleDates();

        List<SyndicationItem> GetRss(List<blog_varticle> list);

        List<SyndicationItem> GetCommentRss(List<blog_varticle> list);

        List<TagInfo> GetTagList(int tid, int count);

        Pager GetKeySearchPaging(Pager pager, int tid, string key, int iscommend, string order = "desc");

        Pager GetAuthorArticlePaging(Pager pager, int tid, string user, int iscommend, string order = "desc");
        
        void BatchUpdateArticlePath(int cid, string catepath);

        IQueryable<blog_varticle> GetArticlesByDateRange(int tid, string cids, int count,int daterange, string from,string to);

        UserInfoModel GetUserInfo(string username);

        int GetArticleCountByUser(string user, int tid);

        int GetCommentPageNo(int pagesize, long parentid, int floor, int ordertype = 1);

        int GetArticlePageNo(int pagesize, int cid, int floor, int ordertype = 1);

        blog_article GetArticleViaTitle(string str);

        List<UserInfoModel> GetAdminList();

        blog_vuser GetUserInfoById(long userid);

        List<UserInfoModel> GetUserList();

        blog_users UserExist(string name, string password);

        bool UserExist(string name);

        void UpdateLastLogInDate(long userid);

        void UpdateLastActivityDate(long userid);

        long AddUser(UserInfoModel user);

        void UpdatePassword(long userid, string newpwd);

        void UpdateUserRole(long userid, int roleid);

        void UpdateUserState(long userid, int state);

        void UpdateUserProfile(UserInfoModel user);

        void DeleteUser(long userid);

        void RemoveArticle(blog_article article);
    }
}