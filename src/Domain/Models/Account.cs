using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
	public class Account
	{
		public Account()
		{
		}
        [Key]
        public Guid Id { get; set; }

		public DateTime CreatedDate { get; set; }

		public List<Transaction> Transactions { get; set; }

		public Guid CustomerId { get; set; }

		public long Balance { get; set; }

	}
}