using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarManager.Web.Mvc
{
    public static class ThemeHelper
    {
        public static void ChanageTheme(string themeName)
        {
            var engine = ViewEngines.Engines.Where(e => e is RazorViewEngine).Single() as RazorViewEngine;

            engine.ViewLocationFormats = engine.PartialViewLocationFormats = engine.MasterLocationFormats = new string[] {
                "~/Views/Themes/"+themeName+"/{1}/{0}.cshtml",
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };
        }
    }
}