using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.Models.CustomAttributes
{
    /// <summary>
    /// Ensuring submitted currency is allow
    /// </summary>
    public class IsoCodeCurrencyAttribute : ValidationAttribute
    {
        private readonly string[] allowedCurrency = ["usd", "eur", "gbp"];

        public override bool IsValid(object value)
        {
            if (value is string cur)
            {
                return allowedCurrency.Contains(cur.ToLowerInvariant());
            }
            return false;
        }
    }
}
