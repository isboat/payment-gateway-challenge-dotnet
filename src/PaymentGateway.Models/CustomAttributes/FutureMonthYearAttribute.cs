using PaymentGateway.Models.Requests;
using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.Models.CustomAttributes
{
    /// <summary>
    /// Ensuring the combination of expiry month + year is in the future
    /// </summary>
    public class FutureMonthYearAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is PostPaymentRequest paymentRequest)
            {
                var currentDate = DateTime.UtcNow;
                var expiryDate = new DateTime(paymentRequest.ExpiryYear, paymentRequest.ExpiryMonth, 1);
                return expiryDate > currentDate;
            }
            return false;
        }
    }
}
