using System.Web.Mvc;

namespace Yqblog.General
{
    public class BlogViewEngine  : RazorViewEngine
    {
        public BlogViewEngine(string key)
        {
            if (!string.IsNullOrWhiteSpace(key) && key != "default")
            {
                var path = "~/Themes/" + key;
                ViewLocationFormats = new[]
                                          {
                                              path + "/{1}/{0}.cshtml",
                                              path + "/{1}/{0}.vbhtml",
                                              path + "/Shared/{0}.cshtml",
                                              path + "/Shared/{0}.vbhtml"
                                          };
            }
            else
            {
                ViewLocationFormats = new[]
                                          {
                                              "~/Views/{1}/{0}.cshtml",
                                              "~/Views/{1}/{0}.vbhtml",
                                              "~/Views/Shared/{0}.cshtml",
                                              "~/Views/Shared/{0}.vbhtml"
                                          };
            }

            PartialViewLocationFormats = ViewLocationFormats;
        }
    }
}