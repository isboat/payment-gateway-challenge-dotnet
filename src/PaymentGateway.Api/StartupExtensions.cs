using PaymentGateway.Api.Services;
using PaymentGateway.Api.Settings;
using PaymentGateway.Common.Validators;
using PaymentGateway.Repositories;
using PaymentGateway.Repositories.Interfaces;
using PaymentGateway.Services;
using PaymentGateway.Services.Interfaces;

namespace PaymentGateway.Api
{
    public static class StartupExtensions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IPaymentsRepository, PaymentsRepository>();
            services.AddSingleton<IPaymentService, PaymentService>();
            services.AddSingleton<IPaymentRequestValidator, LuhnValidator>();
        }

        public static IServiceCollection AddSimulatorClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IBankingSimulatorHttpClient, BankingSimulatorHttpClient>(nameof(BankingSimulatorHttpClient), client =>
            {
                var settings = configuration.GetSection(nameof(SimulatorSettings))
                    .Get<SimulatorSettings>() ?? throw new NullReferenceException();

                client.BaseAddress = new Uri(settings.BaseUri);
            });
            return services;
        }
    }
}
