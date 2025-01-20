using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace PaymentGateway.Api.Middlewares
{
    public class ExceptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionsMiddleware> _logger;

        public ExceptionsMiddleware(RequestDelegate next, ILogger<ExceptionsMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occurred: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            int statusCode;
            string message;

            if (exception is HttpRequestException httpRequestException)
            {
                if (httpRequestException.StatusCode == null)
                {
                    statusCode = StatusCodes.Status500InternalServerError;
                    message = "Unexpected error occurred";
                }
                else
                {
                    var httpStatusCode = (HttpStatusCode)httpRequestException.StatusCode;

                    statusCode = httpStatusCode switch
                    {
                        HttpStatusCode.Unauthorized => StatusCodes.Status401Unauthorized,
                        HttpStatusCode.Forbidden => StatusCodes.Status403Forbidden,
                        HttpStatusCode.NotFound => StatusCodes.Status404NotFound,
                        HttpStatusCode.BadRequest => StatusCodes.Status400BadRequest,
                        _ => StatusCodes.Status500InternalServerError
                    };
                    message = httpRequestException.Message;
                }
            }
            else
            {
                statusCode = StatusCodes.Status500InternalServerError;
                message = "Server Error";
            }

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = message,
                Detail = exception.Message
            };

            context.Response.StatusCode = statusCode;
            await context.Response
                .WriteAsJsonAsync(problemDetails);
        }
    }
}
