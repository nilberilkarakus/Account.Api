using System;
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

        public GetCustomerHandler(ILogger<GetCustomerHandler> logger, ICustomerRepository customerRepository, IAccountRepository accountRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
        }


        public async Task<GetCustomerResponse> Handle(GetCustomerRequest getCustomerRequest, CancellationToken cancellationToken)
        {
            var customer = _customerRepository.GetCustomerById(getCustomerRequest.CustomerId);

            if (customer == null)
                throw new CustomerNotFoundException("Customer not found !");

            var account = _accountRepository.GetAccountById(customer.AccountId);

            var response = new GetCustomerResponse()
            {
                FirstName = customer.Name,
                LastName = customer.Surname,
                Account = account
            };

            return response;

        }
    }
}