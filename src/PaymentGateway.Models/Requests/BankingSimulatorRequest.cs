using System.Text.Json.Serialization;

namespace PaymentGateway.Models.Requests
{
    public class BankingSimulatorRequest
    {
        [JsonPropertyName("card_number")]
        public required string CardNumber { get; set; }

        [JsonPropertyName("expiry_date")]
        public required string ExpiryDate { get; set; }

        [JsonPropertyName("currency")]
        public required string Currency { get; set; }

        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("cvv")]
        public required int Cvv { get; set; }
    }
}
