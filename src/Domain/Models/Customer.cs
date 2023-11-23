using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
	public class Customer
	{
		public Customer()
		{
		}

        [Key]

        public Guid Id { get; set; }

		public string Name { get; set; }

		public string Surname { get; set; }

		public Guid AccountId { get; set; }

	}
}

