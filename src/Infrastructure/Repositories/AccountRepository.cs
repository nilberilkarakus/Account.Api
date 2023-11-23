using System;
using System.Collections.Concurrent;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public AccountRepository()
        {
            using (var context = new AccountApiDbContext())
            {
                var accounts = new List<Account>
                {
                new Account
                {
                    Id = new Guid("02fffc63-603c-40d6-bc64-451652cde192"),
                    CreatedDate = DateTime.UtcNow,
                    CustomerId = new Guid("93527517-56ee-4e7f-9777-794fb193138d"),
                    Balance = 10000,
                    Transactions = new List<Transaction>() {
                    new Transaction
                    {
                    Id = new Guid("0f5c7afd-b4fa-49b5-83c4-6085c40704d6"),
                    TransactionDate = DateTime.UtcNow,
                    AccountId = new Guid("02fffc63-603c-40d6-bc64-451652cde192"),
                    Amount = 100
                    }
                 },
                },
                new Account
                {
                    Id = new Guid("e976f964-aaa1-4e22-8f8c-d75bd355643a"),
                    CreatedDate = DateTime.UtcNow,
                    CustomerId = new Guid("0d031dc6-e26e-4ed7-8424-7c6314695150"),
                    Balance = 200000,
                    Transactions = new List<Transaction>(){
                    new Transaction
                    {
                    Id = new Guid("4a8c019b-9c49-4cba-85d5-8a5275057a23"),
                    TransactionDate = DateTime.UtcNow,
                    AccountId = new Guid("e976f964-aaa1-4e22-8f8c-d75bd355643a"),
                    Amount = 100
                     }
                }
                },
                };
                context.Accounts.AddRange(accounts);
                context.SaveChanges();
            }
        }
        public List<Account> GetAccounts()
        {
            using (var context = new AccountApiDbContext())
            {
                var list = context.Accounts
                    .ToList();
                return list;
            }
        }

        public Account GetAccountById(Guid accountId)
        {
            using (var context = new AccountApiDbContext())
            {
                var account = context.Accounts.Where(account => account.Id == accountId).FirstOrDefault();
                return account;
            }
        }


        public Account CreateAccount(Account account)
        {
            using (var context = new AccountApiDbContext())
            {
                context.Accounts.Add(account);
                context.SaveChanges();
                return account;
            }
        }
    }

}