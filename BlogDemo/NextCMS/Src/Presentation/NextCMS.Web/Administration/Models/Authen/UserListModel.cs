using System.Collections.Generic;
using XCode.Web.Core.Mvc;

namespace XCode.Admin.Models.Authen
{
    public partial class UserListModel : BaseNextCMSModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Active { get; set; }
        public string RegisterDate { get; set; }

        public ICollection<int> SelectedRoles { get; set; }
    }
}