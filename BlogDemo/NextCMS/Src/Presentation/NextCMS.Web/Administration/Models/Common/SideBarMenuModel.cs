using System.Collections.Generic;
using XCode.Web.Core.Mvc;

namespace XCode.Admin.Models.Common
{
    public class SideBarMenuModel : BaseNextCMSModel
    {
        public SideBarMenuModel()
        {
            this.ChildMenus = new List<SideBarMenuModel>();
        }

        public string Name { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string Icon { get; set; }

        public bool Selected { get; set; }

        public List<SideBarMenuModel> ChildMenus { get; set; }
    }
}