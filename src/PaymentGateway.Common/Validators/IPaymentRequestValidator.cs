using PaymentGateway.Models;
using PaymentGateway.Models.Requests;

namespace PaymentGateway.Common.Validators
{
    public interface IPaymentRequestValidator
    {
        ValidationCheckResult Validate(PostPaymentRequest request);
    }
}
