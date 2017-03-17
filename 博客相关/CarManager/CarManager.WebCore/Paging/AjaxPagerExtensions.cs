using CarManager.Core.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace CarManager.WebCore.Paging
{
    public static class AjaxPagerExtensions
    {
        public static HtmlString AjaxPager(this HtmlHelper htmlHelper, IPagedList list, Func<int, string> generatePageUrl, AjaxOptions ajaxOptions, PagerOptions pageOptions = null)
        {
            pageOptions = pageOptions ?? new PagerOptions();
            return PagerExtensions.Pager(htmlHelper,list,generatePageUrl, PagerOptions.EnableUnobtrusiveAjaxReplacing(ajaxOptions));
        }
    }
}
