using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
	public class Transaction
	{
		public Transaction()
		{
		}

        [Key]
        public Guid Id { get; set; }

		public decimal Amount { get; set; }

		public DateTime TransactionDate { get; set; }

		public Guid AccountId { get; set; }
	}
}

