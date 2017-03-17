using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarManager.WebCore.MVC;

namespace CarManager.Web.Controllers
{
    public class ExceptionController : BaseController
    {
        public ActionResult Index(int a,int b)
        {
            int c = a / b;
            return View();
        }
    }
}