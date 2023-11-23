using System;
namespace Domain.DTOs
{
	public class TransactionDTO
	{
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; }

        public Guid AccountId { get; set; }
    }
}

