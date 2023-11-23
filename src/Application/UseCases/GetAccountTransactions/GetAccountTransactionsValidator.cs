using System;
using Application.UseCases.GetCustomer;
using FluentValidation;

namespace Application.UseCases.GetAccountTransactions
{

    public class GetAccountTransactionsValidator : AbstractValidator<GetAccountTransactionsRequest>
    {
        public GetAccountTransactionsValidator()
        {
            RuleFor(rule => rule.AccountId).NotNull().NotEmpty();

        }
    }
}

