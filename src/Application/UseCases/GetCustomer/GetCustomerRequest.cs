using System;
using MediatR;

namespace Application.UseCases.GetCustomer
{
	public class GetCustomerRequest: IRequest<GetCustomerResponse>
    {
		public Guid CustomerId { get; set; }
	}
}

