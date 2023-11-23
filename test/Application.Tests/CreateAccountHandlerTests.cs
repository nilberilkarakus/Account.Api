using Application.UseCases.CreateAccount;
using AutoMapper;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Tests
{
    public class CreateAccountHandlerTests
    {
        private readonly Mock<IAccountRepository> _mockAccountRepository;
        private readonly Mock<ICustomerRepository> _mockCustomerRepository;
        private readonly Mock<ITransactionRepository> _mockTransactionRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<CreateAccountHandler>> _mockLogger;
        private CreateAccountRequest _mockCreateAccountRequest;
        private CreateAccountResponse _mockCreateAccountResponse;
        private Account _mockAccount;
        private Customer _mockCustomer;
        private Transaction _mockTransaction;

        public CreateAccountHandlerTests()
        {
            _mockAccountRepository = new Mock<IAccountRepository>();
            _mockTransactionRepository = new Mock<ITransactionRepository>();
            _mockCustomerRepository = new Mock<ICustomerRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<CreateAccountHandler>>();
            _mockCreateAccountRequest = new CreateAccountRequest()
            {
                CustomerId = Guid.NewGuid(),
                InitialCredit = 0
            };
            _mockCreateAccountResponse = new CreateAccountResponse()
            {
                Balance = 0,
                CreatedDate = DateTime.UtcNow,
                CustomerId = new Guid("93527517-56ee-4e7f-9777-794fb193138d"),
                Id = new Guid("02fffc63-603c-40d6-bc64-451652cde192"),
            };

            _mockCustomer = new Customer()
            {
                AccountId = new Guid("02fffc63-603c-40d6-bc64-451652cde192"),
                Id = new Guid("93527517-56ee-4e7f-9777-794fb193138d"),
                Surname = "test",
                Name = "testname"
            };

            _mockTransaction = new Transaction()
            {
                TransactionDate = DateTime.UtcNow,
                AccountId = new Guid("02fffc63-603c-40d6-bc64-451652cde192"),
                Amount = 0,
                Id = Guid.NewGuid()
            };
        }
        [SetUp]
        public void Setup()
        {
            _mockMapper.Setup(operation => operation.Map<CreateAccountResponse>(It.IsAny<Account>()))
                .Returns(_mockCreateAccountResponse);
            _mockCustomerRepository.Setup(operation =>
                    operation.GetCustomerById(It.IsAny<Guid>()))
                .ReturnsAsync(_mockCustomer);
            _mockAccountRepository.Setup(operation =>
               operation.CreateAccount(It.IsAny<Account>()))
               .ReturnsAsync(_mockAccount);
            _mockTransactionRepository.Setup(operation =>
               operation.CreateTransaction(It.IsAny<Transaction>()))
               .ReturnsAsync(_mockTransaction);
        }

        [Test]
        public async Task CreateAccountHandler_Handle_InitialCredit0_Successful()
        {
            var queryHandler = new CreateAccountHandler(_mockLogger.Object, _mockAccountRepository.Object, _mockCustomerRepository.Object, _mockTransactionRepository.Object, _mockMapper.Object);
            var result = await queryHandler.Handle(_mockCreateAccountRequest, new System.Threading.CancellationToken());
            Assert.AreEqual(result, _mockCreateAccountResponse);
            _mockMapper.Verify(call => call.Map<CreateAccountResponse>(It.IsAny<Account>()), Times.AtLeastOnce);
            _mockAccountRepository.Verify(call => call.CreateAccount(It.IsAny<Account>()), Times.AtLeastOnce);
            _mockCustomerRepository.Verify(call => call.GetCustomerById(It.IsAny<Guid>()), Times.AtLeastOnce);
            _mockTransactionRepository.Verify(call => call.CreateTransaction(It.IsAny<Transaction>()), Times.Never);

        }

        [Test]
        public async Task CreateAccountHandler_Handle_InitialCreditGreaterThan0_Successful()
        {
            _mockCreateAccountRequest.InitialCredit = 500;
            var queryHandler = new CreateAccountHandler(_mockLogger.Object, _mockAccountRepository.Object, _mockCustomerRepository.Object, _mockTransactionRepository.Object, _mockMapper.Object);
            var result = await queryHandler.Handle(_mockCreateAccountRequest, new System.Threading.CancellationToken());
            Assert.AreEqual(result, _mockCreateAccountResponse);
            _mockMapper.Verify(call => call.Map<CreateAccountResponse>(It.IsAny<Account>()), Times.AtLeastOnce);
            _mockAccountRepository.Verify(call => call.CreateAccount(It.IsAny<Account>()), Times.AtLeastOnce);
            _mockCustomerRepository.Verify(call => call.GetCustomerById(It.IsAny<Guid>()), Times.AtLeastOnce);
            _mockTransactionRepository.Verify(call => call.CreateTransaction(It.IsAny<Transaction>()), Times.AtLeastOnce);

        }
        [Test]
        public void CreateAccountHandler_Handle_CustomerNotFound_ThrowsCustomerNotFoundException()
        {
            _mockCustomerRepository.Setup(operation =>
                    operation.GetCustomerById(It.IsAny<Guid>()))
                .ReturnsAsync((Customer)null);
            var queryHandler = new CreateAccountHandler(_mockLogger.Object, _mockAccountRepository.Object, _mockCustomerRepository.Object, _mockTransactionRepository.Object, _mockMapper.Object);
            Assert.That(() => queryHandler.Handle(_mockCreateAccountRequest, new System.Threading.CancellationToken()), Throws.TypeOf<CustomerNotFoundException>());
        }
    }
}