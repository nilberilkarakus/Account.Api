using System;
using Domain.Models;

namespace Domain.Interfaces
{
	public interface ITransactionRepository
	{
        public List<Transaction> GetTransactionsByAccountId(Guid AccountId);

        public Transaction CreateTransaction(Transaction transaction);

    }
}

