using System;
using Domain.Models;

namespace Application.UseCases.GetAccountTransactions
{
	public class GetAccountTransactionsResponse
	{
        public Guid Id { get; set; }

        public long Amount { get; set; }

        public DateTime TransactionDate { get; set; }

        public Guid AccountId { get; set; }
    }
}

