using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarManager.WebCore.MVC;

namespace CarManager.Web.Controllers
{
    public class XssController : BaseController
    {
        private static List<string> db = new List<string>();

        // GET: Xss
        public ActionResult Index()
        {
            return View(db);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Save(string msg)
        {
            db.Add(msg);

            return RedirectToAction(nameof(Index));
        }
    }
}