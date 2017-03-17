using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManager.Core.Domain
{
    public class Student : BaseEntity
    {
        public string Name { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
