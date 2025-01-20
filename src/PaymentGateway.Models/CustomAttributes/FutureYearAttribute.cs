using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.Models.CustomAttributes
{
    public class FutureYearAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is int year)
            {
                return year >= DateTime.Now.Year;
            }
            return false;
        }
    }
}
