using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.Models.CustomAttributes
{
    /// <summary>
    /// Value must be in the future
    /// </summary>
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
