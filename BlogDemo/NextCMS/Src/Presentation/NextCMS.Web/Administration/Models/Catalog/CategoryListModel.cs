using System;
using XCode.Web.Core.Mvc;

namespace XCode.Admin.Models.Catalog
{
    public class CategoryListModel : BaseNextCMSModel
    {
        public string Name { get; set; }

        public bool Published { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime CreatedOnDate { get; set; }

    }
}