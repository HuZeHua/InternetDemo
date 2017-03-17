using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using System.IO;
using System.Collections;
using System.Globalization;
using System.Text;
using Yqblog.Data;
using Yqblog.General;
using Yqblog.Models;
using res = Resource.Admin.Admin;

namespace Yqblog.Controllers
{
    [AdminAuthFilter]
    public class AdminController : BaseController
    {
        delegate void CreatePageDelegate(int typeid, long id, string rename, DateTime d,string rootPath);

        #region AdminIndex

        public ActionResult AdminIndex()
        {
            return View();
        }

        #endregion

        #region Admin Article

        public ActionResult AdminArticle(int? pageNo,int? tid,int? layer,int? cid,string sort,string order)
        {
            var typeid = tid ?? 1;
            var layerid = layer ?? 0;
            var cateid = cid ?? 0;
            var navSelectedItem = string.Empty;
            if (layerid == 1)
            {
                navSelectedItem = "comment";  
            }
            else
            {
                switch (typeid)
                {
                    case 1:
                        navSelectedItem = "article";
                        break;
                    case 2:
                        navSelectedItem = "singlePage";
                        break;
                    case 4:
                        navSelectedItem = "album";
                        break;
                    case 6:
                        navSelectedItem = "message";
                        break;
                    case 5:
                        navSelectedItem = "posts";
                        break;
                    case 7:
                        navSelectedItem = "customArea";
                        break;
                    case 8:
                        navSelectedItem = "customGlobalArea";
                        break;
                }
            }

            ViewBag.NavSelectedItem = navSelectedItem;
            var pager = new Pager {PageNo = pageNo ?? 1, PageSize = 20};
            pager = cateid>0 ? MyService.GetReplyPaging(pager, typeid, MyService.GetCategoryIds(cateid), layerid, sort, string.IsNullOrWhiteSpace(order)?"desc":order) : MyService.GetReplyPaging(pager, typeid, cateid, layerid, sort, string.IsNullOrWhiteSpace(order) ? "desc" : order);
            ViewBag.PageNo = pageNo ?? 1;
            ViewBag.PageCount = pager.PageCount;
            ViewBag.TypeId = typeid;
            ViewBag.LayerId = layerid;
            ViewBag.Cid = cateid;
            ViewBag.Sort = sort;
            ViewBag.Order = order;
            ViewBag.Amount = pager.Amount;
            var catelist=MyService.GetFCategoryList(typeid.ToString(), "", " -- ");
            catelist.Insert(0, new CategoryModel
            {
                CateId = 0,
                ViewName = res.SelectSubcategories
            });
            ViewBag.CurrentItem = layerid > 0 ? "l" + layerid + "_" + (typeid < 5 ? "1" : typeid.ToString()) : "t" + typeid.ToString();
            ViewData["CateId"] = new SelectList(catelist, "CateId", "ViewName", cateid);
            return View(pager.Entity);
        }
        
        public ActionResult AdminAdd(int? tid,int? cid)
        {
            var typeid = tid ?? 0;
            var cateid = cid ?? 0;
            var navSelectedItem = string.Empty;
            switch (tid)
            {
                case 1:
                    navSelectedItem = "article";
                    break;
                case 2:
                    navSelectedItem = "singlePage";
                    break;
                case 4:
                    navSelectedItem = "album";
                    break;
                case 6:
                    navSelectedItem = "message";
                    break;
                case 5:
                    navSelectedItem = "posts";
                    break;
                case 7:
                    navSelectedItem = "customArea";
                    break;
                case 8:
                    navSelectedItem = "customGlobalArea";
                    break;
            }
            ViewBag.NavSelectedItem = navSelectedItem;
            var catelist=MyService.GetFCategoryList(typeid.ToString(), "", " -- ");
            var defaultcateid = cateid;
            if (catelist.Count > 0)
            {
                defaultcateid = catelist.First().CateId;
            }
            ViewData["CateId"] = new SelectList(catelist, "CateId", "ViewName", cid);
            ViewBag.DefaultCateId = defaultcateid;
            ViewBag.cid = cid;
            ViewBag.tid = tid;
            return typeid == 4 ? View("AdminAlbumAdd") : View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AdminAdd(ArticleModel model)
        {
            if (ModelState.IsValid)
            {
                var cateid = model.CateId;
                if (cateid == 0)
                {
                    var newcate = new CategoryModel
                    {
                        CateName = "syscreate",
                        IsIndex = true,
                        IsNav = false,
                        ListNum = 0,
                        ParentId = 0,
                        ReName = string.Empty,
                        CustomView = string.Empty,
                        ReplyPermit = 2,
                        Status = 1,
                        Type = model.TypeId,
                        Description = string.Empty,
                        SubCount = 0
                    };

                    cateid = AddNewCategory(newcate);
                }
                blog_varticle obj = InitialArticleModel.VArticle();
                CategoryModel category = MyService.GetCategoryById(cateid);
                obj.typeid = category.Type;
                obj.cateid = model.CateId;
                obj.catepath = category.Path;
                obj.title = Utils.FileterStr(model.Title);
                obj.summary = string.IsNullOrWhiteSpace(model.Summary) ? "" : model.Summary;
                obj.content = Utils.DownloadImages(model.Content, "~/upload/article/", GetRootPath());
                obj.tags = (string.IsNullOrWhiteSpace(model.Tags) ? "" : model.Tags).Trim();
                obj.replypermit = model.ReplyPermit;
                obj.seodescription = Utils.RemoveHtml((string.IsNullOrWhiteSpace(model.SeoDescription) ? "" : model.SeoDescription)).Trim();
                obj.seokeywords = Utils.RemoveHtml((string.IsNullOrWhiteSpace(model.Seokeywords) ? "" : model.Seokeywords)).Trim();
                obj.seometas = string.IsNullOrWhiteSpace(model.SeoMetas) ? "" : model.SeoMetas;
                obj.seotitle = Utils.RemoveHtml((string.IsNullOrWhiteSpace(model.SeoTitle) ? "" : model.SeoTitle)).Trim();
                obj.rename = Utils.RemoveHtml((string.IsNullOrWhiteSpace(model.ReName) ? "" : model.ReName)).Trim();
                obj.status = model.Status;
                obj.userid = MyService.GetUserInfo(UserInfo.username).UserId;
                obj.username = UserInfo.username;
                obj.iscommend = model.IsCommend;
                obj.istop = model.IsTop;
                obj.isindextop = model.IsIndexTop;
                var re=MyService.AddArticle(obj);
                var createHtml = Configinfo.IfHtmlCreated == 1 || Configinfo.IfWebStatic == 1;
                if (createHtml && category.Type==1 && obj.status == 1)
                {
                    new CreatePageDelegate(WebUtils.CreateHtml).BeginInvoke(obj.typeid, re, obj.rename, obj.createdate, GetRootPath(), null, null);
                }

                return RedirectToAction("AdminArticle", new { tid = obj.typeid, cid = obj.cateid });
            }
            return View(model);
        }

        public ActionResult AdminEdit(int id)
        {
            var obj = MyService.GetVArticleById(id);
            var model = new ArticleModel
                            {
                                Id = obj.id,
                                CateId = obj.cateid,
                                Title = Utils.FileterStr(obj.title),
                                Summary = string.IsNullOrWhiteSpace(obj.summary) ? "" : obj.summary,
                                Content = string.IsNullOrWhiteSpace(obj.content) ? "" : obj.content,
                                Tags = string.IsNullOrWhiteSpace(obj.tags) ? "" : obj.tags,
                                SeoDescription =
                                    string.IsNullOrWhiteSpace(obj.seodescription) ? "" : obj.seodescription,
                                Seokeywords =
                                    string.IsNullOrWhiteSpace(obj.seokeywords) ? "" : obj.seokeywords,
                                SeoMetas = string.IsNullOrWhiteSpace(obj.seometas) ? "" : obj.seometas,
                                SeoTitle = string.IsNullOrWhiteSpace(obj.seotitle) ? "" : obj.seotitle,
                                ReName = WebUtils.MyString(obj.rename),
                                Status = obj.status,
                                ReplyPermit = obj.replypermit,
                                IsCommend = obj.iscommend,
                                IsTop = obj.istop,
                                IsIndexTop = obj.isindextop,
                                CreateDate = obj.createdate,
                                ArticleTypeId = obj.typeid
                            };

            var navSelectedItem = string.Empty;
            switch (obj.typeid)
            {
                case 1:
                    navSelectedItem = "article";
                    break;
                case 2:
                    navSelectedItem = "singlePage";
                    break;
                case 4:
                    navSelectedItem = "album";
                    break;
                case 6:
                    navSelectedItem = "message";
                    break;
                case 5:
                    navSelectedItem = "posts";
                    break;
                case 7:
                    navSelectedItem = "customArea";
                    break;
                case 8:
                    navSelectedItem = "customGlobalArea";
                    break;
            }
            ViewBag.NavSelectedItem = navSelectedItem;

            ViewData["CateId"] = new SelectList(MyService.GetFCategoryList(obj.typeid.ToString(), "", " -- "), "CateId", "ViewName", model.CateId);
            if (obj.typeid == 4)
            { return View("AdminAlbumEdit", model); }
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AdminEdit(ArticleModel model)
        {
            if (ModelState.IsValid)
            {
                var isResetThumbnail = Request["IsResetThumbnail"];
                var obj = InitialArticleModel.VArticle();
                var category = MyService.GetCategoryById(model.CateId);
                obj.id = model.Id;
                obj.typeid = category.Type;
                obj.cateid = model.CateId;
                obj.catepath = category.Path;
                obj.title = Utils.FileterStr(model.Title);
                obj.summary = Utils.FileterStr(model.Summary);
                obj.content = Utils.FileterStr(Utils.DownloadImages(model.Content, "~/upload/article/", GetRootPath()));
                obj.tags = (string.IsNullOrWhiteSpace(model.Tags) ? "" : model.Tags).Trim();
                obj.replypermit = model.ReplyPermit;
                obj.seodescription = Utils.RemoveHtml((string.IsNullOrWhiteSpace(model.SeoDescription) ? "" : model.SeoDescription)).Trim();
                obj.seokeywords = Utils.RemoveHtml((string.IsNullOrWhiteSpace(model.Seokeywords) ? "" : model.Seokeywords)).Trim();
                obj.seometas = string.IsNullOrWhiteSpace(model.SeoMetas) ? "" : model.SeoMetas;
                obj.seotitle = Utils.RemoveHtml((string.IsNullOrWhiteSpace(model.SeoTitle) ? "" : model.SeoTitle)).Trim();
                obj.rename = Utils.RemoveHtml((string.IsNullOrWhiteSpace(model.ReName) ? "" : model.ReName)).Trim();
                obj.status = model.Status;
                obj.userid = MyService.GetUserInfo(UserInfo.username).UserId;
                obj.username = UserInfo.username;
                obj.iscommend = model.IsCommend;
                obj.istop = model.IsTop;
                obj.isindextop = model.IsIndexTop;

                if (obj.typeid == 1 && obj.status == 2)
                {
                    var varticle = MyService.GetArticleById(obj.id);
                    if (varticle.status==1)
                    {WebUtils.DeleteHtml(obj.id, varticle.rename, varticle.createdate);}
                }

                MyService.UpdateVArticle(obj);
                var createHtml = Configinfo.IfHtmlCreated == 1 || Configinfo.IfWebStatic == 1;
                if (createHtml && category.Type==1 && obj.status == 1)
                {
                    new CreatePageDelegate(WebUtils.CreateHtml).BeginInvoke(obj.typeid, obj.id, obj.rename, model.CreateDate, GetRootPath(), null, null);
                }

                if (isResetThumbnail != null && isResetThumbnail=="true")
                {
                    ResetThumbnails(obj.content);
                }

                return RedirectToAction("AdminArticle", new { tid = obj.typeid, cid = obj.cateid });
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult AdminDel(int id, int parentid)
        {
            try
            {
                var obj = new blog_article {id = id, parentid = parentid};
                var varticle = MyService.GetArticleById(id);
                if (parentid == 0 && varticle.typeid==1 && varticle.status!=2)
                {
                    WebUtils.DeleteHtml(varticle.id, varticle.rename, varticle.createdate);
                }
                MyService.RemoveArticle(obj);
            }
            catch (Exception)
            {
                return Content(res.DeleteFailed, "text/html;charset=UTF-8");
            }
            return Content(res.DeletedSuccessfully, "text/html;charset=UTF-8");
        }

        #endregion

        #region Admin Category

        public List<CategoryModel> RefreshCateList(List<CategoryModel> lst)
        {
            var newlst = new List<CategoryModel>();
            foreach (var c in lst)
            {
                var category = c;
                category.Path = GetCategoryPath(lst, category);
                category.SubCount = GetSubCount(lst, category.CateId);
                newlst.Add(category);
            }
            return newlst;
        }

        public void SaveCateInfo(List<CategoryModel> lst)
        {
            var langlst = new List<CategoryLangModel>();

            if (Configinfo.IfIndependentContentViaLang == 1)
            {
                var jsonstr = Utils.GetJson(lst);
                var fileViaLang = System.Web.HttpContext.Current.Server.MapPath("~/Content/js/Category." + Resource.Models.Web.Web.Lang + ".js");
                using (var writer = new StreamWriter(fileViaLang, false, Encoding.UTF8))
                {
                    writer.Write("var category = " + jsonstr);
                }

                if (Resource.Models.Web.Web.Lang == Configinfo.DefaultLang)
                {
                    var file = System.Web.HttpContext.Current.Server.MapPath("~/Content/js/Category.js");
                    using (var writer = new StreamWriter(file, false, Encoding.UTF8))
                    {
                        writer.Write("var category = " + jsonstr);
                    }
                    SaveCateLangInfo(lst); 
                }
            }
            else
            {
                List<CategoryModel> newlst;
                if (Resource.Models.Web.Web.Lang != Configinfo.DefaultLang)
                {
                    var jsonnavlang =
                        Utils.GetFileSource("~/Content/js/Category.Lang." + Configinfo.DefaultLang + ".js")
                             .Replace("var categorylang =", "")
                             .Trim()
                             .Replace("\n", "");
                    if (jsonnavlang != "")
                    {
                        langlst = Utils.ParseFromJson<List<CategoryLangModel>>(jsonnavlang);
                    }

                    newlst = (from a in lst
                              join b in langlst
                                  on a.CateId equals b.CateId into temp
                              from t in temp.DefaultIfEmpty()
                              select new CategoryModel
                                         {
                                             CateId = a.CateId,
                                             CateName = t == null ? a.CateName : t.CateName,
                                             Type = a.Type,
                                             ListNum = a.ListNum,
                                             ReplyPermit = a.ReplyPermit,
                                             ParentId = a.ParentId,
                                             IsNav = a.IsNav,
                                             IsIndex = a.IsIndex,
                                             Status = a.Status,
                                             ReName = a.ReName,
                                             CustomView = t == null ? a.CustomView : t.CustomView,
                                             SubCount = a.SubCount,
                                             OrderId = a.OrderId,
                                             Path = a.Path,
                                             HomePageKey = a.HomePageKey
                                         }).ToList();
                }
                else
                {
                    newlst = lst;
                }

                var jsonstr = Utils.GetJson(newlst);
                var file = System.Web.HttpContext.Current.Server.MapPath("~/Content/js/Category.js");

                using (var writer = new StreamWriter(file, false, Encoding.UTF8))
                {
                    writer.Write("var category = " + jsonstr);
                }

                var fileViaLang = System.Web.HttpContext.Current.Server.MapPath("~/Content/js/Category." + Configinfo.DefaultLang + ".js");
                using (var writer = new StreamWriter(fileViaLang, false, Encoding.UTF8))
                {
                    writer.Write("var category = " + jsonstr);
                }
                SaveCateLangInfo(lst);
            }
            RefreshConfigVersionNo();
        }

        public void SaveCateLangInfo(List<CategoryModel> lst)
        {
            var lstlang = (from p in lst select new CategoryLangModel { CateId = p.CateId, CateName = p.CateName, CustomView = p.CustomView }).ToList();
            string jsonstr = Utils.GetJson(lstlang);
            string file = System.Web.HttpContext.Current.Server.MapPath("~/Content/js/" + WebUtils.GetCategoryLangName());

            using (var writer = new StreamWriter(file, false, Encoding.UTF8))
            {
                writer.Write("var categorylang = " + jsonstr);
            }
        }

        public ActionResult AdminCategorySort()
        {
            var lst = MyService.GetFCategoryList(" -- ");
            return View(lst);
        }

        [HttpPost]
        public ActionResult AdminCategorySort(string ids)
        {
            var orderlst = new List<CategoryModel>();
            var newlst = new List<CategoryModel>();
            var arrId = ids.Trim(',').Split(',');
            for (var i = 0; i < arrId.Length; i++)
            {
                var orderid = i + 1;
                var category = MyService.GetCategoryById(Utils.StrToInt(arrId[i]));
                category.OrderId = orderid;
                orderlst.Add(category);
            }
            GetNewCategoryList(orderlst, ref newlst, 0);
            SaveCateInfo(newlst);

            var re = "";
            var lst = MyService.GetFCategoryList(" -- ");
            foreach (var c in lst)
            {
                var classstr = "default";
                if(c.ParentId == 0)
                { classstr += " cl_root"; }
                if (c.Type == 7 || c.Type == 8)
                {
                    classstr += " hide"; 
                }
                re += "<li id=\"" + c.CateId + "\" class=\"" + classstr + "\">" + c.ViewName + "</li>";
            }
            return Content(re, "text/html;charset=UTF-8");
        }

        public ActionResult AdminCategoryAdd(int id = 0, int tid = 0)
        {
            ViewBag.CateId = id;
            ViewBag.CateName = string.Empty;
            ViewBag.TypeName = string.Empty;
            ViewBag.Tid = tid;
            var typeid = tid;
            if (id > 0)
            {
                var currentcategory = MyService.GetCategoryById(id);
                ViewBag.CateName = currentcategory.CateName;
                typeid = currentcategory.Type;
            }
            ViewBag.TypeId = typeid;
            ViewData["Type"] = new SelectList(WebType.GetTypeList().Where(x => x.IsCustomView), "TypeId", "TypeName", typeid);
            if (typeid > 0)
            {
                ViewBag.TypeName = WebType.GetTypeList().Single(x => x.TypeId == typeid).TypeName;
            }
            var list = MyService.GetFCategoryList("", "", " -- ").Where(m => m.Type == typeid).Where(m => !("," + MyService.GetCategoryIds(typeid) + ",").Contains("," + m.CateId + ",")).ToList();
            list.Add(new CategoryModel { CateId = 0, ViewName = "Root" });
            ViewData["ParentId"] = new SelectList(list, "CateId", "ViewName", id);

            var category = new CategoryModel {ParentId = id,ListNum = 10};
            return View(category);
        }

        [HttpPost]
        public ActionResult AdminCategoryAdd(CategoryModel model)
        {
            var category = new CategoryModel
                               {
                                   CateName = model.CateName,
                                   IsIndex = model.IsIndex,
                                   IsNav = model.IsNav,
                                   ListNum = model.ListNum == 0 ? 10 : model.ListNum,
                                   ParentId = model.ParentId,
                                   ReName =
                                       string.IsNullOrWhiteSpace(model.ReName) ? string.Empty : model.ReName,
                                   CustomView =
                                       string.IsNullOrWhiteSpace(model.CustomView)
                                           ? string.Empty
                                           : model.CustomView,
                                   HomePageKey =
                                       string.IsNullOrWhiteSpace(model.HomePageKey)
                                           ? string.Empty
                                           : model.HomePageKey,
                                   ReplyPermit = model.ReplyPermit,
                                   Status = model.Status,
                                   Type = model.Type,
                                   Description = WebUtils.MyString(model.Description),
                                   SubCount = 0
                               };

            var cateid = AddNewCategory(category);

            return Content(Url.Action("AdminAdd", "Admin", new { tid = category.Type, cid = cateid }), "text/html;charset=UTF-8");
        }

        public ActionResult AdminCategoryEdit(int id, int? tab)
        {
            var category = MyService.GetCategoryById(id);
            ViewBag.TypeId = category.Type;
            ViewBag.TypeName = WebType.GetTypeList().Single(x => x.TypeId == category.Type).TypeName;
            ViewData["Type"] = new SelectList(WebType.GetTypeList(), "TypeId", "TypeName", category.Type);
            var list = MyService.GetFCategoryList("", "", " -- ").Where(m => m.Type == category.Type).Where(m => !("," + MyService.GetCategoryIds(category.CateId) + ",").Contains("," + m.CateId + ",")).ToList();
            list.Add(new CategoryModel { CateId = 0, ViewName = "Root" });
            ViewData["ParentId"] = new SelectList(list, "CateId", "ViewName", category.ParentId);
            ViewBag.Tab = tab ?? 1;
            return View(category);
        }

        [HttpPost]
        public ActionResult AdminCategoryEdit(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var catelist = MyService.GetFCategoryList();
                var newlst = new List<CategoryModel>();
                var newlst2 = new List<CategoryModel>();
                var category = new CategoryModel
                                   {
                                       CateId = model.CateId,
                                       CateName = model.CateName,
                                       IsIndex = model.IsIndex,
                                       IsNav = model.IsNav,
                                       ListNum = model.ListNum,
                                       ParentId = model.ParentId,
                                       ReName =
                                           string.IsNullOrWhiteSpace(model.ReName) ? string.Empty : model.ReName,
                                       CustomView =
                                           string.IsNullOrWhiteSpace(model.CustomView)
                                               ? string.Empty
                                               : model.CustomView,
                                       HomePageKey =
                                           string.IsNullOrWhiteSpace(model.HomePageKey)
                                               ? string.Empty
                                               : model.HomePageKey
                                   };
                if (category.IsIndex && category.HomePageKey == string.Empty && category.Type < 3)
                {
                    category.HomePageKey = "cate" + category.CateId;
                }
                category.ReplyPermit = model.ReplyPermit;
                category.Status = model.Status;
                category.Type = model.Type;
                category.Description = WebUtils.MyString(model.Description);

                category.OrderId = catelist.Count() + 1;
                category.SubCount = 0;
                category.Path = "0";
                var isPathChange = false;

                foreach (var c in catelist)
                {
                    if (category.CateId == c.CateId)
                    {
                        if (category.ParentId == c.ParentId)
                        {
                            category.OrderId = c.OrderId;
                            category.SubCount = c.SubCount;
                            category.Path = c.Path;
                        }
                        newlst.Add(category);
                    }
                    else
                        newlst.Add(c);
                }

                if (category.Path == "0")
                {
                    isPathChange = true;
                    newlst = RefreshCateList(newlst);
                    GetNewCategoryList(newlst, ref newlst2, 0);
                }
                else
                {
                    newlst2 = newlst;
                }
                SaveCateInfo(newlst2);
                if (isPathChange)
                {
                    var newcategory = MyService.GetCategoryById(model.CateId);
                    MyService.BatchUpdateArticlePath(model.CateId, newcategory.Path);
                }
            }
            else
            {
                return Content("error", "text/html;charset=UTF-8");
            }

            return Content(string.Empty, "text/html;charset=UTF-8");
        }

        [HttpPost]
        public ActionResult AdminCategoryDel(int id)
        {
            var catelist = MyService.GetFCategoryList();
            var newlst2 = new List<CategoryModel>();
            var newlst = catelist.Where(c => id != c.CateId).ToList();
            GetNewCategoryList(newlst, ref newlst2, 0);
            newlst2 = RefreshCateList(newlst2);
            SaveCateInfo(newlst2);
            return Content(res.DeletedSuccessfully + " <a href=\"" + Url.Action("AdminCategorySort", "Admin") + "\">" + res.View + "</a>", "text/html;charset=UTF-8");
        }

        #endregion

        #region Admin BaseConfig
        public ActionResult AdminBaseConfig()
        {
            var langList = WebUtils.GetLangList().Select(lang => new MyCheckBox
                                                                     {
                                                                         Text = lang.Key, Value = lang.Value, IsChecked = Array.IndexOf(Configinfo.WebLangList, lang.Value) > -1
                                                                     }).ToList();

            ViewBag.Langs = langList;

            var themelst=new List<SelectItem> {new SelectItem {Key = res.Default, Value = ""}};

            if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("~/Themes")))
            { Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("~/Themes")); }

            var di = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/Themes"));
            var dirs = di.GetDirectories();
            themelst.AddRange(dirs.Select(t => new SelectItem { Key = t.Name, Value = t.Name }));


            ViewData["DefaultLang"] = new SelectList(WebUtils.GetLangList(), "Value", "Key", Configinfo.DefaultLang);
            var contentLangList = new List<PageLang> {new PageLang {Key = "All", Value = "all"}};
            contentLangList.AddRange(WebUtils.GetLangList());
            ViewData["WebContentLang"] = new SelectList(contentLangList, "Value", "Key", Configinfo.WebContentLang);
            ViewData["Theme"] = new SelectList(themelst, "Value", "Key", Configinfo.Theme);

            foreach (var lang in WebUtils.GetLangList())
            {
                ViewData["Theme_" + lang.Value] = new SelectList(themelst, "Value", "Key", WebUtils.GetLangTemplate(lang.Value));
            }
            return View(Configinfo);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AdminBaseConfig(GeneralConfigInfo model)
        {
            try
            {
                var config = Configinfo;
                config.Webtitle = model.Webtitle;
                config.Icp = model.Icp;
                config.IndexPagerCount = model.IndexPagerCount;
                config.CatePagerCount = model.CatePagerCount;
                config.CommentPagerCount = model.CommentPagerCount;
                config.NotePagerCount = model.NotePagerCount;
                config.ForumPagerCount = model.ForumPagerCount;
                config.ThreadPagerCount = model.ThreadPagerCount;
                config.WebDescription = WebUtils.MyString(model.WebDescription).Replace("\r\n", "<br>");
                config.ThumbnailInfo = model.ThumbnailInfo;
                config.Theme = model.Theme;
                config.DefaultLang = model.DefaultLang;
                config.WebContentLang = model.WebContentLang;
                config.MaxSummaryCharCount = model.MaxSummaryCharCount;
                config.AdminEmail = model.AdminEmail;
                config.SmtpServer = model.SmtpServer;
                config.SmtpUser = model.SmtpUser;
                config.SmtpPass = model.SmtpPass;
                config.SmtpPort = model.SmtpPort;
                config.IfSendReplyEmail = model.IfSendReplyEmail;
                config.IfWebStatic = model.IfWebStatic;
                config.IfHtmlCreated = model.IfHtmlCreated;
                config.WebStaticFolderPart1 = model.WebStaticFolderPart1.Trim('/');
                config.WebStaticFolderPart2 = string.IsNullOrEmpty(model.WebStaticFolderPart2) ? "" : model.WebStaticFolderPart2.Trim();
                config.WebStaticFolder = config.WebStaticFolderPart1 + "/{lang}/" + config.WebStaticFolderPart2;
                config.WebLangList = model.WebLangList;
                config.UserPermission = model.UserPermission;
                config.IfValidateCode = model.IfValidateCode;
                config.IfPagingAjax = model.IfPagingAjax;
                config.IfIndependentContentViaLang = model.IfIndependentContentViaLang;
                config.LangTemplateStr = model.LangTemplateStr;
                config.Logo = model.Logo;
                //WebUtils.ChangeTheme(WebUtils.GetLangTemplate(model.DefaultLang, model.LangTemplateStr));
                //var cultureinfo = new CultureInfo(model.DefaultLang == "zh-cn" ? "" : Configinfo.DefaultLang);
                //System.Web.HttpContext.Current.Session["CurrentLanguage"] = cultureinfo;
                GeneralConfigs.Serialiaze(config, Server.MapPath(WebUtils.GetWebConfigPath()));
            }
            catch (Exception)
            {
                return Content(res.ModifyFailed + " <a href=\"" + Url.Action("AdminBaseConfig", "Admin") + "\">" + res.ContinueModify + "</a>", "text/html;charset=UTF-8");
            }
            return Content(res.ModifiedSuccessfully + " <a href=\"" + Url.Action("AdminBaseConfig", "Admin") + "\">" + res.ContinueModify + "</a>", "text/html;charset=UTF-8");
        }

        #endregion

        #region UploadFile
        [HttpPost]
        public ActionResult UploadFile(string dir)
        {
            new Hashtable();
            var savePath = Url.Content("~/upload/attached/");
            var saveUrl = savePath;
            const int maxSize = 1000000; 
            var extTable = new Hashtable
                               {
                                   {"image", "gif,jpg,jpeg,png,bmp"},
                                   {"flash", "swf,flv"},
                                   {"media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb"},
                                   {"file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2"}
                               };
            switch (dir)
            {
                case "image":
                    savePath = Url.Content("~/upload/images/");
                    saveUrl = savePath;
                    break;
                case "flash":
                    break;
                case "media":
                    break;
                case "file":
                    break;
            }
            HttpPostedFileBase file = Request.Files["imgFile"];
            var dirPath = Server.MapPath(savePath);
            var fileName = file.FileName;
            var fileExt = Path.GetExtension(fileName).ToLower();
            
            if (file == null)
                return UploadJsonRe(1, res.UploadFile_Tip1, "");

            if (!Directory.Exists(dirPath))
                return UploadJsonRe(1, res.UploadFile_Tip2, "");

            if (file.InputStream == null || file.InputStream.Length > maxSize)
                return UploadJsonRe(1, res.UploadFile_Tip3, ""); 

            if (string.IsNullOrEmpty(fileExt) || Array.IndexOf(((String)extTable[dir]).Split(','), fileExt.Substring(1).ToLower()) == -1)
                return UploadJsonRe(1, res.UploadFile_Tip4, "");

            var newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt; 
            var filePath = dirPath + newFileName; 
            file.SaveAs(filePath); 
            var fileUrl = saveUrl + newFileName; 

            return UploadJsonRe(0, "", fileUrl);    
        }

        #endregion

        #region Admin User

        public ActionResult AdminUserList(int? pageNo)
        {
            const int pageSize = 20;
            var pageno = pageNo ?? 1;
            var pager = new Pager { PageNo = pageno, PageSize = pageSize };
            ViewBag.PageNo = pageno;
            var users = MyService.GetUserList();
            pager.Amount = users.Count;
            ViewBag.PageCount = pager.PageCount;
            pager.Entity = users.OrderByDescending(a => a.CreateDate).Skip(pageSize * (pageno - 1)).Take(pageSize).AsQueryable();
            return View(pager.Entity);
        }

        public ActionResult AdminManagerList()
        {
            var users = MyService.GetAdminList(); ;
            return View(users);
        }

        [HttpPost]
        public ActionResult AdminDelUser(string user)
        {
            try
            {
                MyService.DeleteUser(MyService.GetUserInfo(user).UserId);
            }
            catch (Exception)
            {
                return Content(res.DeleteFailed, "text/html;charset=UTF-8");
            }
            return Content(res.DeletedSuccessfully, "text/html;charset=UTF-8");
        }

        [HttpPost]
        public ActionResult AdminRemoveUserFromRole(string user)
        {
            MyService.UpdateUserRole(MyService.GetUserInfo(user).UserId,1);
            return Content(res.DeletedSuccessfully, "text/html;charset=UTF-8");
        }

        [HttpPost]
        public ActionResult AdminAddUserToRole(string user)
        {
            MyService.UpdateUserRole(MyService.GetUserInfo(user).UserId, 2);
            return Content(res.OperationSuccessful, "text/html;charset=UTF-8");
        }

        #endregion

        #region Admin HtmlCreate

        public ActionResult AdminHtmlCreate()
        {
            var langList = WebUtils.GetLangList().Where(lang => Array.IndexOf(Configinfo.WebLangList, lang.Value) > -1).ToList();
            langList.Insert(0, new PageLang
            {
                Key = res.Nolimited,
                Value = ""
            });
            var defaultlang = Configinfo.IfIndependentContentViaLang == 1 ? Resource.Models.Web.Web.Lang : "";
            ViewData["LangList"] = new SelectList(langList, "Value", "Key", defaultlang);

            var catelist = MyService.GetFCategoryList("1", "", " -- ");
            catelist.Insert(0, new CategoryModel
            {
                CateId = 0,
                CateName = res.Nolimited
            });
            ViewData["CateId"] = new SelectList(catelist, "CateId", "CateName", 0);
            return View();
        }

        [HttpPost]
        public ActionResult AdminHtmlBatchCreate(int cate,int dateRange,string from,string to)
        {
            var re = new List<ArticleItem>();
            var cids = cate.ToString();
            if (cate > 0)
            {
                var category = MyService.GetCategoryById(cate);
                if (category.SubCount > 0)
                {
                    cids = MyService.GetCategoryIds(cate);
                }
            }
            if (cids == "0") { cids = ""; }
            var articles = MyService.GetArticlesByDateRange(1, cids, 0, dateRange, from, to);

            foreach (var varticle in articles)
            {
                re.Add(new ArticleItem { Id = varticle.id, Rename = varticle.rename, Date = varticle.createdate.ToString("yyyy-MM-dd") });
            }

            return Json(re, "text/html;charset=UTF-8");
        }

        [HttpPost]
        public ActionResult AdminDoCreate(string date, string lang, int id, string rename)
        {
            try
            {
                WebUtils.CreateHtml(1, id, rename, lang, Convert.ToDateTime(date), GetRootPath());
            }
            catch (Exception)
            {
                return Content("0", "text/html;charset=UTF-8");
            }
            return Content("1", "text/html;charset=UTF-8");
        }

        #endregion

        #region Admin Multi-Language
        public ActionResult AdminJsMultiLanguage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminGenerateJsMultiLanguage()
        {
            foreach (var lang in WebUtils.GetLangList())
            {
                var langinfo = "{";
                foreach (DictionaryEntry entry in Resource.Js.Lang.ResourceManager.GetResourceSet(new CultureInfo(lang.Value), true, true))
                {
                    var resourceKey = (string)entry.Key;
                    var resource = (string)entry.Value;
                    langinfo += "\"" + resourceKey + "\":\"" + resource + "\",";
                }

                langinfo = "var lang=" + langinfo.Trim(',') + "};";

                var filePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/js/lang_" + lang.Value+".js");

                using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    writer.Write(langinfo);
                }
            }
            return Content("success", "text/html;charset=UTF-8");
        }
        #endregion

        #region Private Method
        private void RefreshConfigVersionNo()
        {
            var config = Configinfo;
            config.VersionNo = DateTime.Now.ToString("yyyyMMddhhmmss");
            GeneralConfigs.Serialiaze(config, Server.MapPath(WebUtils.GetWebConfigPath()));
            DataCache.RemoveCache("CategoriesInfoCache");
        }

        private void RefreshConfigMaxCateId(int id)
        {
            var config = Configinfo;
            config.MaxCategoryId = id;
            GeneralConfigs.Serialiaze(config, Server.MapPath(WebUtils.GetWebConfigPath()));
        }

        private JsonResult UploadJsonRe(int error, string message, string url)
        {
            var hash = new Hashtable();
            hash["error"] = error;
            hash["message"] = message;
            hash["url"] = url;
            return Json(hash, "text/html;charset=UTF-8");
        }

        private static int GetSubCount(IEnumerable<CategoryModel> lst, int parent)
        {
            return lst.Count(c => c.ParentId == parent);
        }

        private static string GetCategoryPath(List<CategoryModel> lst, CategoryModel category)
        {
            var c = category;
            var path = GetCurrentCategoryUrl(c);
            while (c.ParentId != 0)
            {
                foreach (var cc in lst.Where(cc => cc.CateId == c.ParentId))
                {
                    path = GetCurrentCategoryUrl(cc) + path;
                    c = cc;
                }
            }
            return path.Trim(',');
        }

        private static string GetCurrentCategoryUrl(CategoryModel category)
        {
            return "," + category.CateId;
        }

        private int AddNewCategory(CategoryModel category)
        {
            var newlst = new List<CategoryModel>();
            var catelist = MyService.GetFCategoryList();
            var cateid = Configinfo.MaxCategoryId + 1;
            category.CateId = cateid;
            category.OrderId = catelist.Count() + 1;
            category.Path = cateid.ToString();
            if (category.IsIndex && category.HomePageKey == string.Empty && category.Type < 3)
            {
                category.HomePageKey = "cate" + cateid;
            }
            if (category.ParentId == 0)
            {
                catelist.Add(category);
                newlst = catelist;
            }
            else
            {
                catelist.Add(category);
                catelist = RefreshCateList(catelist);
                GetNewCategoryList(catelist, ref newlst, 0);
            }
            SaveCateInfo(newlst);
            RefreshConfigMaxCateId(cateid);
            return cateid;
        }

        private static void GetNewCategoryList(IEnumerable<CategoryModel> orderlst, ref List<CategoryModel> newlst, int parentId)
        {
            new List<CategoryModel>();
            var categoryModels = orderlst as CategoryModel[] ?? orderlst.ToArray();
            foreach (var c in categoryModels)
            {
                if (c.ParentId != parentId) continue;
                var category = c;
                category.OrderId = newlst.Count() + 1;
                newlst.Add(category);

                if (c.SubCount > 0)
                {
                    GetNewCategoryList(categoryModels, ref newlst, c.CateId);
                }
            }
        }

        private static void ResetThumbnails(string content)
        {
            try
            {
                var jsonnav = Utils.RemoveHtml(content).Replace("\n", "").Replace("&nbsp;", "");
                if (string.IsNullOrEmpty(jsonnav)) return;
                var photos = Utils.ParseFromJson<List<AlbumPhotoModel>>(jsonnav);
                foreach (var photo in photos)
                {
                    var imgsrc = photo.Src;
                    var attachDir = imgsrc.Remove(imgsrc.LastIndexOf("/", StringComparison.Ordinal)) + "/";
                    var filename = imgsrc.Remove(0, imgsrc.LastIndexOf("/", StringComparison.Ordinal) + 1);
                    WebUtils.CreateThumbnail(imgsrc, attachDir, filename);
                }
            }
            catch { }
        }
        #endregion
    }
}
