using System;
using FluentValidation;

namespace Application.UseCases.CreateAccount
{
	public class CreateAccountValidator:AbstractValidator<CreateAccountRequest>
	{
		public CreateAccountValidator()
		{
			RuleFor(rule => rule.CustomerId).NotNull().NotEmpty();
            RuleFor(x => x.InitialCredit)
             .Custom((x, context) =>
               {
                 if ( x < 0)
                 {
                   context.AddFailure($"{x} is not a valid number or less than 0");
                 }
             });
        }
	}
}

