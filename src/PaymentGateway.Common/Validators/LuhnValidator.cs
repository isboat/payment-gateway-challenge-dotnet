using PaymentGateway.Models;
using PaymentGateway.Models.Requests;

namespace PaymentGateway.Common.Validators
{
    public class LuhnValidator : IPaymentRequestValidator
    {
        public ValidationCheckResult Validate(PostPaymentRequest request)
        {
            // Reverse the number and convert to an integer array
            var digits = request.CardNumber.ToCharArray().Reverse().Select(c => c - '0').ToArray();

            int sum = 0;

            for (int i = 0; i < digits.Length; i++)
            {
                int digit = digits[i];

                // Double every second digit
                if (i % 2 == 1)
                {
                    digit *= 2;
                    // If the result is greater than 9, subtract 9
                    if (digit > 9)
                    {
                        digit -= 9;
                    }
                }

                sum += digit;
            }

            // The number is valid if the sum modulo 10 is 0
            return new ValidationCheckResult
            {
                IsValid = sum % 10 == 0
            };
        }
    }
}
