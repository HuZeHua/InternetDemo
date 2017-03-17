using System;
using System.ComponentModel;

namespace CarManager.Web.Models
{
    public class CustomerModel 
    {
        [DisplayName("客户姓名")]
        public string CustomName { get; set; }

        [DisplayName("创建时间")]
        public System.DateTime CreateDate { get; set; }

        [DisplayName("排序")]
        public int OrderIndex { get; set; }

    }
}