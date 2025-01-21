using PaymentGateway.Models.Responses;
using PaymentGateway.Models.Requests;

namespace PaymentGateway.Services.Interfaces
{
    /// <summary>
    /// Dedicated service to process payment transactions
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// Process the requested payment
        /// </summary>
        /// <param name="submitPaymentRequest"></param>
        /// <returns></returns>
        Task<PostPaymentResponse> ProcessPaymentAsync(PostPaymentRequest submitPaymentRequest);

        /// <summary>
        /// Get submited payment details by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PostPaymentResponse> GetAsync(Guid id);
    }
}
