using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Infrastructure.Context;
using Clicco.Infrastructure.Repositories;
using Clicco.Infrastructure.Services;
using Clicco.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddScoped<ICacheManager, RedisCacheManager>();

            #region Repositories
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            
            services.AddScoped<IProductRepository, ProductRepository>();
            
            services.AddScoped<IReviewRepository, ReviewRepository>();
            
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            
            services.AddScoped<ICouponRepository,CouponRepository>();

            services.AddScoped<IAddressRepository , AddressRepository>();

            services.AddScoped<IMenuRepository, MenuRepository>();

            //services.AddScoped<UserRepository, UserRepository>();
            #endregion

            #region Services
            services.AddScoped<ITransactionService, TransactionService>();

            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IReviewService, ReviewService>();

            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<ICouponService, CouponService>();

            services.AddScoped<IAddressService, AddressService>();

            services.AddScoped<IMenuService, MenuService>();
            #endregion

            return services;
        }
    }
}
