using System.Web.Mvc;

namespace XCode.Web.Controllers
{
    public class BasePublicController : Controller
    {
        // GET: BasePublic
        public ActionResult Index()
        {
            return View();
        }
    }
}