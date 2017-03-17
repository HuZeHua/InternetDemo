using XCode.Core;
using XCode.Core.Infrastructure;

namespace XCode.Web.Core.ViewEngines.Razor
{
    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        private IWorkContext _workContext;

        public IWorkContext WorkContext
        {
            get
            {
                return _workContext;
            }
        }

        public override void InitHelpers()
        {
            base.InitHelpers();
           _workContext = EngineContext.Current.Resolve<IWorkContext>();
        }
    }
}
