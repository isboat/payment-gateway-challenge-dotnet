using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.Models.CustomAttributes
{
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
