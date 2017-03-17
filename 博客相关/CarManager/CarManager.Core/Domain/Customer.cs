using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManager.Core.Domain
{
    public class Customer : BaseEntity
    {
        public string CustomName { get; set; }

        public DateTime CreateDate { get; set; }

        public int OrderIndex { get; set; }

    }
}
