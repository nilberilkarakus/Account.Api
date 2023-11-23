using System;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.CreateAccount
{
	public class CreateAccountHandler:IRequestHandler<CreateAccountRequest,CreateAccountResponse>
	{
		private readonly ILogger<CreateAccountHandler> _logger;
		private readonly IAccountRepository _accountRepository;

		public CreateAccountHandler(ILogger<CreateAccountHandler> logger, IAccountRepository accountRepository)
		{
			_logger = logger;
			_accountRepository = accountRepository;
		}


		public async Task<CreateAccountResponse> Handle(CreateAccountRequest createAccountRequest, CancellationToken cancellationToken)
		{
			var X = _accountRepository.GetAccounts();
			return new CreateAccountResponse();
		}
	}
}

