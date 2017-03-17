using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManager.Core.Domain
{
    public class EntityPermission : BaseEntity
    {
        public int EntityID { get; set; }

        public string EntityName { get; set; }

        public int RoleID { get; set; }
    }
}
