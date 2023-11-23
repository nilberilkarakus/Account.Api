using System;
using MediatR;

namespace Application.UseCases.CreateAccount
{
	public class CreateAccountRequest:IRequest<CreateAccountResponse>
	{
		public long InitialCredit { get; set; }

		public Guid CustomerId { get; set; }
	}
}

