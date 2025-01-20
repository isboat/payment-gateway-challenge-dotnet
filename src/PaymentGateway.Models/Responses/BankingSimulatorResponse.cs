using System.Text.Json.Serialization;

namespace PaymentGateway.Models.Responses;

public class BankingSimulatorResponse
{
    [JsonPropertyName("authorized")]
    public bool Authorized { get; set; }

    [JsonPropertyName("authorization_code")]
    public string AuthorizationCode { get; set; }
}

public class BankingSimulatorErrorResponse
{
    [JsonPropertyName("errorMessage")]
    public string ErrorMessage { get; set; }
}
