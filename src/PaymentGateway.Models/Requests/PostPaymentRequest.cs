using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using PaymentGateway.Models.CustomAttributes;

namespace PaymentGateway.Models.Requests;

[FutureMonthYear(ErrorMessage = "Combination of both year and month must be in the future")]
public class PostPaymentRequest
{
    [Required]
    [StringLength(19, MinimumLength = 14, ErrorMessage = "Card number must be a string with a minimum length of 14 and a maximum length of 19.")]
    [RegularExpression(@"^\d+$", ErrorMessage = "Only numeric characters are allowed.")]
    [JsonPropertyName("cardNumber")]
    public required string CardNumber { get; set; }

    [Required]
    [Range(1, 12, ErrorMessage = "Expiry Month must be between 1-12.")]
    [JsonPropertyName("expiryMonth")]
    public int ExpiryMonth { get; set; }

    [Required]
    [FutureYear(ErrorMessage = "Expiry year must be in the future.")]
    [JsonPropertyName("expiryYear")]
    public int ExpiryYear { get; set; }

    [Required]
    [StringLength(3, MinimumLength = 3)]
    [IsoCodeCurrency(ErrorMessage = "Invalid currency supplied.")]
    [JsonPropertyName("currency")]
    public required string Currency { get; set; }

    [Required]
    [JsonPropertyName("amount")]
    public int Amount { get; set; }

    [Required]
    [StringLength(4, MinimumLength = 3)]
    [RegularExpression(@"^\d+$", ErrorMessage = "Only numeric characters are allowed.")]
    [JsonPropertyName("cvv")]
    public required string Cvv { get; set; }
}