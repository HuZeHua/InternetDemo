using XCode.Web.Core.Mvc;

namespace XCode.Admin.Models.Authen
{
    public class PermissionListModel : BaseNextCMSModel
    {
        public PermissionListModel()
        {
 
        }

        public int ParentId { get; set; }

        public string BreadCrumb { get; set; }

        public string Name { get; set; }

        public string SystemName { get; set; }

        public string LinkUrl { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string Icon { get; set; }

        public string DisplayOrder { get; set; }

        public bool Active { get; set; }

        public bool Deleted { get; set; }
    }
}