using System;
using MediatR;

namespace Application.UseCases.GetAccountTransactions
{
	public class GetAccountTransactionsRequest: IRequest<List<GetAccountTransactionsResponse>>
	{
		public Guid AccountId { get; set; }
	}
}

