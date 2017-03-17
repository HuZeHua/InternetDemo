using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Xml;

namespace Yqblog.General
 {
     public static class ResourceProvider
     {
         private const string ResourcePath = "~/App_Data/Lang.xml";

         private const string LanguageCahceName = "Lang.xml";

         private const string DefaultCulture = "zh-CN";
 
         public static string R(string culture, string name)
         {
             Dictionary<string, Dictionary<CultureInfo, string>> res = null;
 
             if (HttpRuntime.Cache[LanguageCahceName] == null)
             {
                 var fullPath = HttpContext.Current.Server.MapPath(ResourcePath);
                 if (!System.IO.File.Exists(fullPath))
                 {
                     throw new Exception("file not exist!");
                 }
                 var doc = new XmlDocument();
                 doc.Load(fullPath);
                 var dicts = doc.SelectNodes("dicts/dict");
                 if (dicts != null)
                 {
                     res = new Dictionary<string, Dictionary<CultureInfo, string>>(dicts.Count);
                     foreach (XmlNode item in dicts)
                     {
                         var kvs = item.ChildNodes.Cast<XmlNode>().ToDictionary(el => new CultureInfo(el.Name), el => el.InnerText);
                         if (item.Attributes != null) res.Add(item.Attributes["name"].Value, kvs);
                     }
                 }
                 if (res != null)
                 {
                     HttpRuntime.Cache.Insert(LanguageCahceName,
                                              res, new System.Web.Caching.CacheDependency(fullPath));
                 }
             }
             else
             {
                 res = (Dictionary<string, Dictionary<CultureInfo, string>>)
                     HttpRuntime.Cache[LanguageCahceName];
             }
             try
             {
                 if (res[name] != null)
                 {
                     if (res[name][new CultureInfo(culture)] != null)
                     {
                         return res[name][new CultureInfo(culture)];
                     }
                     else
                     {
                         return res[name][new CultureInfo(DefaultCulture)];
                     }
                 }
                 return res[name][new CultureInfo(culture)];
             }
             catch
             {
                 return string.Empty;
             }
         }
 
         public static string R(string name)
         {
             return R(Culture, name);
         }
 
         private const string CultureCookieName = "Culture";

         public static string Culture
         {
             get
             {
                 var cookie = HttpContext.Current.Request.Cookies[CultureCookieName];
                 var cultrue= cookie == null ? CultureInfo.CurrentCulture.Name : cookie.Value;
                 return string.IsNullOrEmpty(cultrue) ? "zh-cn" : cultrue;
             }
             set
             {
                 if (string.IsNullOrEmpty(value)) return;
                 var cookie = HttpContext.Current.Request.Cookies[CultureCookieName];
                 if (cookie != null)
                 {
                     cookie.Expires = DateTime.Now.AddDays(-1);
                     HttpContext.Current.Response.Cookies.Add(cookie);
                 }
 
                 cookie = new HttpCookie(CultureCookieName, value)
                              {
                                  Expires = DateTime.Now.AddYears(1)
                              };
                 HttpContext.Current.Response.Cookies.Add(cookie);
             }
         }
     }
 }