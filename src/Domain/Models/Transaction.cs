using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
	public class Transaction
	{
        [Key]
        public Guid Id { get; set; }

		public long Amount { get; set; }

		public DateTime TransactionDate { get; set; }

		public Guid AccountId { get; set; }

	}
}

