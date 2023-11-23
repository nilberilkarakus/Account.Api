using System;
using Domain.Models;

namespace Domain.Interfaces
{
	public interface ICustomerRepository
	{
        public Task<Customer> GetCustomerById(Guid CustomerId);

    }
}

