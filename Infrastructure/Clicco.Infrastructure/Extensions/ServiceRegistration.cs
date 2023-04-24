using Clicco.Application.Interfaces.Services.External;
using Clicco.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Clicco.Infrastructure.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IPaymentService, PaymentService>();

            return services;
        }
    }
}
