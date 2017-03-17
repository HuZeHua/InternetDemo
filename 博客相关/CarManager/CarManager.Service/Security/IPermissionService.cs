using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManager.Service.Security
{
    public interface IPermissionService
    {
        bool Authorize(string permissionName,string userName);
    }
}
