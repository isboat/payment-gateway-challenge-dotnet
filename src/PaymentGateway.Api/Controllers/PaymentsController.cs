using Microsoft.AspNetCore.Mvc;

using PaymentGateway.Models.Requests;
using PaymentGateway.Models.Responses;
using PaymentGateway.Services.Interfaces;

namespace PaymentGateway.Api.Controllers;

/// <summary>
/// Considering versioning the endpoint for future proof
/// starting with V1: example api/v1/payment
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PaymentsController : Controller
{
    private readonly IPaymentService _paymentService;
    private readonly ILogger<PaymentsController> _logger;

    public PaymentsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpGet("{id:guid}")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PostPaymentResponse?>> GetPaymentAsync(Guid id)
    {
        var payment = await _paymentService.GetAsync(id);
        
        if (payment == null)
        {
            return NotFound();
        }

        return new OkObjectResult(payment);
    }

    [HttpPost()]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PostPaymentResponse?>> PostPaymentRequest([FromBody] PostPaymentRequest request)
    {
        var paymentResponse = await _paymentService.ProcessPaymentAsync(request);
        if (paymentResponse == null) return BadRequest();

        return new OkObjectResult(paymentResponse);
    }
}