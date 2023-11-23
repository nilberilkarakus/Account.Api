using System;
using Domain.Models;

namespace Application.UseCases.GetCustomer
{
	public class GetCustomerResponse
	{
       public string FirstName { get; set; }

	   public string LastName { get; set; }

	   public Account Account { get; set; }


	}
}

