using System.Globalization;
using System.Web.Mvc;

namespace Yqblog.General
{
    public class BaseViewPage<TModel> : WebViewPage<TModel> where TModel : class
    {
        public virtual MvcHtmlString LangHtml(string neutralValue, params object[] args)
        {
            return MvcHtmlString.Create(Lang(neutralValue, args));
        }

        public virtual string Lang(string neutralValue, params object[] args)
        {
            return string.Format(ResourceProvider.R(neutralValue), args);
        }

        public virtual string Lang(string neutralValue)
        {
            return ResourceProvider.R(neutralValue);
        }

        public override void Execute()
        {
        }

        protected override void InitializePage()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(ResourceProvider.Culture);
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(ResourceProvider.Culture);
            base.InitializePage();
        }
    }
}