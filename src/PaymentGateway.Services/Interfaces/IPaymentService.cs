using PaymentGateway.Models.Responses;
using PaymentGateway.Models.Requests;

namespace PaymentGateway.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<PostPaymentResponse> ProcessPaymentAsync(PostPaymentRequest submitPaymentRequest);

        Task<PostPaymentResponse> GetAsync(Guid id);
    }
}
