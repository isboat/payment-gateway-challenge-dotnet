using System.Text.Json;
using System.Net.Http;
using PaymentGateway.Services.Interfaces;
using PaymentGateway.Models.Responses;
using PaymentGateway.Models.Requests;
using System.Text;
using Microsoft.Extensions.Logging;

namespace PaymentGateway.Api.Services;

/// <inheritdoc />
public class BankingSimulatorHttpClient : IBankingSimulatorHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<BankingSimulatorHttpClient> _logger;

    public BankingSimulatorHttpClient(HttpClient httpClient, ILogger<BankingSimulatorHttpClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<BankingSimulatorResponse?> SubmitPaymentAsync(PostPaymentRequest request)
    {
        var bankingSimulatorRequest = new BankingSimulatorRequest()
        {
            CardNumber = request.CardNumber,
            ExpiryDate = $"{PadMonthInteger(request.ExpiryMonth)}/{request.ExpiryYear}",
            Currency = request.Currency,
            Amount = request.Amount,
            Cvv = int.Parse(request.Cvv)
        };

        try
        {
            var bodyContent = new StringContent(
                JsonSerializer.Serialize(bankingSimulatorRequest),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync("/payments", bodyContent);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<BankingSimulatorResponse>(content);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during payment submission.");
            return null;
        }
    }

    private static string PadMonthInteger(int number) 
    { 
        if (number < 10) 
        { 
            return "0" + number; 
        } 
        return number.ToString(); 
    }
}
