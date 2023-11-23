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
                    Balance = 10000

                },
                new Account
                {
                    Id = new Guid("e976f964-aaa1-4e22-8f8c-d75bd355643a"),
                    CreatedDate = DateTime.UtcNow,
                    CustomerId = new Guid("0d031dc6-e26e-4ed7-8424-7c6314695150"),
                    Balance = 100
                }
                };
                context.Accounts.AddRange(accounts);
                context.SaveChanges();
            }
        }
        public async Task<List<Account>> GetAccounts()
        {
            using (var context = new AccountApiDbContext())
            {
                var list = await context.Accounts
                    .ToListAsync();
                return list;
            }
        }

        public async Task<Account> GetAccountById(Guid accountId)
        {
            using (var context = new AccountApiDbContext())
            {
                var account = await context.Accounts.Where(account => account.Id == accountId).FirstOrDefaultAsync();
                return account;
            }
        }


        public async Task<Account> CreateAccount(Account account)
        {
            using (var context = new AccountApiDbContext())
            {
                context.Accounts.Add(account);
                await context.SaveChangesAsync();
                return account;
            }
        }
    }

}