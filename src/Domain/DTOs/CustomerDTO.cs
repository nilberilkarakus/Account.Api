using System;
namespace Domain.DTOs
{
	public class CustomerDTO
	{
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public Guid AccountId { get; set; }
    }
}

