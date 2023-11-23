using System;
using Domain.Models;

namespace Domain.Interfaces
{
	public interface IAccountRepository
	{
        public List<Account> GetAccounts();

        public Account CreateAccount(Account account);

        public Account GetAccountById(Guid accountId);


    }
}

