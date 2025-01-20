using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PaymentGateway.Repositories.Interfaces;

namespace PaymentGateway.Repositories.Tests
{
    public class PaymentsRepositoryTests
    {
        // subject under test
        private IPaymentsRepository _paymentsRepository = new PaymentsRepository();

        [Fact]
        public void GetPaymentInfo_Successfully()
        {
            // Arrange
            var id = Guid.NewGuid();
            _paymentsRepository.Add(new Models.Responses.PostPaymentResponse {  Id = id });

            // Act
            var response = _paymentsRepository.Get(id);

            // Assert
            Assert.NotNull(response);
        }

        // Add more scenarios
    }
}
