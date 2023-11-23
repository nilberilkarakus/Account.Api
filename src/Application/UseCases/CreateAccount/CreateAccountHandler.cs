using System;
using AutoMapper;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.CreateAccount
{
	public class CreateAccountHandler:IRequestHandler<CreateAccountRequest,CreateAccountResponse>
	{
		private readonly ILogger<CreateAccountHandler> _logger;
		private readonly IAccountRepository _accountRepository;
		private readonly ICustomerRepository _customerRepository;
		private readonly ITransactionRepository _transactionRepository;
		private readonly IMapper _mapper;

		public CreateAccountHandler(ILogger<CreateAccountHandler> logger, IAccountRepository accountRepository, ICustomerRepository customerRepository, ITransactionRepository transactionRepository,IMapper mapper)
		{
			_logger = logger;
			_accountRepository = accountRepository;
			_customerRepository = customerRepository;
			_transactionRepository = transactionRepository;
			_mapper = mapper;
		}


		public async Task<CreateAccountResponse> Handle(CreateAccountRequest createAccountRequest, CancellationToken cancellationToken)
		{
			var customer = await _customerRepository.GetCustomerById(createAccountRequest.CustomerId);

            if (customer == null)
                throw new CustomerNotFoundException("Customer not found !");

            var newAccount = new Account()
            {
                CreatedDate = DateTime.UtcNow,
                CustomerId = customer.Id,
                Id = Guid.NewGuid(),
                Balance = createAccountRequest.InitialCredit
            };

            if (createAccountRequest.InitialCredit > 0)
			{
				var transaction = new Transaction()
				{
					Id = Guid.NewGuid(),
					TransactionDate = DateTime.UtcNow,
					AccountId = newAccount.Id,
					Amount = createAccountRequest.InitialCredit
				};

				await _transactionRepository.CreateTransaction(transaction);
			}

			var createdAccount = await _accountRepository.CreateAccount(newAccount);

			var createAccountResponse = _mapper.Map<CreateAccountResponse>(createdAccount);

			return createAccountResponse;


		}
	}
}

