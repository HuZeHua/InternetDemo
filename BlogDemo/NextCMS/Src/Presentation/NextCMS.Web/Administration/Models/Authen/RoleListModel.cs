using XCode.Web.Core.Mvc;

namespace XCode.Admin.Models.Authen
{
    public class RoleListModel : BaseNextCMSModel
    {
        public RoleListModel()
        {
            this.Active = true;
        }

        public string Name { get; set; }
        public string SystemName { get; set; }
        public bool Active { get; set; }
    }
}