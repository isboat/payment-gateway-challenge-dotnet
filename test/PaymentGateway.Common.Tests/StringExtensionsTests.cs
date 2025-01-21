using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PaymentGateway.Common.Utils;

namespace PaymentGateway.Common.Tests
{
    public class StringExtensionsTests
    {
        [Fact]
        public void StringExtensions_NullOrEmptyString_Check()
        {
            // Arrange

            // Act
            var response = Assert.Throws<ArgumentException>(() => "".GetLastFourDigits());

            // Assert
        }

        [Fact]
        public void StringExtensions_FormatException_Check()
        {
            // Arrange

            // Act
            var response = Assert.Throws<FormatException>(() => "123456789012345678901234567890poiu".GetLastFourDigits());

            // Assert
        }
        [Fact]
        public void StringExtensions_FormatExceptionBigInterger_Check()
        {
            // Arrange

            // Act
            var response = Assert.Throws<FormatException>(() => "1234rfgt".GetLastFourDigits());

            // Assert
        }


        [Fact]
        public void StringExtensions_Success_Check()
        {
            // Arrange

            // Act
            var response = "123456487".GetLastFourDigits();

            // Assert
            Assert.Equal(6487, response);
        }
    }
}
