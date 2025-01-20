using PaymentGateway.Repositories.Interfaces;
using PaymentGateway.Models.Responses;

namespace PaymentGateway.Repositories;

public class PaymentsRepository: IPaymentsRepository
{
    private List<PostPaymentResponse> payments = new();
    
    public void Add(PostPaymentResponse payment)
    {
        payments.Add(payment);
    }

    public PostPaymentResponse Get(Guid id)
    {
        return payments.FirstOrDefault(p => p.Id == id);
    }
}
