using System.Collections.Generic;
using System.Web.Mvc;
using XCode.Admin.Models.Common;

namespace XCode.Admin.Models.Catalog
{
    public class ArticleSearchModel : DataTableParameter
    {
        public ArticleSearchModel()
        {
            this.SearchCategories = new List<SelectListItem> { 
                new SelectListItem { Text = "请选择", Value = "0"}
            };
        }

        public string SearchTitle { get; set; }

        public int SearchCategoryId { get; set; }

        public List<SelectListItem> SearchCategories { get; set; }
    }
}