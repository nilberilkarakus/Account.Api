using System;
using System.Data;
using FluentValidation;

namespace Application.UseCases.GetCustomer
{
    public class GetCustomerValidator : AbstractValidator<GetCustomerRequest>
    {
        public GetCustomerValidator()
        {
            RuleFor(rule => rule.CustomerId).NotNull().NotEmpty();

        }
    }
}

