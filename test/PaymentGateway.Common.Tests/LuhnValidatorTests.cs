using PaymentGateway.Common.Validators;
using PaymentGateway.Models.Requests;

namespace PaymentGateway.Common.Tests
{
    public class LuhnValidatorTests
    {
        // service under test
        private LuhnValidator _luhnValidator = new LuhnValidator();

        [Fact]
        public void Unsuccessful_Test()
        {
            // Arrange

            // Act
            var response = _luhnValidator.Validate(new PostPaymentRequest 
            { 
                CardNumber = "12345678901234",
                Amount = 10,
                Currency = "GBP",
                Cvv = "123"
            });

            // Assert
            Assert.NotNull(response);
            Assert.False(response.IsValid);
        }


        [Fact]
        public void Successful_Test()
        {
            // Arrange

            // Act
            var response = _luhnValidator.Validate(new PostPaymentRequest
            {
                CardNumber = "2222405343248877",
                Amount = 10,
                Currency = "GBP",
                Cvv = "123"
            });

            // Assert
            Assert.NotNull(response);
            Assert.True(response.IsValid);
        }
    }
}