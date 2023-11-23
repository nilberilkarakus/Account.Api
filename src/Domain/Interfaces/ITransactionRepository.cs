using System;
using Domain.Models;

namespace Domain.Interfaces
{
	public interface ITransactionRepository
	{
        public Task<List<Transaction>> GetTransactionsByAccountId(Guid AccountId);

        public Task<Transaction> CreateTransaction(Transaction transaction);

    }
}

