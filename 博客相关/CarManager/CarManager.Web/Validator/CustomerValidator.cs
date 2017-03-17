using FluentValidation;
using CarManager.Web.Models;

namespace CarManager.Web.Validator
{
    public class CustomerModelValidator : AbstractValidator<CustomerModel>
    {
        public CustomerModelValidator()
        {
            RuleFor(customer => customer.CustomName).Length(5, 15);

            RuleFor(customer => customer.CreateDate);

            RuleFor(customer => customer.OrderIndex).GreaterThanOrEqualTo(0).LessThanOrEqualTo(100);

        }
    }
}

