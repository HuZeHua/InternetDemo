using System.Linq;
using System.Web.Mvc;
using System.Globalization;
using Common;
using Yqblog.General;

namespace Yqblog.Controllers
{
    public class LangController : BaseController
    {
        public ActionResult ChangeLanguage(string language)
        {
            DataCache.RemoveCache("CategoriesInfoCache");
            if (Session["CustomTheme"]==null || string.IsNullOrEmpty((string)Session["CustomTheme"]))
            {
                WebUtils.ChangeThemeViaLang(language);
            }
            var lang = language == "zh-cn" ? "" : language;
            Session["CurrentLanguage"] = new CultureInfo(lang);

            if (Request.UrlReferrer == null)
            {    return Redirect(Url.Content("~/"));}

            var referrerUrl = Request.UrlReferrer.AbsolutePath.TrimEnd('/');
            var absoluteUrl = Url.Content("~/").TrimEnd('/');
            var redirectUrl = Request.UrlReferrer.AbsoluteUri;

            if (!redirectUrl.ToLower().Contains("admin"))
            {
                if (!string.IsNullOrEmpty(referrerUrl) && !string.IsNullOrEmpty(absoluteUrl))
                {
                    referrerUrl = referrerUrl.Replace(absoluteUrl, "/");
                }
                else
                {
                    referrerUrl = "/";
                }
                var langStr = language.ToLower() != Configinfo.DefaultLang ? language : "";
                if (Configinfo.IfIndependentContentViaLang == 1)
                {
                    redirectUrl = langStr;
                }
                else
                {
                    referrerUrl = WebUtils.Langs.Aggregate(referrerUrl, (current, c) => current.Replace(c + "", ""));
                    if (!string.IsNullOrWhiteSpace(langStr) && !string.IsNullOrWhiteSpace(referrerUrl))
                    {
                        referrerUrl = ("/" + referrerUrl + "/").Replace("//", "/");
                        if (referrerUrl.Split('/')[1] == Configinfo.WebStaticFolderPart1)
                        {
                            referrerUrl = referrerUrl.Replace("/" + Configinfo.WebStaticFolderPart1, "");
                            redirectUrl = "/" + Configinfo.WebStaticFolderPart1 + "/" + langStr + "/" + referrerUrl;
                        }
                        else
                        { redirectUrl = langStr + "/" + referrerUrl; }
                    }
                    else
                    {
                        redirectUrl = string.Empty;
                    }

                }
                redirectUrl = absoluteUrl + "/" + redirectUrl.TrimStart('/');
                redirectUrl = redirectUrl.Replace("//", "/");
            }
            else
            {
                redirectUrl = Request.UrlReferrer.ToString();
            }

            return Redirect(redirectUrl);
        }
    }
}
