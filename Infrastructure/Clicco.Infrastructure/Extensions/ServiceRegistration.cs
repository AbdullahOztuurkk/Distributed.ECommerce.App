using Clicco.Application.Interfaces.Services.External;
using Clicco.Infrastructure.Configurations;
using Clicco.Infrastructure.HostedServices;
using Clicco.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clicco.Infrastructure.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMqSettings>(configuration.GetSection(nameof(RabbitMqSettings)));

            services
                .AddScoped<IPaymentService, PaymentService>()
                .AddScoped<IEmailService, EmailService>()
                .AddScoped<IInvoiceService, InvoiceService>()
                .AddScoped<IQueueService, RabbitMqService>();

            services.AddHostedService<TransactionWorker>();

            return services;
        }
    }
}
