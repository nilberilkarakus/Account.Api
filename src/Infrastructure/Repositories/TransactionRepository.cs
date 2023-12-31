﻿using System;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        public TransactionRepository()
        {
            using (var context = new AccountApiDbContext())
            {
                var transactions = new List<Transaction>
                {
                new Transaction
                {
                    Id = new Guid("0f5c7afd-b4fa-49b5-83c4-6085c40704d6"),
                    TransactionDate = DateTime.UtcNow,
                    AccountId = new Guid("02fffc63-603c-40d6-bc64-451652cde192"),
                    Amount = 10000
                },
                new Transaction
                {
                    Id = new Guid("4a8c019b-9c49-4cba-85d5-8a5275057a23"),
                    TransactionDate = DateTime.UtcNow,
                    AccountId = new Guid("e976f964-aaa1-4e22-8f8c-d75bd355643a"),
                    Amount = 100
                }
                };
                context.Transactions.AddRange(transactions);
                context.SaveChanges();
            }
        }
        public async Task<List<Transaction>> GetTransactionsByAccountId(Guid AccountId)
        {
            using (var context = new AccountApiDbContext())
            {
                var list = await context.Transactions.Where(transaction => transaction.AccountId == AccountId)
                    .ToListAsync();
                return list;
            }
        }

        public async Task<Transaction> CreateTransaction(Transaction transaction)
        {
            using (var context = new AccountApiDbContext())
            {
                context.Transactions.Add(transaction);
                await context.SaveChangesAsync();
                return transaction;
            }
        }
    }
}

