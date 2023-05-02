using Clicco.Application.Interfaces.Services.External;
using Clicco.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Clicco.Infrastructure.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddHttpClient()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IPaymentService, PaymentService>()
                .AddScoped<IEmailService, EmailService>()
                .AddScoped<IInvoiceService, InvoiceService>();

            return services;
        }
    }
}
