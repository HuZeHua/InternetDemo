using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using System.Collections;
using Yqblog.Data;
using Yqblog.General;
using Yqblog.Models;
using Yqblog.ViewModels;
using res = Resource.Web;

namespace Yqblog.Controllers
{
    [WebFilter]
    public class HomeController : BaseController
    {
        delegate void SendDelegate(string to, string subject, string body);

        public ActionResult Index(int? p, int? commend,string order)
        {
            var iscommend = commend ?? 0;
            var lst = MyService.GetIndexCategoryList();
            foreach (var category in lst)
            {
                switch (category.Type)
                {
                    case 1:
                        ViewData["Articles_" + category.HomePageKey] = MyService.GetVArticles(0, category.CateId, category.ListNum == 0 ? 10 : category.ListNum);
                        break;
                    case 2:
                        ViewData["SinglePage_" + category.HomePageKey] = MyService.GetVArticles(0, category.CateId, category.ListNum == 0 ? 10 : category.ListNum);
                        break;
                    case 4:
                        ViewData["Albums_" + category.HomePageKey] = MyService.GetAlbums(category.CateId, category.ListNum == 0 ? 10 : category.ListNum);
                        break;
                    case 6:
                        ViewData["Notes_" + category.HomePageKey] = MyService.GetVArticles(0, category.CateId, category.ListNum == 0 ? 10 : category.ListNum);
                        break;
                    case 7:
                        {
                            var re = MyService.GetVArticles(7, 0, 0);
                            if (re != null && re.Any())
                            {
                                foreach (var varticle in re)
                                {
                                    ViewData["Home_" + varticle.title] = varticle.content;
                                }
                            }
                        }
                        break;
                }
            }

            var pager = new Pager { PageNo = p ?? 1, PageSize = Configinfo.IndexPagerCount };
            pager = MyService.GetArticlePaging(pager, 1, 0, iscommend, order: order);

            var indexViewModel = new IndexViewModel
            {
                WebTitle = Configinfo.Webtitle,
                ArticleDates = MyService.GetArticleDates(),
                ArticleArchivesInfo = MyService.GetArticleArchives(),
                NewBbsTopics = MyService.GetVArticles(5, 0, 10),
                MostViewArticles = MyService.GetVArticles(1, 0, 10, field: "viewcount"),
                MostCommendArticles = MyService.GetVArticles(1, 0, 10, field: "favor"),
                MostReplyArticles = MyService.GetVArticles(1, 0, 10, field: "subcount"),
                NewReplyArticles = MyService.GetVArticles(1, 0, 10, field: "lastpost"),
                NewArticleReplies = MyService.GetReplyArticles(1, 0, 10),
                Tags = MyService.GetTagList(1, 0),
                ArticleCount = MyService.GetArticleCountByType(1),
                AlbumCount = MyService.GetArticleCountByType(4),
                NoteCount = MyService.GetArticleCountByType(6),
                ArticleReplyCount = MyService.GetArticleCountByType(1, 1),
                AlbumReplyCount = MyService.GetArticleCountByType(4, 1),
                ArticlePagerInfo = pager
            };
            var articleListParasInfo = new AjaxArticleListParamsModel
                                           {
                                               ArticleListType = "index",
                                               Commend = iscommend,
                                               Order = order
                                           };
            ViewBag.ArticleListParasInfo = articleListParasInfo;

            return View(indexViewModel);
        }

        public ActionResult CategoryByKey(string key, int? p, int? commend, int? f,string order)
        {
            var iscommend = commend ?? 0;
            var category = MyService.GetCategoryByReName(key);
            return category != null ? Category(category.CateId, p, iscommend, f ?? 0, order) : View("Error");
        }

        public ActionResult Category(int id, int? p, int? commend, int? f, string order)
        {
            var category = MyService.GetCategoryById(id);
            var currenturl = WebUtils.GetCateUrl(category);
            var webPath = MyService.GetCategoryPathUrl(category.Path);
            var pager = new Pager { PageNo = p ?? 1 };

            switch (category.Type)
            {
                case 2:
                    var re = MyService.GetVArticles(0, id, 0);
                    if (re.Any())
                    {
                        var varticle = re.ToList()[0];
                        var singleViewModel = new SingleViewModel
                        {
                            WebTitle = category.CateName,
                            WebPath = webPath,
                            CurrentUrl = currenturl,
                            ArticleInfo = varticle,
                            Category = category
                        };
                        return View(WebUtils.GetViewName(category.CustomView, "Single"), singleViewModel);
                    }
                    return View("Error");
                case 4:
                    var albumsViewModel = new AlbumsViewModel
                    {
                        WebTitle = category.CateName,
                        WebPath = webPath,
                        CurrentUrl = currenturl,
                        Category = category,
                        Albums = MyService.GetAlbums(id),
                    };
                    return View(WebUtils.GetViewName(category.CustomView, "Albums"), albumsViewModel);
                case 6:
                    var note = new NoteModel
                    {
                        CategoryId = id,
                        NoteId = 0,
                        DataType = 1,
                        UserId = UserInfo == null ? 0 : UserInfo.userid,
                        UserName = UserInfo == null ? string.Empty : UserInfo.username
                    };
                    pager.PageSize = Configinfo.NotePagerCount;
                    var orderType = string.IsNullOrEmpty(order) ? "desc" : order;
                    if (f != null && f > 0)
                    {
                        pager = MyService.GetFloorNoteByOrderId(pager, id, (long)f, orderType);
                    }
                    else
                    {
                        pager = MyService.GetFloorNotePaging(pager, id, orderType);
                    }
                    var noteViewModel = new NoteViewModel
                    {
                        WebTitle = category.CateName,
                        WebPath = webPath,
                        CurrentUrl = currenturl,
                        Note = note,
                        NoteList = new NoteListViewModel { NotePagerInfo = pager },
                        NoteOrderType = orderType,
                        Category = category
                    };
                    return View(WebUtils.GetViewName(category.CustomView, "Note"), noteViewModel);
                default:
                    var iscommend = commend ?? 0;
                    pager.PageSize = Configinfo.CatePagerCount;
                    pager = category.SubCount > 0
                        ? MyService.GetArticlePaging(pager, 0, id, MyService.GetCategoryIds(id), iscommend, order)
                        : MyService.GetArticlePaging(pager, 0, id, iscommend, order: order, articleListType: "category");
                    var categoryViewModel = new CategoryViewModel
                    {
                        WebTitle = category.CateName,
                        WebPath = webPath,
                        CurrentUrl = currenturl,
                        IsCommend = iscommend,
                        CateId = id,
                        ArticlePagerInfo = pager,
                        Category = category
                    };
                    var articleListParasInfo = new AjaxArticleListParamsModel
                    {
                        ArticleListType = "category",
                        CategoryId = id,
                        Commend = iscommend,
                        Order = order
                    };
                    ViewBag.ArticleListParasInfo = articleListParasInfo;
                    return View(WebUtils.GetViewName(category.CustomView, "Category"), categoryViewModel);
            }
        }

        public ActionResult Tag(string key, int? p, int? commend, string order)
        {
            var iscommend = commend ?? 0;
            var pager = new Pager {PageNo = p ?? 1, PageSize = Configinfo.CatePagerCount};
            pager = MyService.GetTagArticlePaging(pager, 1, key.Replace("@", "."), iscommend, order);

            var archivesViewModel = new ArchivesViewModel
                                   {
                                       WebPath =  "[" + res.Tag + "] " + key.Replace("@", "."),
                                       ArticlePagerInfo = pager
                                   };
            var articleListParasInfo = new AjaxArticleListParamsModel
            {
                ArticleListType = "tag",
                Tag = key,
                Commend = iscommend,
                Order = order
            };
            ViewBag.ArticleListParasInfo = articleListParasInfo;
            return View("Archives", archivesViewModel);
        }

        public ActionResult Archives(int year, int? month, int? day, int? p, int? commend, string order)
        {
            var iscommend = commend ?? 0;
            var pager = new Pager {PageNo = p ?? 1, PageSize = Configinfo.CatePagerCount};
            pager = MyService.GetArchivesArticlePaging(pager, 1, year, month ?? 0, day ?? 0, iscommend, order);

            var archivesViewModel = new ArchivesViewModel
            {
                WebPath = "[" + res.Archive + "] " + year + (month != null ? "/" + month : string.Empty) + (day != null ? "/" + day : string.Empty),
                ArticlePagerInfo = pager
            };
            var articleListParasInfo = new AjaxArticleListParamsModel
            {
                ArticleListType = "archives",
                Year = year,
                Month = month ?? 0,
                Day = day ?? 0,
                Commend = iscommend,
                Order = order
            };
            ViewBag.ArticleListParasInfo = articleListParasInfo;
            return View(archivesViewModel);
        }

        public ActionResult Search(string key, int? p, int? commend, string order)
        {
            var iscommend = commend ?? 0;
            var pager = new Pager { PageNo = p ?? 1, PageSize = Configinfo.CatePagerCount };
            pager = MyService.GetKeySearchPaging(pager, 1, key, iscommend, order);

            var archivesViewModel = new ArchivesViewModel
            {
                WebPath = "[" + res.SearchKey + "] " + key,
                ArticlePagerInfo = pager
            };
            var articleListParasInfo = new AjaxArticleListParamsModel
            {
                ArticleListType = "search",
                SearchKey = key,
                Commend = iscommend,
                Order = order
            };
            ViewBag.ArticleListParasInfo = articleListParasInfo;
            return View("Archives", archivesViewModel);
        }

        public ActionResult Author(string user, int? p, int? commend, string order)
        {
            var iscommend = commend ?? 0;
            var pager = new Pager { PageNo = p ?? 1, PageSize = Configinfo.CatePagerCount };
            pager = MyService.GetAuthorArticlePaging(pager, 1, user, iscommend, order);

            var archivesViewModel = new ArchivesViewModel
            {
                WebPath = "[Author] " + user,
                ArticlePagerInfo = pager
            };
            var articleListParasInfo = new AjaxArticleListParamsModel
            {
                ArticleListType = "author",
                AuthorName = user,
                Commend = iscommend,
                Order = order
            };
            ViewBag.ArticleListParasInfo = articleListParasInfo;
            return View("Archives", archivesViewModel);
        }

        public ActionResult ArticleByKey(string key)
        {
            var articleid = Utils.StrToInt(key);
            if (articleid > 0)
            {
                return Article(articleid);
            }
            var varticle = MyService.GetVArticleByReName(key);
            return varticle != null ? View("Article", ConvertToArticleViewModel(varticle)) : View("Error");
        }

        public ActionResult Article(long id)
        {
            return View("Article", GetArticleViewModel(id));
        }

        public ActionResult GetNav()
        {
            var navs = MyService.GetCategories();
            return Content("navstr", "text/html;charset=UTF-8");
        }

        public ActionResult Reply(int id)
        {
            ViewBag.CI = Configinfo;
            var comment = new CommentModel();
            var article = MyService.GetVArticleById(id);
            var category = MyService.GetCategoryById(article.cateid);
            comment.CommentArticleId = id;
            if (category.ReplyPermit == 1 && article.replypermit == 1)
            {   comment.ReplyPermit = 1;}
            else
            {   comment.ReplyPermit = 0;}
            return PartialView("_Comment", comment);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AjaxNote(NoteModel model)
        {
            if (Configinfo.UserPermission && UserInfo == null)
            {
                return JsonReturn("error.userpermission", string.Empty, string.Empty);
            }

            if (ModelState.IsValid)
            {
                if (Configinfo.IfValidateCode && model.ValidationCode != HttpContext.Session["validationCode"].ToString())
                {
                    return JsonReturn("error.validationcode", string.Empty, string.Empty);
                }

                if (UserInfo != null && (model.NoteId > 0 && UserInfo.userid == model.UserId))
                {
                    var article = MyService.GetArticleById(model.NoteId);
                    article.title = Utils.RemoveHtml(string.IsNullOrWhiteSpace(model.Email) ? "" : model.Email).Trim();
                    article.summary = Utils.RemoveHtml(string.IsNullOrWhiteSpace(model.Url) ? "" : model.Url).Trim();
                    article.content = WebUtils.FileterData(model.Content, model.DataType);
                    MyService.UpdateArticle(article);
                    return JsonReturn(string.Empty, string.Empty, article.orderid.ToString());
                }

                if (model.NoteId == 0)
                {
                    var obj = InitialArticleModel.VArticle();
                    obj.title = Utils.RemoveHtml(string.IsNullOrWhiteSpace(model.Email) ? "" : model.Email).Trim();
                    obj.summary = Utils.RemoveHtml(string.IsNullOrWhiteSpace(model.Url) ? "" : model.Url).Trim();
                    obj.content = WebUtils.FileterData(model.Content, model.DataType);
                    obj.layer = 0;
                    obj.parentid = model.ParentId;
                    obj.userid = model.UserId;
                    obj.username = model.UserName.Trim();
                    obj.cateid = model.CategoryId;
                    obj.typeid = 6;
                    var re = MyService.AddArticle(obj);
                    var newNote = MyService.GetArticleById(re);
                    SendNoteReplyEmail(newNote);
                    return JsonReturn(string.Empty, string.Empty, newNote.orderid.ToString());
                }
            }
            return JsonReturn("error.modelvalid", string.Empty, string.Empty);
        }

        public ActionResult NoteList(int id, string orderType, int? p)
        {
            var pageNo = (p ?? 1) > 0 ? (p ?? 1) : 1;
            var pager = new Pager { PageNo = pageNo, PageSize = Configinfo.NotePagerCount };
            pager = MyService.GetFloorNotePaging(pager, id, orderType);
            var noteListViewModel = new NoteListViewModel
                                       {
                                           NotePagerInfo = pager
                                       };

            return PartialView("_NoteList", noteListViewModel);
        }

        public ActionResult GetNoteListByOrderId(int id, string orderType, long orderid)
        {
            var pager = new Pager { PageSize = Configinfo.NotePagerCount };
            pager = MyService.GetFloorNoteByOrderId(pager, id, orderid, orderType);
            var noteListViewModel = new NoteListViewModel
            {
                NotePagerInfo = pager
            };

            return PartialView("_NoteList", noteListViewModel);
        }

        [HttpPost]
        public ActionResult DeleteNote(int id,string orderType)
        {
            var article = MyService.GetArticleById(id);
            MyService.DelArticle(article);
            return GetNoteListByOrderId(article.cateid, orderType, article.orderid);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AjaxComment(CommentModel model)
        {
            if (Configinfo.UserPermission && UserInfo==null)
            {
                return JsonReturn("error.userpermission", string.Empty, string.Empty);
            }

            if (ModelState.IsValid)
            {
                if (Configinfo.IfValidateCode && model.ValidationCode != HttpContext.Session["validationCode"].ToString())
                {
                    return JsonReturn("error.validationcode", string.Empty, string.Empty);
                }

                if (UserInfo != null && (model.CommentId > 0 && UserInfo.userid == model.UserId))
                {
                    var item = MyService.GetArticleById(model.CommentId);
                    item.title = Utils.RemoveHtml(string.IsNullOrWhiteSpace(model.Email) ? "" : model.Email);
                    item.summary = Utils.RemoveHtml(string.IsNullOrWhiteSpace(model.Url) ? "" : model.Url);
                    item.content = WebUtils.FileterData(model.Content, model.DataType);
                    MyService.UpdateArticle(item);
                    return JsonReturn(string.Empty, string.Empty, item.orderid.ToString());
                }

                if (model.CommentId==0)
                {
                    var obj = InitialArticleModel.VArticle();
                    var article = MyService.GetArticleById(model.CommentArticleId);
                    obj.title = Utils.RemoveHtml(string.IsNullOrWhiteSpace(model.Email) ? "" : model.Email).Trim();
                    obj.summary = Utils.RemoveHtml(string.IsNullOrWhiteSpace(model.Url) ? "" : model.Url).Trim();
                    obj.content = WebUtils.FileterData(model.Content, model.DataType);
                    obj.articleid = model.CommentArticleId;
                    obj.layer = 1;
                    obj.parentid = model.ParentId;
                    obj.userid = model.UserId;
                    obj.username = model.UserName.Trim();
                    obj.typeid = article.typeid;
                    obj.cateid = article.cateid;
                    var re = MyService.AddArticle(obj);
                    var newComment = MyService.GetArticleById(re);
                    SendCommentReplyEmail(newComment);
                    return JsonReturn(string.Empty, string.Empty, newComment.orderid.ToString());
                }
            }
            return JsonReturn("error.modelvalid", string.Empty, string.Empty);
        }

        public ActionResult CommentList(long id,string orderType, int? p)
        {
            var pageNo = (p ?? 1) > 0 ? (p ?? 1) : 1;
            var pager = new Pager { PageNo = pageNo, PageSize = Configinfo.CommentPagerCount };
            pager = MyService.GetFloorCommentPaging(pager, id, orderType);
            var commentListViewModel = new CommentListViewModel
                                       {
                                           CommentPagerInfo = pager
                                       };
            return PartialView("_CommentList", commentListViewModel);
        }

        public ActionResult GetCommentListByOrderId(long id,string orderType, long orderid)
        {
            var pager = new Pager { PageSize = Configinfo.CommentPagerCount };
            pager = MyService.GetFloorCommentPagingByOrderId(pager, id, orderid, orderType);
            var commentListViewModel = new CommentListViewModel
            {
                CommentPagerInfo = pager
            };
            return PartialView("_CommentList", commentListViewModel);
        }

        [HttpPost]
        public ActionResult DeleteComment(int id, string orderType)
        {
            var article = MyService.GetArticleById(id);
            MyService.DelArticle(article);
            return GetCommentListByOrderId(article.articleid, orderType, article.orderid);
        }

        public ActionResult ArticleAjaxInfo(long id, long? orderId, string orderType)
        {
            var varticle = MyService.GetVArticleById(id);
            varticle.viewcount++;
            MyService.UpdateArticle(varticle);

            var pager = new Pager { PageNo = 1, PageSize = Configinfo.CommentPagerCount };
            pager = (orderId ?? 0) > 0 ? MyService.GetFloorCommentPagingByOrderId(pager, id, orderId ?? 0, orderType) : MyService.GetFloorCommentPaging(pager, id, orderType);

            var prearticle = MyService.GetPreviewVArticle(varticle);
            var nextarticle = MyService.GetNextVArticle(varticle);
            var previousLink = res.NoMore;
            var nextLink = res.NoMore;
            if (prearticle != null && !string.IsNullOrWhiteSpace(prearticle.title))
            {
                previousLink = "<a href=\"" + WebUtils.GetYqUrl(prearticle) + "\">" + prearticle.title.Replace("'", "\\'") + "</a>";
            }
            if (nextarticle != null && !string.IsNullOrWhiteSpace(nextarticle.title))
            {
                nextLink = "<a href=\"" + WebUtils.GetYqUrl(nextarticle) + "\">" + nextarticle.title.Replace("'", "\\'") + "</a>";
            }

            var articleAjaxInfoViewModel = new ArticleAjaxInfoViewModel
            {
                PreviousLink = MvcHtmlString.Create(previousLink),
                NextLink = MvcHtmlString.Create(nextLink),
                ViewCount = varticle.viewcount,
                CommentCount = pager.Amount,
                FavorCount = varticle.favor,
                AgainstCount = varticle.against,
                CommentsInfo = new CommentListViewModel{CommentPagerInfo = pager}
            };
            return PartialView("_ArticleAjaxInfo", articleAjaxInfoViewModel);
        }

        [HttpPost]
        public ActionResult ValidationCodeJudge(string code)
        {
            if (Configinfo.IfValidateCode && code != HttpContext.Session["validationCode"].ToString())
            { return Content("0", "text/html;charset=UTF-8"); }
            return Content("1", "text/html;charset=UTF-8");
        }

        [HttpPost]
        public ActionResult AjaxVote(int id, int vote)
        {
            if (Configinfo.UserPermission && UserInfo == null)
            {
                return JsonReturn("error.userpermission","please log in first!", string.Empty);
            }
            if (IsVoted(id))
            {
                return JsonReturn("error.voted", res.Vote_Tip_Voted, string.Empty); 
            }
            if (Request.Cookies["article.vote"] == null)
            {
                var voteCookie = new HttpCookie("article.vote");
                voteCookie["articleid"] = id + ",";
                voteCookie.Expires = DateTime.Now.AddDays(7d);
                Response.Cookies.Add(voteCookie);
            }
            else
            {
                var cookieString = Request.Cookies["article.vote"]["articleid"];
                var cookies = cookieString.Split(new[] { ',' });
                if (!cookies.Contains(id.ToString()))
                {
                    var voteCookie = new HttpCookie("article.vote");
                    voteCookie["articleid"] = cookieString + id + ",";
                    voteCookie.Expires = DateTime.Now.AddDays(7d);
                    Response.Cookies.Add(voteCookie);
                }
            }
            int re;
            var article = MyService.GetArticleById(id);
            if (vote == 1)
            {
                article.favor++;
                re = article.favor;
            }
            else
            {
                article.against++;
                re = article.against;
            }
            MyService.UpdateArticle(article);
            return JsonReturn("", res.Vote_Tip_Success, re.ToString());
        }

        public Rss Rss()
        {
            return new Rss(Configinfo.Webtitle, Configinfo.WebDescription, GetRootPath() + "/rss/", MyService.GetRss(MyService.GetVArticles(1, 0, 20).ToList()));
        }

        public Rss CommentRss()
        {
            return new Rss(Configinfo.Webtitle + " - " + res.LatestComments, Configinfo.WebDescription, GetRootPath() + "/commentrss/", MyService.GetCommentRss(MyService.GetReplyArticles(1, 0, 20).ToList()));
        }

        public ActionResult JsonArticleList(int cid, int? p)
        {
            var pager = new Pager { PageNo = p ?? 1 };

            if (cid > 0)
            {
                var category = MyService.GetCategoryById(cid);
                pager.PageSize = Configinfo.CatePagerCount;
                pager = category.SubCount > 0 ? MyService.GetArticlePaging(pager, 0,cid, MyService.GetCategoryIds(cid)) : MyService.GetArticlePaging(pager, 0, cid);
            }
            else
            {
                pager.PageSize = Configinfo.IndexPagerCount;
                pager = MyService.GetArticlePaging(pager, 1, 0);
            }

            var hash = new Hashtable();
            hash["pageinfo"] = pager.PageCount > (p ?? 1) ? (p ?? 1) + 1 : 0;
            hash["articles"] = pager.Entity;
            return Json(hash, "text/html;charset=UTF-8");
        }

        public ActionResult GetOneNote(long id)
        {
            var article = MyService.GetArticleById(id);
            var note = new NoteModel
                           {
                               CategoryId = article.cateid,
                               NoteId = id,
                               UserId = article.userid,
                               UserName = article.username.Trim(),
                               Email = article.title.Trim(),
                               Url = article.summary.Trim(),
                               Content = article.content
                           };
            return Json(note, "text/html;charset=UTF-8");
        }

        public ActionResult GetOneComment(long id)
        {
            var article = MyService.GetArticleById(id);
            var comment = new CommentModel
            {
                CommentArticleId = article.articleid,
                CommentId = id,
                UserId = article.userid,
                UserName = article.username.Trim(),
                Email = article.title.Trim(),
                Url = article.summary.Trim(),
                Content = article.content
            };
            return Json(comment, "text/html;charset=UTF-8");
        }

        [HttpPost]
        public ActionResult AjaxGetArticleList(string data)
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var paras = serializer.Deserialize<AjaxArticleListParamsModel>(data);
            ViewBag.ArticleListParasInfo = paras;

            var pager = new Pager { PageNo = paras.PageId ,PageSize = Configinfo.CatePagerCount};
            switch (paras.ArticleListType)
            {
                case "category":
                        var category = MyService.GetCategoryById(paras.CategoryId);
                        pager = category.SubCount > 0
                            ? MyService.GetArticlePaging(pager, 0, paras.CategoryId, MyService.GetCategoryIds(paras.CategoryId), paras.Commend, paras.Order)
                            : MyService.GetArticlePaging(pager, 0, paras.CategoryId, paras.Commend, order: paras.Order, articleListType: paras.ArticleListType);
                        break;
                case "tag":
                    pager = MyService.GetTagArticlePaging(pager, 1, paras.Tag.Replace("@", "."), paras.Commend, paras.Order);
                    break;
                case "archives":
                    pager = MyService.GetArchivesArticlePaging(pager, 1, paras.Year, paras.Month, paras.Day, paras.Commend, paras.Order);
                    break;
                case "search":
                    pager = MyService.GetKeySearchPaging(pager, 1, paras.SearchKey, paras.Commend, paras.Order);
                    break;
                case "author":
                    pager = MyService.GetAuthorArticlePaging(pager, 1, paras.AuthorName, paras.Commend, paras.Order);
                    break;
                default:
                    pager.PageSize = Configinfo.IndexPagerCount;
                    pager = MyService.GetArticlePaging(pager, 1, 0, paras.Commend, order: paras.Order, articleListType: paras.ArticleListType);
                    break;
            }
            return PartialView("_ArticleList", pager);
        }

        public ActionResult AlbumByKey(string key)
        {
            long id = Utils.StrToInt(key);
            if (id > 0)
            { return Album(id); }
            var varticle = MyService.GetVArticleByReName(key);
            return varticle != null ? Album(varticle.id) : View("Error");
        }

        public ActionResult Album(long id)
        {
            var album = GetAlbumViewModel(id);
            ViewData["AllAlbums"] = MyService.GetAlbums(Convert.ToInt32(album.Category.Path.Split(',')[0]));
            return View("Album", album);
        }

        public ActionResult AjaxCommentForm()
        {
            var comment = new CommentModel
                                       {
                                           CommentId = 0,
                                           DataType = 1,
                                           UserId = UserInfo == null ? 0 : UserInfo.userid,
                                           UserName = UserInfo == null ? string.Empty : UserInfo.username
                                       };

            return PartialView("_Comment", comment);   
        }

        private JsonResult JsonReturn(string error, string message, string value)
        {
            var hash = new Hashtable();
            hash["error"] = error;
            hash["message"] = message;
            hash["value"] = value;
            return Json(hash, "text/html;charset=UTF-8");
        }

        private bool IsVoted(long articleid)
        {
            if (Request.Cookies["article.vote"] == null)
            { return false; }
            var cookieString = Request.Cookies["article.vote"]["articleid"];
            if (string.IsNullOrWhiteSpace(cookieString))
            { return false; }
            var cookies = cookieString.Split(new[] { ',' });
            return cookies.Contains(articleid.ToString());
        }

        private AlbumViewModel GetAlbumViewModel(long id)
        {
            var varticleViewModel = ConvertToAlbumViewModel(MyService.GetAlbum(id));
            varticleViewModel.Comment = new CommentModel
            {
                CommentArticleId = varticleViewModel.ArticleInfo.articleid,
                CommentId = 0,
                DataType = 1,
                UserId = UserInfo == null ? 0 : UserInfo.userid,
                UserName = UserInfo == null ? string.Empty : UserInfo.username
            };
            return varticleViewModel;
        }

        private AlbumViewModel ConvertToAlbumViewModel(AlbumModel album)
        {
            return new AlbumViewModel
            {
                WebTitle = WebUtils.MyString(album.Varticle.seotitle, album.Varticle.title),
                WebPath = MyService.GetCategoryPathUrl(album.Varticle.catepath),
                Seo = WebUtils.GetSeoInfo(album.Varticle),
                ArticleInfo = album.Varticle,
                AlbumPath = MyService.GetCategoryPathUrl2(album.Varticle.catepath),
                PhotoList = album.ImageList,
                ImgCount = album.ImgCount,
                Cover = album.Cover,
                Category = MyService.GetCategoryById(album.Varticle.cateid)
            };
        }

        private ArticleViewModel GetArticleViewModel(long id)
        {
            var varticleViewModel = ConvertToArticleViewModel(MyService.GetVArticleById(id));
            varticleViewModel.Comment = new CommentModel
            {
                CommentArticleId = varticleViewModel.ArticleInfo.articleid,
                CommentId = 0,
                DataType = 1,
                UserId = UserInfo == null ? 0 : UserInfo.userid,
                UserName = UserInfo == null ? string.Empty : UserInfo.username
            };
            return varticleViewModel;
        }

        private ArticleViewModel ConvertToArticleViewModel(blog_varticle varticle)
        {
            return new ArticleViewModel
            {
                WebTitle = WebUtils.MyString(varticle.seotitle, varticle.title),
                WebPath = MyService.GetCategoryPathUrl(varticle.catepath),
                Seo = WebUtils.GetSeoInfo(varticle),
                ArticleInfo = varticle,
                Category = MyService.GetCategoryById(varticle.cateid)
            };
        }

        private void SendCommentReplyEmail(blog_article comment)
        {
            if (Configinfo.IfSendReplyEmail == 2)
            {
                return;
            }
            if (comment.parentid == 0)
            {
                return;
            }
            var parentItem = MyService.GetArticleById(comment.parentid);
            if (parentItem.userid == 0 && string.IsNullOrEmpty(parentItem.title.Trim()))
            {
                return;
            }
            var email = parentItem.title.Trim();
            if (parentItem.userid > 0)
            {
                var user = MyService.GetUserInfoById(parentItem.userid);

                if (string.IsNullOrEmpty(user.email.Trim()) || !(user.isSendEmail ?? false))
                {
                    return;
                }
                email = user.email.Trim();
            }
            if (!Utils.IsValidEmail(email))
            { return; }
            var authorinfo = comment.username.Trim();
            if (comment.userid > 0)
            {
                authorinfo = "<a href=\"" + WebUtils.GetWebRootPath() + "/u/" + HttpUtility.UrlEncode(comment.username, System.Text.Encoding.UTF8) + "\" target=\"_blank\">" + authorinfo + "</a>";
            }
            var varticle = MyService.GetArticleById(comment.articleid);
            var emailCulture = Configinfo.IfIndependentContentViaLang == 1 ? res.Lang : Configinfo.DefaultLang;
            var etitle = "[" + Configinfo.Webtitle + " " + ResourceProvider.R(emailCulture, "Comment.Reply.EmailTemplateTitle") + "]" + " Re:" + varticle.title;
            var emailFormat = ResourceProvider.R(emailCulture, "Comment.Reply.EmailTemplate");
            var econtent = string.Format(emailFormat,
                              varticle.title,
                              Utils.CutString(Utils.RemoveHtml(parentItem.content), 100, "..."),
                              authorinfo,
                              comment.createdate.ToString("yyyy/MM/dd HH:mm"),
                              comment.content,
                              WebUtils.GetYqUrl(varticle),
                              WebUtils.GetYqUrl(varticle) + "?f=" + comment.orderid
                              );
            new SendDelegate(WebUtils.SendSysMail).BeginInvoke(email, etitle, econtent, null, null);
        }

        private void SendNoteReplyEmail(blog_article note)
        {
            if (Configinfo.IfSendReplyEmail == 2)
            {
                return;
            }
            if (note.parentid == 0)
            {
                return;
            }
            var parentItem = MyService.GetArticleById(note.parentid);
            if (parentItem.userid == 0 && string.IsNullOrEmpty(parentItem.title.Trim()))
            {
                return;
            }
            var email = parentItem.title.Trim();
            if (parentItem.userid > 0)
            {
                var user = MyService.GetUserInfoById(parentItem.userid);

                if (string.IsNullOrEmpty(user.email.Trim()) || !(user.isSendEmail ?? false))
                {
                    return;
                }
                email = user.email.Trim();
            }
            if (!Utils.IsValidEmail(email))
            { return; }
            var authorinfo = note.username.Trim();
            if (note.userid > 0)
            {
                authorinfo = "<a href=\"" + WebUtils.GetWebRootPath() + "/u/" + HttpUtility.UrlEncode(note.username, System.Text.Encoding.UTF8) + "\" target=\"_blank\">" + authorinfo + "</a>";
            }
            var emailCulture = Configinfo.IfIndependentContentViaLang == 1 ? res.Lang : Configinfo.DefaultLang;
            var etitle = "[" + Configinfo.Webtitle + " " + ResourceProvider.R(emailCulture, "Note.Reply.EmailTemplateTitle") + "]";
            var emailFormat = ResourceProvider.R(emailCulture, "Note.Reply.EmailTemplate");

            var category = MyService.GetCategoryById(note.cateid);

            var econtent = string.Format(emailFormat,
                              Utils.CutString(Utils.RemoveHtml(parentItem.content), 100, "..."),
                              authorinfo,
                              note.createdate.ToString("yyyy/MM/dd HH:mm"),
                              note.content,
                              WebUtils.GetCateUrl(category) + "?f="+note.orderid
                              );
            new SendDelegate(WebUtils.SendSysMail).BeginInvoke(email, etitle, econtent, null, null);
        }
    }
}
