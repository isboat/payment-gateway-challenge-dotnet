using PaymentGateway.Models.Requests;
using PaymentGateway.Models.Responses;

namespace PaymentGateway.Services.Interfaces
{
    public interface IBankingSimulatorHttpClient
    {
        Task<BankingSimulatorResponse?> SubmitPaymentAsync(PostPaymentRequest request);
    }
}
