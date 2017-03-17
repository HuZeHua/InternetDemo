using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Text.RegularExpressions;
using System.IO;
using Common;
using System.Web.Helpers;
using System.Net.Mail;
using System.Text;
using System.Net;
using System.Web.UI;
using Yqblog.Data;
using Yqblog.Models;
using resModelWeb = Resource.Models.Web.Web;

namespace Yqblog.General
{
    public class WebUtils
    {
        public static GeneralConfigInfo Configinfo = GeneralConfigs.GetConfig();

        public static string GetCateUrl(CategoryModel category)
        {
            return GetCurrentLangPath() + (!string.IsNullOrWhiteSpace(category.ReName) ? "/" + category.ReName + "/" : "/cate/" + category.CateId);
        }

        public static string GetCateUrl(string rename,int id,int typeid=0)
        {
            return GetCurrentLangPath() + (!string.IsNullOrWhiteSpace(rename) ? "/" + rename + "/" : "/cate/" + id);
        }

        public static string GetArticleUrl(int id, string reName)
        {
            return !string.IsNullOrWhiteSpace(reName) ? "/article/" + reName : "/archive/" + id;
        }

        public static string GetArticleUrl(blog_varticle varticle)
        {
            return !string.IsNullOrWhiteSpace(varticle.rename) ? "/article/" + varticle.rename : "/archive/" + varticle.id;
        }

        public static string GetViewName(string customView,string defaultView)
        {
            return string.IsNullOrWhiteSpace(customView) ? defaultView : customView;
        }

        public static string GetWebConfigPath()
        {
            return ConfigurationManager.AppSettings["BlogConfig"];
        }

        public static string GetCategoryLangName()
        {
            return "Category.Lang."+Resource.Models.Web.Web.Lang+".js";
        }

        public static void ChangeTheme(string key)
        {
            var theme = MyString(key);
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new BlogViewEngine(theme));
        }

        public static string ChangeThemeViaLang(string key)
        {
            var theme = GetLangTemplate(key);
            ChangeTheme(theme);
            return theme;
        }

        public static string GetCurrentLangPath()
        {
            return GetWebRootPath() + (resModelWeb.Lang != Configinfo.DefaultLang ? "/" + resModelWeb.Lang : "");
        }

        public static string GetWebRootPath()
        {
            var applicationPath = HttpContext.Current.Request.ApplicationPath != "/" ? HttpContext.Current.Request.ApplicationPath : string.Empty;
            var port = HttpContext.Current.Request.Url.Port;
            return string.Format("{0}://{1}{2}{3}",
                                 HttpContext.Current.Request.Url.Scheme,
                                 HttpContext.Current.Request.Url.Host,
                                 (port == 80 || port == 0) ? "" : ":" + port,
                                 applicationPath
                                 );
        }

        public static string GetCurrentTheme()
        {
            return GetLangTemplate(Configinfo.DefaultLang);
        }

        public static List<PageLang> GetLangList()
        {
            var items = new List<PageLang>
                            {
                                new PageLang
                                    {
                                        Key = "中(简体)",
                                        Value = "zh-cn"
                                    },
                                new PageLang
                                    {
                                        Key = "中(繁体)",
                                        Value = "zh-tw"
                                    },
                                new PageLang
                                    {
                                        Key = "English",
                                        Value = "en-us"
                                    }
                            };
            return items;
        }

        public static readonly string[] Langs = new[] { "/en-us", "/zh-tw", "/zh-cn" };

        public static readonly string[] UrlPrefix = new[] { "", "archive", "archive", "", "album", "forum/thread" };

        public static List<SelectItem> GetGenderList()
        {
            var items = new List<SelectItem>
                            {
                                new SelectItem
                                    {
                                        Key = resModelWeb.Secrecy,
                                        Value = "0"
                                    },
                                new SelectItem
                                    {
                                        Key = resModelWeb.Male,
                                        Value = "1"
                                    },
                                new SelectItem
                                    {
                                        Key = resModelWeb.Female,
                                        Value = "2"
                                    }
                            };
            return items;
        }

        public static string Ubb2Html(string argString)
        {
            var tString = argString;
            if (tString != "")
            {
                var tState = true;
                tString = tString.Replace("&", "&amp;");
                tString = tString.Replace(">", "&gt;");
                tString = tString.Replace("<", "&lt;");
                tString = tString.Replace("\"", "&quot;");
                tString = tString.Replace("&amp;#91;", "&#91;");
                tString = tString.Replace("&amp;#93;", "&#93;");
                tString = tString.Replace("\r\n", "<br/>");
                tString = Regex.Replace(tString, @"\[br\]", "<br/>", RegexOptions.IgnoreCase);
                string[,] tRegexAry = {
                  {@"\[p\]([^\[]*?)\[\/p\]", "$1<br/>"},
                  {@"\[b\]([^\[]*?)\[\/b\]", "<b>$1</b>"},
                  {@"\[i\]([^\[]*?)\[\/i\]", "<i>$1</i>"},
                  {@"\[u\]([^\[]*?)\[\/u\]", "<u>$1</u>"},
                  {@"\[ol\]([^\[]*?)\[\/ol\]", "<ol>$1</ol>"},
                  {@"\[ul\]([^\[]*?)\[\/ul\]", "<ul>$1</ul>"},
                  {@"\[li\]([^\[]*?)\[\/li\]", "<li>$1</li>"},
                  {@"\[code\]([^\[]*?)\[\/code\]", "<div class=\"ubb_code\">$1</div>"},
                  {@"\[quote\]([^\[]*?)\[\/quote\]", "<fieldset class=\"comment_quote\"><legend> "+resModelWeb.Quote+" </legend>$1</fieldset>"},
                  {@"\[color=([^\]]*)\]([^\[]*?)\[\/color\]", "<font style=\"color: $1\">$2</font>"},
                  {@"\[hilitecolor=([^\]]*)\]([^\[]*?)\[\/hilitecolor\]", "<font style=\"background-color: $1\">$2</font>"},
                  {@"\[align=([^\]]*)\]([^\[]*?)\[\/align\]", "<div style=\"text-align: $1\">$2</div>"},
                  {@"\[url=js:([^\]]*)\]([^\[]*?)\[\/url\]", "<a href=\"###\" onclick=\"$1\">$2</a>"},
                  {@"\[url=([^\]]*)\]([^\[]*?)\[\/url\]", "<a href=\"$1\">$2</a>"},
                  {@"\[code=([^\]]*)\](.*?)\[\/code\]", "<pre class=\"brush:$1\">$2</pre>"},
                  {@"\[img\]([^\[]*?)\[\/img\]", "<img src=\"$1\" />"}
                };
                while (tState)
                {
                    tState = false;
                    for (var ti = 0; ti < tRegexAry.GetLength(0); ti++)
                    {
                        var tRegex = new Regex(tRegexAry[ti, 0], RegexOptions.IgnoreCase);
                        if (!tRegex.Match(tString).Success) continue;
                        tState = true;
                        tString = Regex.Replace(tString, tRegexAry[ti, 0], tRegexAry[ti, 1], RegexOptions.IgnoreCase);
                    }
                }
            }
            return GetCode(tString);
        }

        public static string GetCode(string str)
        {
            var re = new Regex(@"<pre.*?</pre>", RegexOptions.Multiline);
            var a = re.Matches(str);
            foreach (Match m in a)
            {
                if (!m.Success) continue;
                var tmp = m.Value.Replace("<br/>", "\r\n");
                str = str.Replace(m.Value, tmp);
            }
            return str;
        }

        public static string HtmlToUbb(string html)
        {
            if (string.IsNullOrEmpty(html))
            {
                return string.Empty;
            }
            html = Regex.Replace(html, "<br[^>]*>", "\n");
            html = Regex.Replace(html, @"<p[^>\/]*\/>", "\n");
            html = Regex.Replace(html, "<hr[^>]*>", "[hr]");
            html = Regex.Replace(html, "<(\\/)?blockquote([^>]*)>", "[$1blockquote]");
            html = Regex.Replace(html, "<img[^>]*smile=\"(\\d+)\"[^>]*>", "‘[s:$1]");
            html = Regex.Replace(html, "<img[^>]*src=[\'\"\\s]*([^\\s\'\"]+)[^>]*>", "[img]$1[/img]");
            html = Regex.Replace(html, "<a[^>]*href=[\'\"\\s]*([^\\s\'\"]*)[^>]*>(.+?)<\\/a>", "[url=$1]$2[/url]");
            html = Regex.Replace(html, "<b>(.+?)</b>", @"\[b\]$1\[/b\]");
            html = Regex.Replace(html, "<[^>]*?>", "");
            html = Regex.Replace(html, "&amp;", "&");
            html = Regex.Replace(html, "&nbsp;", " ");
            html = Regex.Replace(html, "&lt;", "<");
            html = Regex.Replace(html, "&gt;", ">");
            return html;
        }

        public static int GetDecorateCount(string theme)
        {
            const string cacheKey = "DecorateCount";
            var cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                cache = CurrentDecorateCount(theme);
                DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes(180.0), TimeSpan.Zero);
            }
            return Utils.ObjectToInt(cache);
        }

        private static int CurrentDecorateCount(string theme)
        {
            var decoratepath = theme == "" ? "~/Content/image/decorate" : "~/Themes/" + theme + "/Content/image/decorate";
            return Directory.GetFiles(HttpContext.Current.Server.MapPath(decoratepath), "*.jpg").Length - 1;
        }

        public static List<string> GetUsersFromTxt(string str)
        {
            var regstr = new Regex(@"@(.*?)\s", RegexOptions.IgnoreCase);
            var mc = regstr.Matches(str);
            var lst = (from Match match in mc select match.Groups[1].Value).ToList();
            lst = lst.GroupBy(x => x).Select(x => x.Key).ToList();

            return lst;
        }

        public static void SendWebMail(string emailTo, string emailTitle, string emailContent,string[] filePath = null, string[] additionalHeaders = null)        
        {
            var reg = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            WebMail.SmtpServer = Configinfo.SmtpServer;         
            WebMail.SmtpPort = Configinfo.SmtpPort;                   
            WebMail.UserName = Configinfo.SmtpUser;        
            WebMail.From = Configinfo.AdminEmail;         
            WebMail.Password = Configinfo.SmtpPass;
            WebMail.EnableSsl = true; 
            WebMail.SmtpUseDefaultCredentials = true;                 
            try
            {
                if (reg.IsMatch(emailTo))
                {
                    WebMail.Send(emailTo, emailTitle, emailContent, isBodyHtml: true, filesToAttach: filePath, additionalHeaders: additionalHeaders);
                }
            }
            catch{}        
        }

        public static void SendWebMail(string emailTo, string emailTitle, string emailContent)
        {
            SendSysMail(emailTo, emailTitle, emailContent);
        }

        public static void SendSysMail(string to, string subject, string body)
        {
            var from = Configinfo.AdminEmail;
            var host = Configinfo.SmtpServer;
            var userName = Configinfo.SmtpUser;
            var password = Configinfo.SmtpPass;
            var port = Configinfo.SmtpPort;
            if (!string.IsNullOrEmpty(from) && !string.IsNullOrEmpty(host) && !string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            { SendMail(from, to, subject, body, host, port, userName, password);}
        }

        public static void SendMail(string from, string to, string subject, string body, string host, int port, string userName, string password)
        {
            var message = new MailMessage(from, to, subject, body);
            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;
            var client = new SmtpClient(host);
            client.Credentials = new NetworkCredential(userName, password);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Port = port;
            client.Send(message);
        }

        public static void CreateHtml(int typeid, long id, string rename, DateTime d,string rootPath)
        {
            if (Configinfo.IfIndependentContentViaLang == 1)
            {
                CreateHtml(typeid, id, rename, resModelWeb.Lang, d, rootPath);
            }
            else
            {
                foreach (var lang in Configinfo.WebLangList)
                {
                    CreateHtml(typeid, id, rename, lang, d, rootPath);
                }
            }
        }

        public static void CreateHtml(int typeid, long id, string rename, string lang, DateTime d, string rootPath)
        {
            if (typeid != 1)
            { return; }
            var folder = "\\" + GetStaticFolder(d, lang).Trim('/').Replace("/","\\") + "\\";
            var hostPath = System.Web.Hosting.HostingEnvironment.MapPath("~");
            var folderPath = (hostPath + folder).Replace("\\\\", "\\");
            Utils.CreateFolder(folderPath);
            var langStr = Configinfo.DefaultLang == lang ? "/" : "/" + lang + "/";
            var sourcePath = rootPath + langStr + UrlPrefix[typeid] + "/" + id;
            var aimPath = folderPath + id + ".html";
            if (rename.Length > 0)
            { aimPath = folderPath + rename + ".html"; }
            Utils.CreateHtml(sourcePath, aimPath);
        }
         
        public static void DeleteHtml(long id, string rename, DateTime d)
        {
            foreach (var lang in Configinfo.WebLangList)
            {
                var page = new Page();
                var folder = "~/" + GetStaticFolder(d, lang).Trim('/') + "/";
                var aimPath = page.Server.MapPath(folder);
                if (rename.Length > 0)
                { aimPath += rename + ".html"; }
                else
                { aimPath += id + ".html"; }

                Utils.DeleteHtml(aimPath);
            }
        }

        public static string GetStaticFolder(DateTime date,string lang)
        {
            var langstr = (lang == Configinfo.DefaultLang ? "" : lang);
            var path = Configinfo.WebStaticFolder;
            var year = date.Year.ToString();
            var month = date.Month < 10 ? "0" + date.Month.ToString() : date.Month.ToString();
            var day = date.Day.ToString();
            return path.Replace("{lang}", langstr).Replace("{year}", year).Replace("{month}", month).Replace("{day}", day).Replace("//", "/");
        }

        public static string GetYqUrl(blog_varticle varticle)
        {
            string url;
            if (varticle.typeid == 1)
            {
                var pageId = varticle.articleid;
                if (Configinfo.IfWebStatic == 1 && GetCurrentTheme() == GetLangTemplate(Configinfo.DefaultLang))
                {
                    url = GetWebRootPath() +"/"+ GetStaticFolder(varticle.createdate, resModelWeb.Lang).Trim('/') + "/";
                    if (varticle.rename.Trim().Length > 0)
                    { url += varticle.rename.Trim() + ".html"; }
                    else
                    { url += pageId + ".html"; }
                }
                else
                {
                    url = GetYqUrl(varticle.url, 1);
                }
            }
            else
            {
                var articleurl = varticle.rename.Trim().Length > 0 ? varticle.rename.Trim() : varticle.articleid.ToString();
                url = GetYqUrl(articleurl, varticle.typeid);
            }
            return url;
        }

        public static string GetYqUrl(blog_article article)
        {
            string url;
            var articleurl = article.rename.Trim().Length > 0 ? article.rename.Trim() : article.articleid.ToString();
            if (article.typeid == 1)
            {
                if (Configinfo.IfWebStatic == 1 && GetCurrentTheme() == GetLangTemplate(Configinfo.DefaultLang))
                {
                    url = GetWebRootPath()+"/" + GetStaticFolder(article.createdate, resModelWeb.Lang).Trim('/') + "/" + articleurl + ".html";
                }
                else
                {
                    url = GetYqUrl(articleurl, 1);
                }
            }
            else
            {
                url = GetYqUrl(articleurl, article.typeid);
            }
            return url;
        }

        public static string GetYqUrl(string url, int typeid = 0)
        {
            return GetCurrentLangPath() + (typeid > 0 ? "/" + UrlPrefix[typeid] + "/" : "") + url;
        }

        public static List<LangTemplate> GetLangTemplateList()
        {
            return GetLangTemplateList(Configinfo.LangTemplateStr);
        }

        public static List<LangTemplate> GetLangTemplateList(string langtemplatestr)
        {
            var langTemplatelst = new List<LangTemplate>();
            try
            {
                if (langtemplatestr != "")
                {
                    langTemplatelst = Utils.ParseFromJson<List<LangTemplate>>(langtemplatestr);
                }
            }
            catch (Exception) { }
            return langTemplatelst;
        }

        public static string GetLangTemplate(string lang, string langtemplatestr="")
        {
            var template = MyString(Configinfo.Theme);
            try
            {
                var langTemplatelst = GetLangTemplateList();
                if (langTemplatelst.Count > 0)
                {
                    foreach (var p in langTemplatelst.Where(p => p.Lang == lang))
                    {
                        return p.Template;
                    }
                }
            }
            catch
            {
                template = Configinfo.Theme;
            }
            return template;
        }

        public static string GetCurrentLangStr()
        {
            return Configinfo.IfIndependentContentViaLang == 1 ? resModelWeb.Lang : Configinfo.DefaultLang;
        }

        public static string MyString(string str, string def = "")
        {
            return string.IsNullOrWhiteSpace(str) ? def : str;
        }

        public static string[] ExtensionKeys()
        {
            return new[] { resModelWeb.String + "(" + resModelWeb.Length + ":50 " + resModelWeb.Character + ")", resModelWeb.String + "(" + resModelWeb.Length + ":500 " + resModelWeb.Character + ")", resModelWeb.Text };
        }

        public static string DateFromNow(DateTime dt, int days=10)
        {
            var span = DateTime.Now - dt;
            if (span.TotalDays > days)
            {
                return dt.ToShortDateString();
            }
            if (span.TotalDays > 1)
            {
                return string.Format("{0} "+resModelWeb.DaysAgo, (int)Math.Floor(span.TotalDays));
            }
            if (span.TotalHours > 1)
            {
                return string.Format("{0} "+resModelWeb.HoursAgo, (int)Math.Floor(span.TotalHours));
            }
            if (span.TotalMinutes > 1)
            {
                return string.Format("{0} "+resModelWeb.MinutesAgo, (int)Math.Floor(span.TotalMinutes));
            }
            if (span.TotalSeconds >= 1)
            {
                return string.Format("{0} "+resModelWeb.SecAgo, (int)Math.Floor(span.TotalSeconds));
            }
            return "1 "+resModelWeb.SecAgo;
        }

        public static string GetSeoInfo(blog_varticle varticle)
        {
            var metaDescription = string.IsNullOrWhiteSpace(varticle.seodescription) ? "" : "<meta content=\"" + varticle.seodescription + "\" name=\"Description\" />\r\n";
            var metaKeywords = string.IsNullOrWhiteSpace(varticle.seokeywords) ? "" : "<meta content=\"" + varticle.seokeywords + "\" name=\"keywords\" />\r\n";
            var metaInfo = string.IsNullOrWhiteSpace(varticle.seometas) ? "" : varticle.seometas + "\r\n";
            return metaDescription + metaKeywords + metaInfo;
        }

        public static string FileterData(string content, int datatype)
        {
            return datatype == 2 ? Utils.FileterScript(content) : Ubb2Html(content);
        }


        public static void CreateThumbnail(string pic, string path, string filename)
        {
            if (string.IsNullOrEmpty(Configinfo.ThumbnailInfo.Trim())) return;
            foreach (var s in Configinfo.ThumbnailInfo.Split(new[] { ',' }))
            {
                var ss = s.Split(new[] { 'x' });
                if (ss.Length != 2) continue;
                var w = Utils.StrToInt(ss[0]);
                var h = Utils.StrToInt(ss[1]);
                if (w <= 0 || h <= 0) continue;
                CreateFolder(HttpContext.Current.Server.MapPath(path + s + "/"));
                Thumbnail.MakeThumbnailImage(HttpContext.Current.Server.MapPath(pic), HttpContext.Current.Server.MapPath(path + s + "/") + filename, w, h);
            }
        }

        public static void CreateFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
        }
    }
}