using System;
namespace Domain.DTOs
{
	public class AccountDTO
	{
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid CustomerId { get; set; }

        public long Balance { get; set; }
    }
}

