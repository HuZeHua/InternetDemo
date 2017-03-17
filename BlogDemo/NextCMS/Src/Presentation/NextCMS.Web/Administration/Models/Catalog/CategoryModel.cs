﻿using System.Collections.Generic;
using System.Web.Mvc;
using FluentValidation.Attributes;
using XCode.Admin.Validators.Catalog;
using XCode.Web.Core.Mvc;

namespace XCode.Admin.Models.Catalog
{
    [Validator(typeof(CategoryValidator))]
    public partial class CategoryModel : BaseNextCMSModel
    {
        public CategoryModel()
        {
            this.AvailableCategories = new List<SelectListItem>();
            this.Published = true;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public string MetaTitle { get; set; }

        public int ParentId { get; set; }

        public int PictureId { get; set; }

        public bool ShowOnHomePage { get; set; }

        public bool IncludeInTopMenu { get; set; }

        public bool Published { get; set; }

        public int DisplayOrder { get; set; }

        public bool Deleted { get; set; }

        public string CreatedOnDate { get; set; }

        public string UpdatedOnDate { get; set; }

        public List<SelectListItem> AvailableCategories { get; set; }
    }
}