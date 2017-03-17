using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FluentValidation;
using FluentValidation.Attributes;
using CarManager.Web.Validator;

namespace CarManager.Web.Models.Cars
{
    public class CarViewModel
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Email { get; set; }
    }
}