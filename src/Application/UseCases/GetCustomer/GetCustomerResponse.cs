using System;
using Domain.DTOs;
using Domain.Models;

namespace Application.UseCases.GetCustomer
{
	public class GetCustomerResponse
	{
       public string Name { get; set; }

	   public string Surname { get; set; }

	   public AccountDTO Account { get; set; }

	   public List<TransactionDTO> Transactions { get; set; }

	}
}

