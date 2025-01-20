using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using PaymentGateway.Api.Controllers;

using PaymentGateway.Api.Services;
using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.Repositories;
using PaymentGateway.Repositories.Interfaces;
using PaymentGateway.Services.Interfaces;
using PaymentGateway.Services;
using PaymentGateway.Common.Validators;
using PaymentGateway.Models.Requests;

namespace PaymentGateway.Api.Tests
{
    public class SubmitPaymentRequestModelValidationTests
    {
        private HttpClient CreateClient()
        {
            var webApplicationFactory = new WebApplicationFactory<PaymentsController>();
            var client = webApplicationFactory.WithWebHostBuilder(builder =>
                builder.ConfigureServices(services => ((ServiceCollection)services)
                    .AddSingleton<IPaymentsRepository, PaymentsRepository>()
                    .AddSingleton<IPaymentService, PaymentService>()
                    .AddSingleton<IPaymentRequestValidator, LuhnValidator>()
                )).CreateClient();

            return client;
        }

        [Fact]
        public async Task SubmitPayment_Request_Successfully()
        {
            // Arrange
            var client = CreateClient();
            var requestModel = CreatePaymentRequestModel();

            // Act
            var response = await client.PostAsJsonAsync($"/api/Payments", requestModel);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var model = await response.Content.ReadFromJsonAsync<ValidationModel>();
        }

        [Theory]
        [InlineData("", "The CardNumber field is required.")]
        [InlineData("12345", "Card number must be a string with a minimum length of 14 and a maximum length of 19.")]
        [InlineData("12345678901234567890", "Card number must be a string with a minimum length of 14 and a maximum length of 19.")]
        [InlineData("edfghfhfghfghf", "Only numeric characters are allowed.")]
        [InlineData("abcdefghjk123456789", "Only numeric characters are allowed.")]
        public async Task SubmitPayment_Request_CreditCard_Validation(string cardNumber, string errorMessage)
        {
            // Arrange
            var client = CreateClient();
            var requestModel = CreatePaymentRequestModel(cardNumber: cardNumber);

            // Act
            var response = await client.PostAsJsonAsync($"/api/Payments", requestModel);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var model = await response.Content.ReadFromJsonAsync<ValidationModel>();
            Assert.NotNull(model?.Errors?.CardNumber);
            Assert.Contains(errorMessage, model!.Errors.CardNumber);
        }

        [Theory]
        [InlineData(0, "Expiry Month must be between 1-12.")]
        [InlineData(13, "Expiry Month must be between 1-12.")]
        public async Task SubmitPayment_Request_ExpiryMonth_Validation(int month, string errorMessage)
        {
            // Arrange
            var client = CreateClient();
            var requestModel = CreatePaymentRequestModel(month: month);

            // Act
            var response = await client.PostAsJsonAsync($"/api/Payments", requestModel);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var model = await response.Content.ReadFromJsonAsync<ValidationModel>();
            Assert.NotNull(model?.Errors?.ExpiryMonth);
            Assert.Contains(errorMessage, model!.Errors.ExpiryMonth);
        }

        [Theory]
        [InlineData(0, "Expiry year must be in the future.")]
        [InlineData(2024, "Expiry year must be in the future.")]
        public async Task SubmitPayment_Request_ExpiryYear_Validation(int year, string errorMessage)
        {
            // Arrange
            var client = CreateClient();
            var requestModel = CreatePaymentRequestModel(year: year);

            // Act
            var response = await client.PostAsJsonAsync($"/api/Payments", requestModel);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var model = await response.Content.ReadFromJsonAsync<ValidationModel>();
            Assert.NotNull(model?.Errors?.ExpiryYear);
            Assert.Contains(errorMessage, model!.Errors.ExpiryYear);
        }

        [Theory]
        [InlineData("", "The Currency field is required.")]
        [InlineData("YEN", "Invalid currency supplied.")]
        public async Task SubmitPayment_Request_Currency_Validation(string currency, string errorMessage)
        {
            // Arrange
            var client = CreateClient();
            var requestModel = CreatePaymentRequestModel(currency: currency);

            // Act
            var response = await client.PostAsJsonAsync($"/api/Payments", requestModel);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var model = await response.Content.ReadFromJsonAsync<ValidationModel>();
            Assert.NotNull(model?.Errors?.Currency);
            Assert.Contains(errorMessage, model!.Errors.Currency);
        }

        [Theory]
        [InlineData("", "The Cvv field is required.")]
        [InlineData("abc", "Only numeric characters are allowed.")]
        [InlineData("a1c", "Only numeric characters are allowed.")]
        public async Task SubmitPayment_Request_Cvv_Validation(string cvv, string errorMessage)
        {
            // Arrange
            var client = CreateClient();
            var requestModel = CreatePaymentRequestModel(cvv: cvv);

            // Act
            var response = await client.PostAsJsonAsync($"/api/Payments", requestModel);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var model = await response.Content.ReadFromJsonAsync<ValidationModel>();
            Assert.NotNull(model?.Errors?.Cvv);
            Assert.Contains(errorMessage, model!.Errors.Cvv);
        }

        /// <summary>
        /// Creates a submit payment request model.
        /// If not arguments are passed, valid model is returned
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="cardNumber"></param>
        /// <param name="cvv"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        private static PostPaymentRequest CreatePaymentRequestModel(int? amount = null, int? month = null, int? year = null, string cardNumber = null, string cvv = null, string currency = null)
        {
            return new PostPaymentRequest
            {
                CardNumber = cardNumber ?? "123456789012345",
                Cvv = cvv ?? "123",
                Amount = amount ?? 0,
                Currency = currency ?? "gbp",
                ExpiryMonth = month ?? 1,
                ExpiryYear = year ?? DateTime.UtcNow.Year + 1
            };
        }
    }
}
