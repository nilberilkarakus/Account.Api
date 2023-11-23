using System;
using Application.UseCases.CreateAccount;
using Application.UseCases.GetAccountTransactions;
using AutoMapper;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Tests
{

    public class GetAccountTransactionsHandlerTests
    {
        private readonly Mock<IAccountRepository> _mockAccountRepository;
        private readonly Mock<ITransactionRepository> _mockTransactionRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<GetAccountTransactionsHandler>> _mockLogger;
        private GetAccountTransactionsRequest _mockGetAccountTransactionsRequest;
        private List<GetAccountTransactionsResponse>_mockGetAccountTransactionsResponse;
        private Account _mockAccount;
        private List<Transaction> _mockTransactionList;

        public GetAccountTransactionsHandlerTests()
        {
            _mockAccountRepository = new Mock<IAccountRepository>();
            _mockTransactionRepository = new Mock<ITransactionRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<GetAccountTransactionsHandler>>();
            _mockGetAccountTransactionsRequest = new GetAccountTransactionsRequest()
            {
                AccountId = new Guid("02fffc63-603c-40d6-bc64-451652cde192")
            };
            _mockGetAccountTransactionsResponse = new List<GetAccountTransactionsResponse>(){
                new GetAccountTransactionsResponse()
            {
                AccountId = new Guid("02fffc63-603c-40d6-bc64-451652cde192"),
                Id = Guid.NewGuid(),
                TransactionDate = DateTime.UtcNow,
                Amount = 0
            }
            };

            _mockAccount = new Account()
            {
                Balance = 0,
                CreatedDate = DateTime.UtcNow,
                CustomerId = Guid.NewGuid(),
                Id = new Guid("02fffc63-603c-40d6-bc64-451652cde192")
            };
            _mockTransactionList = new List<Transaction>()
            {
                new Transaction()
                {
                 TransactionDate = DateTime.UtcNow,
                 AccountId = new Guid("02fffc63-603c-40d6-bc64-451652cde192"),
                 Amount = 0,
                 Id = Guid.NewGuid()
                }
            };
        }
        [SetUp]
        public void Setup()
        {
            _mockMapper.Setup(operation => operation.Map<List<GetAccountTransactionsResponse>>(It.IsAny<List<Transaction>>()))
                .Returns(_mockGetAccountTransactionsResponse);
            _mockAccountRepository.Setup(operation =>
               operation.GetAccountById(It.IsAny<Guid>()))
               .ReturnsAsync(_mockAccount);
            _mockTransactionRepository.Setup(operation =>
               operation.GetTransactionsByAccountId(It.IsAny<Guid>()))
               .ReturnsAsync(_mockTransactionList);
        }

        [Test]
        public async Task GetAccountTransactionsHandler_Handle_Successful()
        {
            var queryHandler = new GetAccountTransactionsHandler(_mockLogger.Object, _mockTransactionRepository.Object, _mockAccountRepository.Object, _mockMapper.Object);
            var result = await queryHandler.Handle(_mockGetAccountTransactionsRequest, new System.Threading.CancellationToken());
            Assert.AreEqual(result.Count, _mockGetAccountTransactionsResponse.Count);
            _mockMapper.Verify(call => call.Map<List<GetAccountTransactionsResponse>>(It.IsAny<List<Transaction>>()), Times.AtLeastOnce);
            _mockAccountRepository.Verify(call => call.GetAccountById(It.IsAny<Guid>()), Times.AtLeastOnce);
            _mockTransactionRepository.Verify(call => call.GetTransactionsByAccountId(It.IsAny<Guid>()), Times.AtLeastOnce);

        }

        [Test]
        public void GetAccountTransactionsHandler_Handle_AccountNotFound_ThrowsAccountNotFoundException()
        {
            _mockAccountRepository.Setup(operation =>
                    operation.GetAccountById(It.IsAny<Guid>()))
                .ReturnsAsync((Account)null);
            var queryHandler = new GetAccountTransactionsHandler(_mockLogger.Object, _mockTransactionRepository.Object, _mockAccountRepository.Object, _mockMapper.Object);
            Assert.That(() => queryHandler.Handle(_mockGetAccountTransactionsRequest, new System.Threading.CancellationToken()), Throws.TypeOf<AccountNotFoundException>());
        }

        [Test]
        public void GetAccountTransactionsHandler_Handle_TransactionNotFound_ThrowsTransactionNotFoundException()
        {
            _mockTransactionRepository.Setup(operation =>
                    operation.GetTransactionsByAccountId(It.IsAny<Guid>()))
                .ReturnsAsync((List<Transaction>)null);
            var queryHandler = new GetAccountTransactionsHandler(_mockLogger.Object, _mockTransactionRepository.Object, _mockAccountRepository.Object, _mockMapper.Object);
            Assert.That(() => queryHandler.Handle(_mockGetAccountTransactionsRequest, new System.Threading.CancellationToken()), Throws.TypeOf<TransactionNotFoundException>());
        }
    }


}

