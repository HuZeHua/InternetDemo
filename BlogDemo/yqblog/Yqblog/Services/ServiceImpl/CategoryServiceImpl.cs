using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Yqblog.Data;
using Yqblog.General;
using Yqblog.Models;

namespace Yqblog.Services.ServiceImpl
{
    public partial class ServiceImpl
    {
        readonly GeneralConfigInfo _configinfo = WebUtils.Configinfo;

        public CategoryModel GetCategoryById(int cid)
        {
            try
            {
                var lst = GetCategoryList();
                var category = lst.Find(p => p.CateId == cid);
                return category;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int GetMaxCategoryId()
        {
            try
            {
                var lst = GetCategoryList();
                var re = lst.Select(p => p.CateId).Max();
                return re;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public CategoryModel GetCategoryByReName(string rename)
        {
            try
            {
                var lst = GetCategoryList();
                var category = lst.Find(p => p.ReName == rename);
                return category;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<CategoryModel> GetFCategoryList(string space = "")
        {
            return GetFCategoryList("", "", space);
        }

        public List<CategoryModel> GetFCategoryList(string tids, string cids, string space = "")
        {
            var arrcid = cids.Split(',');
            var arrtid = tids.Split(',');
            var list = GetCategories();
            if (cids.Trim()!="")
            {   list = list.Where(m => arrcid.Contains(m.CateId.ToString()));} 
            else if (tids.Trim() != "")
            {   list = list.Where(m => arrtid.Contains(m.Type.ToString()));}
            list = list.OrderBy(m => m.OrderId);
            var newlst = new List<CategoryModel>();
            if (space == "")
            {
                newlst = list.ToList();
            }
            else
            {
                foreach (var c in list)
                {
                    c.ViewName = Utils.GetSpace(c.Path.Split(',').Count(), space) + c.CateName;
                    newlst.Add(c);
                }
            }
            return newlst;
        }

        public string GetCategoryPathUrl(string path)
        {
            try
            {
                var str = string.Empty;
                var lst = GetCategoryList();
                str = path.Split(',').Select(s => lst.Find(p => p.CateId.ToString() == s)).Aggregate(str, (current, category) => current + ("<li><a href=\"" + WebUtils.GetCateUrl(category) + "\">" + category.CateName + "</a> <span class=\"divider\">/</span></li>"));
                return str.Trim('\\').Trim();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public string GetCategoryPathUrl2(string path)
        {
            string str;
            var arrpath = path.Split(',');
            if (arrpath.Count() < 2)
            {   return string.Empty;}
            var lst = GetCategoryList();
            if (arrpath.Count() == 2)
            {
                var category = lst.Find(p => p.CateId.ToString() == arrpath[1]);
                str=category.CateName;
            }
            else
            {
                var newPath = string.Empty;
                for (var i = 1; i < arrpath.Count(); i++)
                { newPath += arrpath[i] + ','; }
                str = GetCategoryPathUrl(newPath.Trim(','));
            }
            return str;
        }

        public IQueryable<CategoryModel> GetCategories()
        {
            return GetCategoryList().AsQueryable();
        }

        public List<CategoryModel> GetIndexCategoryList()
        {
            var lst = GetCategoryList();
            return lst.Where(category => category.IsIndex && category.HomePageKey!="").ToList();
        }

        public string GetCategoryIds(int cid)
        {
            try
            {
                return String.Concat(GetCategoryList().Where(c =>
                                      ("," + c.Path + ",").Contains("," + cid.ToString() + ",")).Select(c => c.CateId.ToString()+",")).Trim(',');
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public IQueryable<CategoryModel> GetSubCategoryList(int cid)
        {
            try
            {
                return GetCategories().Where(c =>
                                      ("," + c.Path + ",").Contains("," + cid.ToString() + ",")).Where(c => c.CateId != cid).OrderBy(c => c.OrderId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IQueryable<CategoryModel> GetSonCategoryList(int cid)
        {
            try
            {
                return GetCategories().Where(c => c.ParentId == cid).OrderBy(c => c.OrderId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GetCategoryStr()
        {
            var re=string.Empty;
            try
            {
                re = Utils.GetFileSource("~/Content/js/Category.js").Replace("var category =", "").Trim();
            }
            catch {}
            return re;
        }

        public string GetCategoryLangStr()
        {
            var re = "";
            try
            {
                re = Utils.GetFileSource("~/Content/js/" + WebUtils.GetCategoryLangName()).Replace("var categorylang =", "").Trim();
            }
            catch{ }
            return re;
        }

        private List<CategoryModel> GetCategoryList()
        {
            const string cacheKey = "CategoriesInfoCache";
            var cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                cache = GetCategoriesInfo();
                DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes(1800.0), TimeSpan.Zero);
            }
            return cache as List<CategoryModel>;
        }

        private List<CategoryModel> GetCategoriesInfo()
        {
            var lst = new List<CategoryModel>();
            var langlst = new List<CategoryLangModel>();
            try
            {
                if (_configinfo.IfIndependentContentViaLang == 1)
                {
                    var re = Utils.GetFileSource("~/Content/js/Category." + Resource.Models.Web.Web.Lang + ".js").Replace("var category =", "").Trim().Replace("\n", "");
                    if (re != "")
                    {
                        lst = Utils.ParseFromJson<List<CategoryModel>>(re);
                    }
                }
                else
                {
                    var jsonnav = GetCategoryStr().Replace("\n", "");
                    if (jsonnav != "")
                    {
                        lst = Utils.ParseFromJson<List<CategoryModel>>(jsonnav);
                    }
                    var jsonnavlang = GetCategoryLangStr().Replace("\n", "");
                    if (jsonnavlang != "")
                    {
                        langlst = Utils.ParseFromJson<List<CategoryLangModel>>(jsonnavlang);
                    }

                    lst = (from a in lst
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
            }
            catch { }
            var newlst = new List<CategoryModel>();
            foreach (var c in lst)
            {
                c.ViewName = c.CateName;
                newlst.Add(c);
            }
            return newlst;
        }
    }
}