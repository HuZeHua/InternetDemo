using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarManager.WebCore.MVC;
using CarManager.Web.Models.Student;
using CarManager.Web.Mvc;

namespace CarManager.Web.Controllers
{
    public class StudentController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Update()
        {
            return View();
        }

        public ActionResult Delete()
        {
            return View();
        }

        [AllowCall("Update","Create")]
        public ActionResult GetJson()
        {
            return Json(new object());
        }
    }
}