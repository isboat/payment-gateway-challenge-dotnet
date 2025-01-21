using PaymentGateway.Services.Interfaces;
using PaymentGateway.Common.Utils;
using PaymentGateway.Models.Responses;
using PaymentGateway.Models.Requests;
using PaymentGateway.Repositories.Interfaces;
using PaymentGateway.Models;
using PaymentGateway.Common.Validators;
using Microsoft.Extensions.Logging;

namespace PaymentGateway.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentsRepository _paymentRepository;
    private readonly IBankingSimulatorHttpClient _bankingService;
    private readonly IEnumerable<IPaymentRequestValidator> _paymentRequestValidators;
    //private readonly IMerchantService _merchantService;
    private readonly ILogger<PaymentService> _logger;

    public PaymentService(
        IPaymentsRepository paymentRepository, 
        IBankingSimulatorHttpClient bankingService, 
        ILogger<PaymentService> logger, 
        IEnumerable<IPaymentRequestValidator> paymentRequestValidators)
    {
        _paymentRepository = paymentRepository;
        _bankingService = bankingService;
        _logger = logger;
        _paymentRequestValidators = paymentRequestValidators;
    }

    public async Task<PostPaymentResponse> GetAsync(Guid id)
    {
        return await Task.FromResult(_paymentRepository.Get(id));
    }

    public async Task<PostPaymentResponse> ProcessPaymentAsync(PostPaymentRequest submitPaymentRequest)
    {
        if (submitPaymentRequest == null) return null;

        var paymentResponse = new PostPaymentResponse
        {
            Id = Guid.NewGuid(),
            Status = PaymentStatus.Rejected.ToString(),
            Amount = submitPaymentRequest.Amount,
            Currency = submitPaymentRequest.Currency,
            ExpiryMonth = submitPaymentRequest.ExpiryMonth,
            ExpiryYear = submitPaymentRequest.ExpiryYear,
            CardNumberLastFour = submitPaymentRequest.CardNumber.GetLastFourDigits()
        };

        // performs some validations on the payment request information
        var validationResult = new List<ValidationCheckResult>();
        foreach (var validator in _paymentRequestValidators)
        {
            var result = validator.Validate(submitPaymentRequest);
            if (!result.IsValid)
            {
                _logger.LogWarning($"{result.Error}");
            }
            validationResult.Add(result);
        }
        //if (validationResult.Any(x => !x.IsValid)) return paymentResponse;


        // Submit to bank
        var simulatorResponse = await _bankingService.SubmitPaymentAsync(submitPaymentRequest);
        if (simulatorResponse == null) return paymentResponse;

        // store payment result in repo
        // use a library to convert the bank result to pyament response result
        paymentResponse.Status = simulatorResponse.Authorized ? PaymentStatus.Authorized.ToString() : PaymentStatus.Declined.ToString();
        if(!string.IsNullOrEmpty(simulatorResponse.AuthorizationCode)) paymentResponse.Id = Guid.Parse(simulatorResponse.AuthorizationCode);

        _paymentRepository.Add(paymentResponse);

        return paymentResponse;
    }

}