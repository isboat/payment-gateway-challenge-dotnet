using PaymentGateway.Models.Requests;
using PaymentGateway.Models.Responses;

namespace PaymentGateway.Services.Interfaces
{
    /// <summary>
    /// Http client for handle banking simulation
    /// </summary>
    public interface IBankingSimulatorHttpClient
    {
        /// <summary>
        /// Submit payment request to the simulator
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BankingSimulatorResponse?> SubmitPaymentAsync(PostPaymentRequest request);
    }
}
