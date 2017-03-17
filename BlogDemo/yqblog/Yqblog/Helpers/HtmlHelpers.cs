using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using Common;
using System.Globalization;
using Yqblog.Data;
using Yqblog.General;
using Yqblog.Models;
using res = Resource.Models.Web.Web;

namespace Yqblog.Helpers
{
    public static class HtmlHelpers 
    {
        public static GeneralConfigInfo Configinfo = WebUtils.Configinfo;

        #region Get Data

        public static MvcHtmlString GetHomeArea(this HtmlHelper helper, string name)
        {
            if (helper.ViewData["Home_" + name] == null)
            {
                return MvcHtmlString.Empty;
            }
            var re = helper.ViewData["Home_"+name].ToString();
            return MvcHtmlString.Create(re);
        }

        public static MvcHtmlString GetGlobalArea(this HtmlHelper helper, string name)
        {
            if (helper.ViewData["Global_" + name] == null)
            {
                return MvcHtmlString.Empty;
            }
            var re = helper.ViewData["Global_" + name].ToString();
            return MvcHtmlString.Create(re);
        }

        public static List<blog_varticle> GetArticles(this HtmlHelper helper, string name)
        {
            if (helper.ViewData["Articles_" + name] == null)
            {
                return null;
            }
            return helper.ViewData["Articles_" + name] as List<blog_varticle>;
        }

        public static blog_varticle GetFirstArticle(this HtmlHelper helper, string name)
        {
            return helper.ViewData["Articles_" + name] == null ? null : (helper.ViewData["Articles_" + name] as List<blog_varticle>).First();
        }

        public static List<blog_varticle> GetSinglePage(this HtmlHelper helper, string name)
        {
            if (helper.ViewData["SinglePage_" + name] == null)
            {
                return null;
            }
            return helper.ViewData["SinglePage_" + name] as List<blog_varticle>;
        }

        public static blog_varticle GetFirstSinglePage(this HtmlHelper helper, string name)
        {
            return helper.ViewData["SinglePage_" + name] == null ? null : (helper.ViewData["SinglePage_" + name] as List<blog_varticle>).First();
        }

        public static List<AlbumModel> GetAlbums(this HtmlHelper helper, string name)
        {
            if (helper.ViewData["Albums_" + name] == null)
            {
                return null;
            }
            return helper.ViewData["Albums_" + name] as List<AlbumModel>;
        }

        public static List<blog_varticle> GetNotes(this HtmlHelper helper, string name)
        {
            if (helper.ViewData["Notes_" + name] == null)
            {
                return null;
            }
            return helper.ViewData["Notes_" + name] as List<blog_varticle>;
        }
        #endregion

        public static string Thumbnail(this HtmlHelper helper,string imgSrc,string size)
        {
            var imgsrc = imgSrc;
            var imgpath = imgsrc.Remove(imgsrc.LastIndexOf("/", StringComparison.Ordinal));
            var imgname = imgsrc.Remove(0, imgsrc.LastIndexOf("/", StringComparison.Ordinal) + 1);
            imgsrc = imgpath + "/" + size + "/" + imgname;
            return imgsrc;
        }

        public static string Truncate(this HtmlHelper helper, string input, int length)
        {
            if (input.Length <= length)
            { return input; }
            return input.Substring(0, length) + " ... ";
        }

        public static string RemoveHtml(this HtmlHelper helper, string input)
        {
            return input != null ? Utils.RemoveHtml(input) : "";
        }

        public static string YqUrl(this HtmlHelper helper)
        {
            return WebUtils.GetCurrentLangPath();
        }

        public static string YqUrl(this HtmlHelper helper, blog_varticle varticle)
        {
            return WebUtils.GetYqUrl(varticle);
        }

        public static string YqUrl(this HtmlHelper helper, blog_article article)
        {
            return WebUtils.GetYqUrl(article);
        }

        public static string YqUrl(this HtmlHelper helper, string url, int typeid = 0)
        {
            return WebUtils.GetYqUrl(url,typeid);
        }

        public static string YqUrl(this HtmlHelper helper, AlbumModel album)
        {
            return WebUtils.GetYqUrl(album.Varticle.url, 4);
        }

        public static string GetCateUrl(this HtmlHelper helper, string rename,int id,int typeid=0)
        {
            return WebUtils.GetCateUrl(rename, id, typeid);
        }

        public static string GetCateUrl(this HtmlHelper helper, CategoryModel category)
        {
            return WebUtils.GetCateUrl(category);
        }

        public static string GetExtensionTag(this HtmlHelper helper, string field)
        {
            if (string.IsNullOrEmpty(field))
            {
                return string.Empty;
            }
            var index = -1;
            var no = Utils.StrToInt(field.Replace("e_", ""));
            if (no > 0 && no < 6)
            { index = 0; }
            else if (no > 5 && no < 9)
            { index = 1; }
            else if (no > 8 && no < 11)
            { index = 2; }
            return index > -1 ? WebUtils.ExtensionKeys()[index] : "";
        }

        public static MvcHtmlString GetPathInfo(this HtmlHelper helper, string path)
        {
            var re = string.Empty;
            try
            {
                var str = string.Empty;
                var lst = helper.ViewData["CategoriesInfo"] as List<CategoryModel>;
                str = path.Split(',').Select(s => lst != null ? lst.Find(p => p.CateId.ToString() == s) : null).Aggregate(str, (current, category) => current + ("\\ <a href=\"" + WebUtils.GetCateUrl(category) + "\">" + category.CateName + "</a> "));
                re= str.Trim('\\').Trim();
            }
            catch
            {}
            return MvcHtmlString.Create(re);
        }

        public static bool IsLastCategory(this HtmlHelper helper, int id)
        {
            if (id == 0)
            {
                return false;
            }
            var lst = helper.ViewData["CategoriesInfo"] as List<CategoryModel>;
            if (lst != null)
            {
                var category = lst.First(x => x.CateId == id);
                return category.SubCount == 0;
            }
            return false;
        }

        public static MvcHtmlString GetUserInfo(this HtmlHelper helper, string users)
        {
            var arruser = users.Split(',');
            var re = arruser.Aggregate("", (current, user) => current + ("<a href=\"" + WebUtils.GetYqUrl("/u/" + user.Trim()) + "\">" + user.Trim() + "</a> "));
            return MvcHtmlString.Create(re);
        }

        public static string DateFromNow(this HtmlHelper helper, DateTime dt, int days=10)
        {
            return WebUtils.DateFromNow(dt,days);
        }

        public static string GetDate(this HtmlHelper helper, DateTime? dt,string formart="")
        {
            if (dt == null)
            {
                return string.Empty;
            }
            var date = Convert.ToDateTime(dt);
            if (formart != "")
            {
                return date.ToString(formart);
            }
            if (res.Lang == "zh-cn" || res.Lang == "zh-tw")
            {
                return date.ToString("yyyy-MM-dd");
            }
            var myDtfi = new CultureInfo(res.Lang, false).DateTimeFormat;
            var strmon = myDtfi.GetAbbreviatedMonthName(date.Month);
            return strmon + ". " + date.Day + ", " + date.Year;
        }

        public static string GetTime(this HtmlHelper helper, DateTime? dt)
        {
            if (dt == null)
            {
                return string.Empty;
            }
            var date = Convert.ToDateTime(dt);
            if (res.Lang == "zh-cn" || res.Lang == "zh-tw")
            {
                return date.ToString("yyyy-MM-dd HH:mm");
            }
            var myDtfi = new CultureInfo(res.Lang, false).DateTimeFormat;
            var strmon = myDtfi.GetAbbreviatedMonthName(date.Month);
            return strmon + ". " + date.Day + ", " + date.Year + " " + date.ToString("HH:mm");
        }

        public static MvcHtmlString GetNav(this HtmlHelper helper, IEnumerable<CategoryModel> categories, int parentId = 0,CategoryModel category=null)
        {
            var re = GetSubNavStr(categories, parentId, parentId, parentId, 1, category);
            return MvcHtmlString.Create(re);
        }

        private static string GetSubNavStr(IEnumerable<CategoryModel> categories, int parentId, int currentParentId, int firstParentId, int layer, CategoryModel category)
        {
            var categoryRootId = !string.IsNullOrEmpty(category.Path) && category.Path.Split(',').Length > 0
                                     ? category.Path.Split(',')[0]
                                     : string.Empty;
            var re = string.Empty;
            var catelist = categories.Where(x => x.ParentId == parentId).ToList();
            if (firstParentId == currentParentId && layer!=1)
            {
                layer = 2;
            }
            if (catelist.Any())
            {
                var navclass = "subNav";
                switch (layer)
                {
                    case 1:
                        navclass = "nav mainNav navbar-nav";
                        break;
                    case 2:
                        navclass = "firstSubNav subNav";
                        break;
                }
                re = "<ul class=\"" + navclass + "\">\r\n";
                layer++;
                
                foreach (var item in catelist)
                {
                    var cssInfo = string.Empty;
                    cssInfo += (categoryRootId == item.CateId.ToString()) ? " active" : string.Empty;
                    cssInfo += item.SubCount > 0 ? " hassub" : string.Empty;
                    cssInfo = string.IsNullOrEmpty(cssInfo.Trim()) ? string.Empty : " class=\"" + cssInfo.Trim() + "\"";
                    var menuarrow = item.SubCount > 0 ? "<b class=\"menuarrow\"></b>" : string.Empty;
                    re += "<li" + cssInfo + "><a href=\"" + WebUtils.GetCateUrl(item) + "\">" + item.CateName + menuarrow + "</a>\r\n";
                    re += GetSubNavStr(categories, item.CateId, item.ParentId, firstParentId, layer, category);
                    re += "</li>\r\n";
                }
                re += "</ul>\r\n";
            }
            return re;
        }

        public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            return LabelFor(html, expression, new RouteValueDictionary(htmlAttributes));
        }
        
        public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (String.IsNullOrEmpty(labelText))
            {
                return MvcHtmlString.Empty;
            }

            var tag = new TagBuilder("label");
            tag.MergeAttributes(htmlAttributes);
            tag.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));

            var span = new TagBuilder("span");
            span.SetInnerText(labelText);

            tag.InnerHtml = span.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }

        public static string GetCurrentLang(this HtmlHelper helper)
        {
            var weblang = Resource.Models.Web.Web.Lang;
            var langstr = "English";
            switch (weblang)
            {
                case "zh-cn":
                    langstr = "简体";
                    break;
                case "zh-tw":
                    langstr = "繁体";
                    break;
            }
            return langstr;
        }

        public static string JudgeSingularOrPlural(this HtmlHelper helper, int count, string singularKey, string pluralKey)
        {
            return count > 1 ? pluralKey : singularKey;
        }
    } 

}