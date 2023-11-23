using System;
using Domain.Models;

namespace Domain.Interfaces
{
	public interface ICustomerRepository
	{
        public Customer GetCustomerById(Guid CustomerId);

    }
}

