using CarManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CarManager.Web.Mvc
{
    public class Acls
    {
        public static readonly Permission StudentCreate = new Permission { Category = "Student", Name = nameof(StudentCreate) };
        public static readonly Permission StudentUpdate = new Permission { Category = "Student", Name = nameof(StudentUpdate) };

        public static readonly Permission fafa = new Permission { Category = "fa", Name = "xxxx" };

        public IEnumerable<Permission> GetPermissions() 
        {
            var ps = this.GetType().GetFields(BindingFlags.Static & BindingFlags.Public).Where(t=>t.FieldType==typeof(Permission));

            return ps.Select(p => p.GetValue(this) as Permission);
        }
    }
}