using System.Web.Mvc;

namespace Yqblog
{
    public class AdminRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "BlogAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "BlogAdmin/{controller}/{action}/{id}",
                new { id = UrlParameter.Optional }
            );
        }
    }

}