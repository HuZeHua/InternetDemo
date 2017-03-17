using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarManager.Web.Startup))]
namespace CarManager.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
