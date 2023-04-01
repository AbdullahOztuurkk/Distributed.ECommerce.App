using Clicco.Application.Interfaces.Repositories;
using Clicco.Infrastructure.Context;
using Clicco.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Clicco.Infrastructure.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CliccoContext>(opt =>
            {
                opt.UseSqlServer(configuration["CliccoDatabaseConnectionString"],sqlOpt =>
                {
                    sqlOpt.EnableRetryOnFailure(3, TimeSpan.FromSeconds(10), null);
                });
                opt.EnableSensitiveDataLogging();
            });

            services.AddScoped<ITransactionRepository, TransactionRepository>();
            
            services.AddScoped<IProductRepository, ProductRepository>();
            
            services.AddScoped<IReviewRepository, ReviewRepository>();
            
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            
            services.AddScoped<ICouponRepository,CouponRepository>();

            services.AddScoped<IAddressRepository , AddressRepository>();

            services.AddScoped<IMenuRepository, MenuRepository>();

            services.AddScoped<UserRepository, UserRepository>();

            return services;
        }
    }
}
