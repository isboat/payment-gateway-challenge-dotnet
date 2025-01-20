using PaymentGateway.Models.Responses;

namespace PaymentGateway.Repositories.Interfaces
{
    public interface IPaymentsRepository
    {
        void Add(PostPaymentResponse payment);

        PostPaymentResponse Get(Guid id);
    }
}
