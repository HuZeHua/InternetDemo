using System.Collections.Generic;
using XCode.Admin.Models.Common;

namespace XCode.Admin.Models.Authen
{
    public partial class UserSearchModel : DataTableParameter
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Active { get; set; }

        public ICollection<int> SelectedRoles { get; set; }
    }
}