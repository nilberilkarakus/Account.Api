using System;
using Application.UseCases.CreateAccount;
using Application.UseCases.GetCustomer;
using AutoMapper;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.GetAccountTransactions
{


    public class GetAccountTransactionsHandler: IRequestHandler<GetAccountTransactionsRequest, List<GetAccountTransactionsResponse>>
    {
        private readonly ILogger<GetAccountTransactionsHandler> _logger;

        private readonly ITransactionRepository _transactionRepository;

        private readonly IAccountRepository _accountRepository;

        private readonly IMapper _mapper;

        public GetAccountTransactionsHandler(ILogger<GetAccountTransactionsHandler> logger, ITransactionRepository transactionRepository, IAccountRepository accountRepository,IMapper mapper)
        {
            _logger = logger;
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }


        public async Task<List<GetAccountTransactionsResponse>> Handle(GetAccountTransactionsRequest getAccountTransactionsRequest, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAccountById(getAccountTransactionsRequest.AccountId);

            if (account == null)
                throw new AccountNotFoundException("Account not found");


            var transactions = await _transactionRepository.GetTransactionsByAccountId(getAccountTransactionsRequest.AccountId);

            if (transactions == null || transactions.Count == 0)
                throw new TransactionNotFoundException($"Transaction is not found for given AccountId {getAccountTransactionsRequest.AccountId}");

            var transactionList = _mapper.Map<List<GetAccountTransactionsResponse>>(transactions);

            return transactionList;

        }
    }
}

