using System;
using Domain.Models;

namespace Domain.Interfaces
{
	public interface ITransactionRepository
	{
        public List<Transaction> GetTransactions();

        public Transaction CreateTransaction(Transaction transaction);

    }
}

