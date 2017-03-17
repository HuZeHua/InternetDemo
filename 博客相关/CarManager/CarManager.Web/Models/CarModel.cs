using System;
using System.ComponentModel;

namespace CarManager.Web.Models
{
    public class CarModel 
    {
        [DisplayName("汽车名字")]
        public string CarName { get; set; }

        public decimal Price { get; set; }

        public System.DateTime CreateDate { get; set; }

    }
}