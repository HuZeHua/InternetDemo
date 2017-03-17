using System;
using System.Collections.Generic;
using System.Linq;
using Yqblog.Data;
using Yqblog.General;
using Yqblog.Models;

namespace Yqblog.Services.ServiceImpl
{
    public partial class ServiceImpl : IServices
    {
        readonly YqblogEntities _entity = new YqblogEntities();

        public IQueryable<blog_varticle> GetVArticles(int tid, int cid, int count, int iscomment = 0, string field = "", long user = 0)
        {
            var query = GetVArticlesViaLang();
            if (cid > 0)
            {   query = query.Where(m => m.cateid == cid);}
            else if (tid > 0)
            {   query = query.Where(m => m.typeid == tid);}
            if (iscomment > 0)
            {    query = query.Where(m => m.iscommend == iscomment);}
            if (user>0)
            {    query = query.Where(m => m.userid == user);}
            if (field != "")
            {
                switch (field)
                {
                    case "viewcount":
                        query = query.Where(m => m.viewcount>0);
                        query = query.OrderByDescending(m =>m.viewcount);
                        break;
                    case "subcount":
                        query = query.Where(m => m.subcount > 0);
                        query = query.OrderByDescending(m => m.subcount);
                        break;
                    case "lastpost":
                        query = query.Where(m => m.subcount>0).OrderByDescending(m => m.lastreplydate);
                        break;
                    case "favor":
                        query = query.OrderByDescending(m => m.favor);
                        break;
                }
                
            }
            else
            {   query = query.OrderByDescending(m => m.id);}
            if (count > 0)
            {   query = query.Take(count);}

            return query;
        }

        public Pager GetArticlePaging(Pager pager, int tid, int cid, int iscommend = 0,string field="",long user=0,string order="desc",string articleListType="index")
        {
            var query = GetVArticles(tid, cid, 0, iscommend, "",user);
            pager.Amount = query.Count();
            if (field != "")
            {
                switch (field)
                {
                    case "lastpost":
                        query = (cid > 0 ? query.OrderBy(m => m.lastreplydate) : query.OrderByDescending(m => m.lastreplydate)).Skip(pager.PageSize * GetPageNo(pager.PageNo)).Take(pager.PageSize);
                        break;
                }
            }
            else
            {
                switch (articleListType)
                {
                    case "category":
                        query = (order == "asc" ? query.OrderBy(m => m.istop).ThenBy(m => m.id) : query.OrderBy(m => m.istop).ThenByDescending(m => m.id)).Skip(pager.PageSize * GetPageNo(pager.PageNo)).Take(pager.PageSize);
                        break;
                    case "index":
                        query = (order == "asc" ? query.OrderBy(m => m.isindextop).ThenBy(m => m.id) : query.OrderBy(m => m.isindextop).ThenByDescending(m => m.id)).Skip(pager.PageSize * GetPageNo(pager.PageNo)).Take(pager.PageSize);
                        break;
                    default:
                        query = (order == "asc" ? query.OrderBy(m => m.id) : query.OrderByDescending(m => m.id)).Skip(pager.PageSize * GetPageNo(pager.PageNo)).Take(pager.PageSize);
                        break;
                }
                
            }
            pager.Entity = query;
            return pager;
        }

        public Pager GetNotesPagingByOrderId(Pager pager, int cid, long orderid, string order = "asc")
        {
            var query = GetVArticles(0, cid, 0, 0, string.Empty);

            var queryTmp = order == "asc" ? query.Where(m => m.orderid <= orderid) : query.Where(m => m.orderid >= orderid);
            pager.Amount = queryTmp.Count();
            pager.PageNo = pager.PageCount > 0 ? pager.PageCount : 1;
            pager.Amount = query.Count();
            query = (order == "asc" ? query.OrderBy(m => m.id) : query.OrderByDescending(m => m.id)).Skip(pager.PageSize * GetPageNo(pager.PageNo)).Take(pager.PageSize);
            pager.Entity = query;
            return pager;
        }

        public IQueryable<blog_varticle> GetVArticles(int tid, string cids, int count, int iscommend = 0)
        {
            var query = GetVArticlesViaLang();
            if (tid > 0)
            {   query = query.Where(m => m.typeid == tid);}
            if (iscommend>0)
            {  query = query.Where(m => m.iscommend == iscommend);}
            if (cids!="")
            {
                var listids = new List<string>(cids.Split(',')).ConvertAll(int.Parse);
                query = query.Where(m => listids.Contains(m.cateid));
            }
            if (count > 0)
            {  query = query.Take(count);}
            return query;
        }

        public Pager GetArticlePaging(Pager pager, int tid, int cid,string cids, int iscommend = 0, string order = "desc")
        {
            var query = GetVArticles(tid, cids, 0, iscommend);
            pager.Amount = query.Count();

            query = (order == "asc" ? query.OrderBy(m => (m.cateid == cid && m.istop == 1) ? 1 : 2).ThenBy(m => m.id) : query.OrderBy(m => (m.cateid == cid && m.istop == 1) ? 1 : 2).ThenByDescending(m => m.id)).Skip(pager.PageSize * GetPageNo(pager.PageNo)).Take(pager.PageSize);

            pager.Entity = query;
            return pager;
        }

        public Pager GetTagArticlePaging(Pager pager, int tid, string tag, int iscommend, string order = "desc")
        {
            var query = GetVArticles(tid, 0, 0, iscommend).Where(m =>
                                      ("," + m.tags + ",").Contains("," + tag + ","));

            pager.Amount = query.Count();
            query = (order == "asc" ? query.OrderBy(m => m.id) : query.OrderByDescending(m => m.id)).Skip(pager.PageSize * GetPageNo(pager.PageNo)).Take(pager.PageSize);
            pager.Entity = query;
            return pager;
        }

        public Pager GetCommentPaging(Pager pager, long articleid, string order = "asc")
        {
            IQueryable<blog_article> query = _entity.blog_article;
            if (articleid > 0)
            {
                query = query.Where(m =>m.layer==1 && m.articleid == articleid);
            }
            pager.Amount = query.Count();
            query = (order == "asc" ? query.OrderBy(m => m.id) : query.OrderByDescending(m => m.id)).Skip(pager.PageSize*GetPageNo(pager.PageNo)).Take(pager.PageSize);
            pager.Entity = query;
            return pager;
        }

        public Pager GetCommentsPagingByOrderId(Pager pager, long articleid, long orderid, string order = "asc")
        {
            IQueryable<blog_article> query = _entity.blog_article;
            if (articleid > 0)
            {
                query = query.Where(m => m.layer == 1 && m.articleid == articleid);
            }
            var queryTmp = order == "asc" ? query.Where(m => m.orderid <= orderid) : query.Where(m => m.orderid >= orderid);
            pager.Amount = queryTmp.Count();
            pager.PageNo = pager.PageCount > 0 ? pager.PageCount : 1;
            pager.Amount = query.Count();
            query = (order == "asc" ? query.OrderBy(m => m.id) : query.OrderByDescending(m => m.id)).Skip(pager.PageSize * GetPageNo(pager.PageNo)).Take(pager.PageSize);
            pager.Entity = query;
            return pager;
        }

        public Pager GetFloorCommentPaging(Pager pager, long articleid, string order = "asc")
        {
            IQueryable<blog_article> queryAll = _entity.blog_article;
            if (articleid > 0)
            {
                queryAll = queryAll.Where(m => m.layer == 1 && m.articleid == articleid);
            }
            var query = queryAll.Where(m => m.status == 1);
            pager.Amount = query.Count();
            var queryPage = (order == "asc" ? query.OrderBy(m => m.id) : query.OrderByDescending(m => m.id)).Skip(pager.PageSize * GetPageNo(pager.PageNo)).Take(pager.PageSize);
            pager.Entity = ConvertToFloorArticle(queryAll, queryPage);
            return pager;
        }

        public Pager GetFloorCommentPagingByOrderId(Pager pager, long articleid, long orderid, string order = "asc")
        {
            IQueryable<blog_article> queryAll = _entity.blog_article;
            if (articleid > 0)
            {
                queryAll = queryAll.Where(m => m.layer == 1 && m.articleid == articleid);
            }
            var query = queryAll.Where(m => m.status == 1);
            var queryTmp = order == "asc" ? query.Where(m => m.orderid <= orderid) : query.Where(m => m.orderid >= orderid);
            pager.Amount = queryTmp.Count();
            pager.PageNo = pager.PageCount > 0 ? pager.PageCount : 1;
            pager.Amount = query.Count();
            var queryPage = (order == "asc" ? query.OrderBy(m => m.id) : query.OrderByDescending(m => m.id)).Skip(pager.PageSize * GetPageNo(pager.PageNo)).Take(pager.PageSize);
            pager.Entity = ConvertToFloorArticle(queryAll, queryPage);
            return pager;
        }

        public Pager GetFloorNotePaging(Pager pager, int cid, string order = "asc")
        {
            var queryAll = _entity.blog_article.Where(m => m.cateid == cid);
            var query = queryAll.Where(m => m.status == 1);
            pager.Amount = query.Count();
            var queryPage = (order == "asc" ? query.OrderBy(m => m.istop).ThenBy(m => m.id) : query.OrderBy(m => m.istop).ThenByDescending(m => m.id)).Skip(pager.PageSize * GetPageNo(pager.PageNo)).Take(pager.PageSize);
            pager.Entity = ConvertToFloorArticle(queryAll, queryPage);
            return pager;
        }

        public Pager GetFloorNoteByOrderId(Pager pager, int cid, long orderid, string order = "asc")
        {
            var queryAll = _entity.blog_article.Where(m => m.cateid == cid);
            var query = queryAll.Where(m => m.status == 1);
            var queryTmp = order == "asc" ? query.Where(m => m.orderid <= orderid) : query.Where(m => m.orderid >= orderid);
            pager.Amount = queryTmp.Count();
            pager.PageNo = pager.PageCount > 0 ? pager.PageCount : 1;
            pager.Amount = query.Count();
            var queryPage = (order == "asc" ? query.OrderBy(m => m.id) : query.OrderByDescending(m => m.id)).Skip(pager.PageSize * GetPageNo(pager.PageNo)).Take(pager.PageSize);
            pager.Entity = ConvertToFloorArticle(queryAll, queryPage);
            return pager;
        }

        private static IQueryable<FloorArticleModel> ConvertToFloorArticle(IQueryable<blog_article> allData, IEnumerable<blog_article> pageData)
        {
            var result = new List<FloorArticleModel>();
            foreach (var item in pageData)
            {
                result.Add(new FloorArticleModel
                               {
                                   id = item.id,
                                   against = item.against,
                                   articleid = item.articleid,
                                   articletypeid = item.articletypeid,
                                   cateid = item.cateid,
                                   catepath = item.catepath,
                                   userid = item.userid,
                                   username = item.username,
                                   summary = item.summary,
                                   status = item.status,
                                   viewcount = item.viewcount,
                                   content = item.content,
                                   title = item.title,
                                   favor = item.favor,
                                   parentid = item.parentid,
                                   lang = item.lang,
                                   ip = item.ip,
                                   iscommend = item.iscommend,
                                   istop = item.istop,
                                   layer = item.layer,
                                   orderid = item.orderid,
                                   createdate = item.createdate,
                                   FloorArticles = GetFloorArticles(allData, item, new List<blog_article> { item })
                               });
            }

            return result.AsQueryable();
        }

        private static IEnumerable<blog_article> GetFloorArticles(IQueryable<blog_article> allData,blog_article article,List<blog_article> resultData)
        {
            if (article!=null && article.parentid > 0)
            {
                var parentArticle = allData.FirstOrDefault(x => x.id == article.parentid);
                if (parentArticle != null && parentArticle.status == 1)
                {
                    resultData.Add(parentArticle);
                }
                else
                {
                    resultData.Add(null); 
                }
                return GetFloorArticles(allData, parentArticle, resultData);
            }
            resultData.Reverse();
            return resultData;
        }

        public Pager GetReplyPaging(Pager pager, int tid, int cid, string sort = "", string order = "desc", long user = 0)
        {
            var query = GetReplyArticles(tid, cid, 0,user);
            switch (sort)
            {
                case "normal":
                    query = query.Where(m => m.status == 1);
                    break;
                case "draft":
                    query = query.Where(m => m.status == 2);
                    break;
                case "istop":
                    query = query.Where(m => m.istop == 1);
                    break;
                case "iscommend":
                    query = query.Where(m => m.iscommend == 1);
                    break;
            }
            pager.Amount = query.Count();
            query = order == "desc" ? query.OrderByDescending(m => m.id) : query.OrderBy(m => m.id);
            query = query.Skip(pager.PageSize * GetPageNo(pager.PageNo)).Take(pager.PageSize);
            pager.Entity = query;
            return pager;
        }

        public Pager GetReplyPaging(Pager pager, int tid,int cid,int layer, string sort = "", string order = "desc",string user="")
        {
            var query = GetArticlesViaLang(0);
            query = query.Where(m => m.layer == layer);
            if (cid > 0)
            {   query = query.Where(m => m.cateid == cid);}
            if (tid > 0)
            {    query = query.Where(m => m.typeid == tid);}
            if (user!="")
            {   query = query.Where(m => m.userid == 1).Where(m => m.username == user);}

            switch (sort)
            {
                case "normal":
                    query = query.Where(m => m.status == 1);
                    break;
                case "draft":
                    query = query.Where(m => m.status == 2);
                    break;
                case "istop":
                    query = query.Where(m => m.istop == 1);
                    break;
                case "iscommend":
                    query = query.Where(m => m.iscommend == 1);
                    break;
            }
            pager.Amount = query.Count();
            query = order == "desc" ? query.OrderByDescending(m => m.id) : query.OrderBy(m => m.id);
            query = query.Skip(pager.PageSize * GetPageNo(pager.PageNo)).Take(pager.PageSize);
            pager.Entity = query;
            return pager;
        }

        public Pager GetReplyPaging(Pager pager, int tid, string cids, int layer, string sort = "", string order = "desc", string user = "")
        {
            var query = GetArticlesViaLang(0);
            if (tid > 0)
            {   query = query.Where(m => m.typeid == tid);}
            if (cids != "")
            {
                var listids = new List<string>(cids.Split(',')).ConvertAll(int.Parse);
                query = query.Where(m => listids.Contains(m.cateid));
            }
            if (user != "")
            {   query = query.Where(m => m.userid == 1).Where(m => m.username == user);}

            query = query.Where(m => m.layer == layer);
            switch (sort)
            {
                case "normal":
                    query = query.Where(m => m.status == 1);
                    break;
                case "draft":
                    query = query.Where(m => m.status == 2);
                    break;
                case "istop":
                    query = query.Where(m => m.istop == 1);
                    break;
                case "iscommend":
                    query = query.Where(m => m.iscommend == 1);
                    break;
            }
            pager.Amount = query.Count();
            query = order == "desc" ? query.OrderByDescending(m => m.id) : query.OrderBy(m => m.id);
            query = query.Skip(pager.PageSize * GetPageNo(pager.PageNo)).Take(pager.PageSize);
            pager.Entity = query;
            return pager;
        }

        public blog_varticle GetVArticleById(long id)
        {
            return _entity.blog_varticle.First(m => m.id == id);
        }

        public blog_article GetArticleById(long id)
        {
            return _entity.blog_article.FirstOrDefault(m => m.id == id);
        }

        public blog_varticle GetVArticleByReName(string rename)
        {
            return _entity.blog_varticle.FirstOrDefault(m => m.rename == rename);
        }

        public blog_varticle GetPreviewVArticle(blog_varticle varticle)
        {
            return GetPreviewVArticle(varticle, varticle.typeid);
        }

        public blog_varticle GetPreviewVArticle(blog_varticle varticle, int tid, int cid = 0, string field = "")
        {
            var re = new blog_varticle();
            var query = GetVArticles(tid, cid, 0);
            if (field != "")
            {
                switch (field)
                {
                    case "lastpost":
                        re = query.Where(m => m.lastreplydate < varticle.lastreplydate).OrderByDescending(m => m.lastreplydate).FirstOrDefault();
                        break;
                }
            }
            else
            {
                re = query.Where(m => m.id < varticle.id).OrderByDescending(m => m.id).FirstOrDefault();
            }
            return re;
        }

        public blog_varticle GetNextVArticle(blog_varticle varticle)
        {
            return GetNextVArticle(varticle, varticle.typeid);
        }

        public blog_varticle GetNextVArticle(blog_varticle varticle, int tid, int cid = 0, string field = "")
        {
            var re = new blog_varticle();
            var query = GetVArticles(tid, cid, 0);

            if (field != "")
            {
                switch (field)
                {
                    case "lastpost":
                        re = query.Where(m => m.lastreplydate > varticle.lastreplydate).OrderBy(m => m.lastreplydate).FirstOrDefault();
                        break;
                }
            }
            else
            {
                re = query.Where(m => m.id > varticle.id).OrderBy(m => m.id).FirstOrDefault();
            }
            return re;
        }

        public long AddArticle(blog_varticle varticle)
        {
            string lang;
            var obj = varticle;
            if (obj.parentid > 0)
            {
                lang = obj.layer == 0 ? GetArticleById(obj.parentid).lang : GetArticleById(obj.articleid).lang;
            }
            else
            {
                lang = WebUtils.GetCurrentLangStr();
            }
            obj.lang = lang;
            _entity.blog_varticle.AddObject(obj);
            _entity.SaveChanges();
            return obj.id;
        }

        public void UpdateVArticle(blog_varticle varticle)
        {
            var obj = _entity.blog_varticle.FirstOrDefault(m => m.id == varticle.id);
            if (obj == null) return;
            obj.title = varticle.title;
            obj.cateid = varticle.cateid;
            obj.catepath = varticle.catepath;
            obj.summary = varticle.summary;
            obj.content = varticle.content;
            obj.tags = varticle.tags;
            obj.seodescription = varticle.seodescription;
            obj.seokeywords = varticle.seokeywords;
            obj.seometas = varticle.seometas;
            obj.seotitle = varticle.seotitle;
            obj.rename = varticle.rename;
            obj.status = varticle.status;
            obj.replypermit = varticle.replypermit;
            obj.iscommend = varticle.iscommend;
            obj.istop = varticle.istop;
            obj.isindextop = varticle.isindextop;

            _entity.SaveChanges();
        }

        public void BatchUpdateArticlePath(int cid, string catepath)
        {
            var query = GetVArticles(0, cid, 0);
            foreach (var varticle in query)
            {
                varticle.catepath = catepath;
                UpdateArticle(varticle);
            }
        }

        public void UpdateArticle(blog_article aritcle)
        {
            var query = _entity.blog_article.FirstOrDefault(m => m.id == aritcle.id);
            if (query == null) return;
            query.typeid = aritcle.typeid;
            query.cateid = aritcle.cateid;
            query.catepath = aritcle.catepath;
            query.articleid = aritcle.articleid;
            query.parentid = aritcle.parentid;
            query.layer = aritcle.layer;
            query.subcount = aritcle.subcount;
            query.userid = aritcle.userid;
            query.username = aritcle.username;
            query.title = aritcle.title;
            query.summary = aritcle.summary;
            query.content = aritcle.content;
            query.viewcount = aritcle.viewcount;
            query.orderid = aritcle.orderid;
            query.replypermit = aritcle.replypermit;
            query.status = aritcle.status;
            query.ip = aritcle.ip;
            query.favor = aritcle.favor;
            query.against = aritcle.against;
            query.createdate = aritcle.createdate;
            query.istop = aritcle.istop;
            query.iscommend = aritcle.iscommend;
            query.status = aritcle.status;

            _entity.SaveChanges();
        }

        public void UpdateArticle(blog_varticle varitcle)
        {
            var query = _entity.blog_article.FirstOrDefault(m => m.id == varitcle.id);
            if (query == null) return;
            query.typeid = varitcle.typeid;
            query.cateid = varitcle.cateid;
            query.catepath = varitcle.catepath;
            query.articleid = varitcle.articleid;
            query.parentid = varitcle.parentid;
            query.layer = varitcle.layer;
            query.subcount = varitcle.subcount;
            query.userid = varitcle.userid;
            query.username = varitcle.username;
            query.title = varitcle.title;
            query.summary = varitcle.summary;
            query.content = varitcle.content;
            query.viewcount = varitcle.viewcount;
            query.orderid = varitcle.orderid;
            query.replypermit = varitcle.replypermit;
            query.status = varitcle.status;
            query.ip = varitcle.ip;
            query.favor = varitcle.favor;
            query.against = varitcle.against;
            query.createdate = varitcle.createdate;
            query.istop = varitcle.istop;
            query.iscommend = varitcle.iscommend;
            query.status = varitcle.status;

            _entity.SaveChanges();
        }

        public void DelArticle(blog_article article)
        {
            DelBaseArticle(article.id);
        }

        public void RemoveArticle(blog_article article)
        {
            _entity.fun_articledel(article.id);
        }

        public int GetTodayArticleCountByCategory(int cid)
        {
            return _entity.blog_article.Count(m => m.cateid == cid && m.layer == 0 && System.Data.Objects.EntityFunctions.DiffDays(m.createdate ,DateTime.Now)==0);
        }

        public int GetTodayArticleCountByType(int tid)
        {
            return GetArticlesViaLang().Count(m => m.typeid == tid && m.layer == 0 && System.Data.Objects.EntityFunctions.DiffDays(m.createdate, DateTime.Now) == 0);
        }

        public int GetArticleCountByCategory(int cid)
        {
            return _entity.blog_article.Count(m => m.cateid == cid && m.layer==0);
        }

        public int GetArticleCountByType(int tid,int layer=0)
        {
            return GetArticlesViaLang().Count(m => m.typeid == tid && m.layer == layer);
        }

        public int GetDataCountByCategory(int cid)
        {
            return _entity.blog_article.Count(m => m.cateid == cid);
        }

        public int GetDataCountByType(int tid)
        {
            return GetArticlesViaLang().Count(m => m.typeid == tid);
        }

        public blog_varticle GetLatestArticle(int cid)
        {
            return _entity.blog_varticle.Where(m => m.cateid == cid).OrderByDescending(m => m.id).FirstOrDefault();
        }

        public blog_article GetLatestReply(long id)
        {
            return _entity.blog_article.Where(m => m.parentid == id).OrderByDescending(m => m.id).FirstOrDefault();
        }

        public IQueryable<ArticleArchives> GetArticleArchives()
        {
            var grouped = (from p in GetVArticlesViaLang()
                           where p.typeid==1
                           group p by new { month = p.createdate.Month, year = p.createdate.Year } into d
                           select new ArticleArchives { Year = d.Key.year, Month = d.Key.month, Count = d.Count() }).OrderByDescending(g => g.Year).ThenByDescending(g => g.Month); 

            return grouped;
        }

        public Pager GetArchivesArticlePaging(Pager pager, int tid, int year, int month, int day, int iscommend, string order = "desc")
        {
            var query = GetVArticles(tid, 0, 0, iscommend).Where(m => m.createdate.Year == year);
            if (month > 0)
            {
                query = query.Where(m => m.createdate.Month == month);
            }
            if (day > 0)
            {
                query = query.Where(m => m.createdate.Day == day);
            }
            pager.Amount = query.Count();
            query = (order == "asc" ? query.OrderBy(m => m.id) : query.OrderByDescending(m => m.id)).Skip(pager.PageSize * GetPageNo(pager.PageNo)).Take(pager.PageSize);
            pager.Entity = query;
            return pager;
        }

        public IQueryable<blog_varticle> GetReplyArticles(int tid, int cid, int count,long user = 0)
        {
            var reply = GetArticlesViaLang().Where(m => m.layer == 1);
            if (tid > 0)
                reply = reply.Where(m => m.typeid == tid);
            if (cid > 0)
                reply = reply.Where(m => m.cateid == cid);
            if (user>0)
                reply = reply.Where(m => m.userid == user);


            var query = (from r in reply
                         join a in _entity.blog_varticle
                         on r.articleid equals a.id
                         select new
                         {
                             r.id, r.parentid, r.username, r.content, a.rename, a.title, a.url,
                             r.articleid,
                             a.createdate,
                             lastreplydate=r.createdate, r.userid, a.typeid,
                             r.orderid

                         }).OrderByDescending(m => m.id).Select(m => m);

            if (count > 0)
                query = query.Take(count);

            var varticles = query.ToList().Select(m => new blog_varticle
            {
                id = m.id,
                articleid = m.articleid,
                parentid = m.parentid,
                username = m.username,
                content = m.content,
                rename = m.rename,
                title = m.title,
                url = m.url,
                createdate=m.createdate,
                lastreplydate=m.lastreplydate,
                userid = m.userid,
                typeid = m.typeid,
                orderid=m.orderid
            }).ToList();

            return varticles.AsQueryable();
        }

        public IQueryable<string> GetArticleDates()
        {
            var grouped = (from p in GetVArticlesViaLang()
                           where p.typeid == 1
                           group p by new { day = p.createdate.Day, month = p.createdate.Month, year = p.createdate.Year } into d
                           select new
                           {
                               d.Key.day, d.Key.month, d.Key.year
                           });

            var dates = grouped.ToList().Select(m => m.year.ToString() + "-" + m.month.ToString() + "-" + m.day.ToString()).ToList();

            return dates.AsQueryable();
        }

        public Pager GetKeySearchPaging(Pager pager, int tid, string key, int iscommend, string order = "desc")
        {
            var query = GetVArticles(tid, 0, 0, iscommend).Where(m =>
                                      (m.tags + m.title + m.summary).Contains(key) || m.content.Contains(key));

            pager.Amount = query.Count();
            query = (order == "asc" ? query.OrderBy(m => m.id) : query.OrderByDescending(m => m.id)).Skip(pager.PageSize * GetPageNo(pager.PageNo)).Take(pager.PageSize);
            pager.Entity = query;
            return pager;
        }

        public Pager GetAuthorArticlePaging(Pager pager, int tid, string user, int iscommend, string order = "desc")
        {
            var query = GetVArticles(tid, 0, 0, iscommend).Where(m => m.username == user && m.userid > 0);

            pager.Amount = query.Count();
            query = (order == "asc" ? query.OrderBy(m => m.id) : query.OrderByDescending(m => m.id)).Skip(pager.PageSize * GetPageNo(pager.PageNo)).Take(pager.PageSize);
            pager.Entity = query;
            return pager;
        }

        public IQueryable<blog_varticle> GetArticlesByDateRange(int tid, string cids, int count, int daterange, string from, string to)
        {
            var query = GetVArticles(tid, cids, count);
            if (daterange == 1)
            {
                if (from == to)
                {
                    var d = Convert.ToDateTime(from);
                    query = query.Where(m => m.createdate.Year == d.Year && m.createdate.Month == d.Month && m.createdate.Day == d.Day);
                }
                else if (Convert.ToDateTime(from) < Convert.ToDateTime(to))
                {
                    var fromdate = Convert.ToDateTime(from);
                    var todate = Convert.ToDateTime(to).AddDays(1);
                    query = query.Where(m => m.createdate.CompareTo(fromdate)>= 0).Where(m => m.createdate.CompareTo(todate)<0);
                }
            }
            return query;
        }

        public int GetArticleCountByUser(string user,int tid)
        {
            return GetArticlesViaLang().Count(m => m.typeid == tid && m.username == user && m.userid==1);
        }

        public int GetCommentPageNo(int pagesize, long parentid, int floor, int ordertype = 1)
        {
            var pager = new Pager {PageSize = pagesize};
            var query = _entity.blog_article.Where(m => m.parentid == parentid);
            query = ordertype == 1 ? query.Where(m => m.orderid <= floor) : query.Where(m => m.orderid >= floor);
            pager.Amount = query.Count();
            return pager.PageCount;
        }

        public int GetArticlePageNo(int pagesize, int cid, int floor, int ordertype = 1)
        {
            var pager = new Pager {PageSize = pagesize};
            var query = _entity.blog_article.Where(m => m.cateid == cid).Where(m => m.layer ==0);
            query = ordertype == 1 ? query.Where(m => m.orderid <= floor) : query.Where(m => m.orderid >= floor);
            pager.Amount = query.Count();
            return pager.PageCount;
        }

        public blog_article GetArticleViaTitle(string str)
        {
            blog_article article = null;
            if (_entity.blog_article.Any(m => m.title.ToLower() == str.ToLower()))
            {
                article = _entity.blog_article.First(m => m.title.ToLower() == str.ToLower());
            }
            return article;
        }

        private void DelBaseArticle(long id)
        {
            _entity.fun_logicArticleDel(id);
        }

        private static int GetPageNo(int pageNo)
        {
            return (pageNo - 1 == -1) ? 0 : pageNo - 1;
        }

        private IQueryable<blog_varticle> GetVArticlesViaLang(int status = 1)
        {
            IQueryable<blog_varticle> query = _entity.blog_varticle;
            var lang = WebUtils.GetCurrentLangStr();
            if (_configinfo.IfIndependentContentViaLang == 1)
            { query = query.Where(m => m.lang == lang); }
            else
            {
                lang = _configinfo.WebContentLang;
                if (lang != "all")
                { query = query.Where(m => m.lang == lang); }
            }
            if (status > 0)
            { query = query.Where(m => m.status == status); }
            return query;
        }

        private IQueryable<blog_article> GetArticlesViaLang(int status = 1)
        {
            IQueryable<blog_article> query = _entity.blog_article;
            var lang = WebUtils.GetCurrentLangStr();
            if (_configinfo.IfIndependentContentViaLang == 1)
            { query = query.Where(m => m.lang == lang); }
            else
            {
                lang = _configinfo.WebContentLang;
                if (lang != "all")
                { query = query.Where(m => m.lang == lang); }
            }
            if (status > 0)
            { query = query.Where(m => m.status == status); }
            return query;
        }
    }
}