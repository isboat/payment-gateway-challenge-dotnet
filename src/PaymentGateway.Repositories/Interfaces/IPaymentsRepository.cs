using PaymentGateway.Models.Responses;

namespace PaymentGateway.Repositories.Interfaces
{
    /// <summary>
    /// Data storage for payments
    /// </summary>
    public interface IPaymentsRepository
    {
        /// <summary>
        /// Add payment response
        /// </summary>
        /// <param name="payment"></param>
        void Add(PostPaymentResponse payment);

        /// <summary>
        /// Get or retrieve existing payment response
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        PostPaymentResponse Get(Guid id);
    }
}
