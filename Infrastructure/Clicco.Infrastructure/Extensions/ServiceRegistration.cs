using Clicco.Application.Interfaces.Services.External;
using Clicco.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clicco.Infrastructure.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient(nameof(InvoiceService), client =>
            {
                client.BaseAddress = new Uri(configuration["URLS:INVOICE_SERVICE_API"]);
            });

            services.AddHttpClient(nameof(EmailService), client =>
            {
                client.BaseAddress = new Uri(configuration["URLS:EMAIL_SERVICE_API"]);
            });

            services.AddHttpClient(nameof(PaymentService), client =>
            {
                client.BaseAddress = new Uri(configuration["URLS:PAYMENT_SERVICE_API"]);
            });

            services.AddHttpClient(nameof(UserService), client =>
            {
                client.BaseAddress = new Uri(configuration["URLS:AUTH_SERVICE_API"]);
            });

            services.AddScoped<IUserService, UserService>()
                .AddScoped<IPaymentService, PaymentService>()
                .AddScoped<IEmailService, EmailService>()
                .AddScoped<IInvoiceService, InvoiceService>();

            return services;
        }
    }
}
