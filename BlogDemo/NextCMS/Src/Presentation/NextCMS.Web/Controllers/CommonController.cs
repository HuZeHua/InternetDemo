﻿using System.Linq;
using System.Web.Mvc;
using XCode.Services.Catalog;
using XCode.Web.Models.Common;

namespace XCode.Web.Controllers
{
    public class CommonController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CommonController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        [ChildActionOnly]
        public ActionResult TopMenu()
        {
            var model = _categoryService.GetAllCategory()
                .Where(t => t.IncludeInTopMenu && t.Published && t.ShowOnHomePage && !t.Deleted)
                .OrderBy(t => t.DisplayOrder)
                .ThenBy(t => t.Id)
                .Select(t => new TopMenuModel { Id = t.Id, Name = t.Name });
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            return PartialView();
        }
    }
}