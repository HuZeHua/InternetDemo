using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using FluentValidation.Mvc;
using CarManager.Web.Models.Cars;

namespace CarManager.Web.Validator
{
    public class CarValidator : AbstractValidator<CarViewModel>
    {
        public CarValidator()
        {
            RuleFor(car => car.Name).NotNull().Length(5, 10);
            RuleFor(car => car.Email).EmailAddress();
        }
    }
}