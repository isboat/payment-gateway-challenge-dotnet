using Microsoft.Extensions.Logging;

using Moq;

using PaymentGateway.Common.Validators;
using PaymentGateway.Models.Requests;
using PaymentGateway.Repositories.Interfaces;
using PaymentGateway.Services.Interfaces;

using Xunit;

using Assert = Xunit.Assert;
namespace PaymentGateway.Services.Tests
{
    public class PaymentServiceTests
    {
        // subject under test
        private IPaymentService _paymentService;

        private Mock<IPaymentsRepository> _paymentRepository = new();
        private Mock<IBankingSimulatorHttpClient> _bankingSimulatorHttpClient = new();
        private Mock<ILogger<PaymentService>> _logger = new();
        private List<IPaymentRequestValidator> _validators = new List<IPaymentRequestValidator>();

        public PaymentServiceTests() 
        {
            _paymentService = new PaymentService(
                _paymentRepository.Object, 
                _bankingSimulatorHttpClient.Object, 
                _logger.Object, 
                _validators);
        }
        [Fact]
        public async Task GetPayment_Successfully()
        {
            _paymentRepository.Setup(x => x.Get(It.IsAny<Guid>())).Returns(new Models.Responses.PostPaymentResponse());
            var response = await _paymentService.GetAsync(Guid.NewGuid());
            Assert.NotNull(response);
        }

        [Fact]
        public async Task ProcessPaymentAsync_Successfully()
        {
            // Arrrange
            var request = new PostPaymentRequest
            {
                CardNumber = "2222405343248877",
                Amount = 100,
                Currency = "GBP",
                Cvv = "123",
                ExpiryMonth = 4,
                ExpiryYear = 2025
            };

            var validator = new Mock<IPaymentRequestValidator>();
            validator.Setup(x => x.Validate(It.IsAny<PostPaymentRequest>())).Returns(new Models.ValidationCheckResult { IsValid = true });

            _validators.Add(validator.Object);
            _bankingSimulatorHttpClient.Setup(x => x.SubmitPaymentAsync(It.IsAny<PostPaymentRequest>()))
                .ReturnsAsync(new Models.Responses.BankingSimulatorResponse { AuthorizationCode = Guid.NewGuid().ToString(), Authorized = true });
            
            // Act
            var response = await _paymentService.ProcessPaymentAsync(request);

            // Arrange
            Assert.NotNull(response);
            Assert.Equal(Models.PaymentStatus.Authorized, response.Status);

        }
    }
}
