using System;
using Domain.Models;

namespace Domain.Interfaces
{
	public interface IAccountRepository
	{
        public Task<List<Account>> GetAccounts();

        public Task<Account> CreateAccount(Account account);

        public Task<Account> GetAccountById(Guid accountId);


    }
}

