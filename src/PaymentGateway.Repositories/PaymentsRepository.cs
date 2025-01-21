using PaymentGateway.Repositories.Interfaces;
using PaymentGateway.Models.Responses;

namespace PaymentGateway.Repositories;

/// <inheritdoc />
public class PaymentsRepository: IPaymentsRepository
{
    private readonly List<PostPaymentResponse> _payments = [];

    /// <inheritdoc />
    public void Add(PostPaymentResponse payment)
    {
        _payments.Add(payment);
    }

    /// <inheritdoc />
    public PostPaymentResponse Get(Guid id)
    {
        return _payments.FirstOrDefault(p => p.Id == id);
    }
}
