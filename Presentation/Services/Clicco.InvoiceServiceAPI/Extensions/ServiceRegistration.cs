using Clicco.InvoiceServiceAPI.Data.Common;
using Clicco.InvoiceServiceAPI.Data.Context;
using Clicco.InvoiceServiceAPI.Data.Repositories;
using Clicco.InvoiceServiceAPI.Data.Repositories.Contracts;
using Clicco.InvoiceServiceAPI.Services;
using Clicco.InvoiceServiceAPI.Services.Contracts;
using Clicco.InvoiceServiceAPI.Settings;
using Microsoft.Extensions.Options;

namespace Clicco.InvoiceServiceAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, IOptions<MongoDbSettings> opt)
        {
            var dbContext = new MongoDbContext(opt);

            return services.AddSingleton<DbContext>(dbContext);
        }

        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            return services
                .AddHttpClient()
                .AddScoped<IInvoiceRepository, InvoiceRepository>()
                .AddScoped<IEmailService, EmailService>()
                .AddScoped<IInvoiceService, InvoiceService>();
        }

    }
}
