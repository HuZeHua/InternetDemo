using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManager.Core.Domain
{
    public class Car : BaseEntity
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
