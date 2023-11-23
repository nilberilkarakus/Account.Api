using System;
using AutoMapper;
using Domain.DTOs;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.GetCustomer
{

    public class GetCustomerHandler : IRequestHandler<GetCustomerRequest, GetCustomerResponse>
    {
        private readonly ILogger<GetCustomerHandler> _logger;

        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public GetCustomerHandler(ILogger<GetCustomerHandler> logger, ICustomerRepository customerRepository, IAccountRepository accountRepository,ITransactionRepository transactionRepository,IMapper mapper)
        {
            _logger = logger;
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }


        public async Task<GetCustomerResponse> Handle(GetCustomerRequest getCustomerRequest, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerById(getCustomerRequest.CustomerId);

            if (customer == null)
                throw new CustomerNotFoundException("Customer not found for this account !");

            var account = await _accountRepository.GetAccountById(customer.AccountId);

            var accountDTO = _mapper.Map<AccountDTO>(account);

            var transactions = await _transactionRepository.GetTransactionsByAccountId(account.Id);

            var transactionDTO = _mapper.Map<List<TransactionDTO>>(transactions);


            var getCustomerResponse = new GetCustomerResponse()
            {
                Name = customer.Name,
                Surname = customer.Surname,
                Account = accountDTO,
                Transactions = transactionDTO

            };
            
            return getCustomerResponse;

        }
    }
}