namespace PaymentGateway.Models
{
    public class ValidationCheckResult
    {
        public bool IsValid { get; set; }
        public string? Error { get; set; }
    }
}
