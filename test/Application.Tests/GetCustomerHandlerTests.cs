using System;
using Application.UseCases.CreateAccount;
using Application.UseCases.GetCustomer;
using AutoMapper;
using Domain.DTOs;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Tests
{
    public class GetCustomerHandlerTests
    {
        private readonly Mock<IAccountRepository> _mockAccountRepository;
        private readonly Mock<ICustomerRepository> _mockCustomerRepository;
        private readonly Mock<ITransactionRepository> _mockTransactionRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<GetCustomerHandler>> _mockLogger;
        private GetCustomerRequest _mockGetCustomerRequest;
        private GetCustomerResponse _mockGetCustomerResponse;
        private Account _mockAccount;
        private AccountDTO _mockAccountDTO;
        private Customer _mockCustomer;
        private List<Transaction> _mockTransactionList;
        private List<TransactionDTO> _mockTransactionDTOList;

        public GetCustomerHandlerTests()
        {
            _mockAccountRepository = new Mock<IAccountRepository>();
            _mockTransactionRepository = new Mock<ITransactionRepository>();
            _mockCustomerRepository = new Mock<ICustomerRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<GetCustomerHandler>>();
            _mockCustomer = new Customer()
            {
                AccountId = new Guid("02fffc63-603c-40d6-bc64-451652cde192"),
                Id = new Guid("93527517-56ee-4e7f-9777-794fb193138d"),
                Surname = "test",
                Name = "testname"
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

            _mockAccountDTO = new AccountDTO()
            {
                Balance = 0,
                CreatedDate = DateTime.UtcNow,
                CustomerId = Guid.NewGuid(),
                Id = new Guid("02fffc63-603c-40d6-bc64-451652cde192")
            };

            _mockTransactionDTOList = new List<TransactionDTO>()
            {
                new TransactionDTO()
                {
                 TransactionDate = DateTime.UtcNow,
                 AccountId = new Guid("02fffc63-603c-40d6-bc64-451652cde192"),
                 Amount = 0,
                 Id = Guid.NewGuid()
                }
            };

            _mockGetCustomerRequest = new GetCustomerRequest()
            {
                CustomerId = new Guid("93527517-56ee-4e7f-9777-794fb193138d")
            };


            _mockGetCustomerResponse = new GetCustomerResponse()
            {
                Name = "testname",
                Surname = "test",
                Account = _mockAccountDTO,
                Transactions = _mockTransactionDTOList
            };


        }
        [SetUp]
        public void Setup()
        {
            _mockMapper.Setup(operation => operation.Map<AccountDTO>(It.IsAny<Account>()))
                .Returns(_mockAccountDTO);
            _mockMapper.Setup(operation => operation.Map<List<TransactionDTO>>(It.IsAny<List<Transaction>>()))
                 .Returns(_mockTransactionDTOList);
            _mockCustomerRepository.Setup(operation =>
                    operation.GetCustomerById(It.IsAny<Guid>()))
                .ReturnsAsync(_mockCustomer);
            _mockAccountRepository.Setup(operation =>
               operation.GetAccountById(It.IsAny<Guid>()))
               .ReturnsAsync(_mockAccount);
            _mockTransactionRepository.Setup(operation =>
               operation.GetTransactionsByAccountId(It.IsAny<Guid>()))
               .ReturnsAsync(_mockTransactionList);
        }

        [Test]
        public async Task GetCustomerHandler_Handle_Successful()
        {
            var queryHandler = new GetCustomerHandler(_mockLogger.Object, _mockCustomerRepository.Object, _mockAccountRepository.Object, _mockTransactionRepository.Object, _mockMapper.Object);
            var result = await queryHandler.Handle(_mockGetCustomerRequest, new System.Threading.CancellationToken());

            Assert.AreEqual(result.Name, _mockGetCustomerResponse.Name);
            Assert.AreEqual(result.Surname, _mockGetCustomerResponse.Surname);
            Assert.AreEqual(result.Account, _mockGetCustomerResponse.Account);
            Assert.AreEqual(result.Transactions.Count, _mockGetCustomerResponse.Transactions.Count);
            _mockMapper.Verify(call => call.Map<AccountDTO>(It.IsAny<Account>()), Times.AtLeastOnce);
            _mockMapper.Verify(call => call.Map<List<TransactionDTO>>(It.IsAny<List<Transaction>>()), Times.AtLeastOnce);
            _mockAccountRepository.Verify(call => call.GetAccountById(It.IsAny<Guid>()), Times.AtLeastOnce);
            _mockCustomerRepository.Verify(call => call.GetCustomerById(It.IsAny<Guid>()), Times.AtLeastOnce);
            _mockTransactionRepository.Verify(call => call.GetTransactionsByAccountId(It.IsAny<Guid>()), Times.AtLeastOnce);

        }

        [Test]
        public void GetCustomerHandler_Handle_CustomerNotFound_ThrowsCustomerNotFoundException()
        {
            _mockCustomerRepository.Setup(operation =>
                    operation.GetCustomerById(It.IsAny<Guid>()))
                .ReturnsAsync((Customer)null);
            var queryHandler = new GetCustomerHandler(_mockLogger.Object, _mockCustomerRepository.Object, _mockAccountRepository.Object, _mockTransactionRepository.Object, _mockMapper.Object);
            Assert.That(() => queryHandler.Handle(_mockGetCustomerRequest, new System.Threading.CancellationToken()), Throws.TypeOf<CustomerNotFoundException>());
        }
    }
}

