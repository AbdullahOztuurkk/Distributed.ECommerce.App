using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Infrastructure.Context;
using Clicco.Infrastructure.Repositories;
using Clicco.Infrastructure.Services;
using Clicco.Persistence.Repositories;
using Clicco.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Clicco.Infrastructure.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CliccoContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("CliccoContext"),sqlOpt =>
                {
                    sqlOpt.EnableRetryOnFailure(3, TimeSpan.FromSeconds(10), null);
                });
                opt.EnableSensitiveDataLogging();
            });

            services.AddScoped<ICacheManager, RedisCacheManager>();

            services.AddHttpContextAccessor();

            #region Repositories
            
            services.AddScoped<ITransactionDetailRepository,TransactionDetailRepository>();

            services.AddScoped<ITransactionRepository, TransactionRepository>();

            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IReviewRepository, ReviewRepository>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<ICouponRepository, CouponRepository>();

            services.AddScoped<IAddressRepository, AddressRepository>();

            services.AddScoped<IMenuRepository, MenuRepository>();

            services.AddScoped<IVendorRepository, VendorRepository>();

            #endregion

            #region Services

            services.AddScoped<ITransactionDetailService, TransactionDetailService>();

            services.AddScoped<ITransactionService, TransactionService>();

            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IReviewService, ReviewService>();

            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<ICouponService, CouponService>();

            services.AddScoped<IAddressService, AddressService>();

            services.AddScoped<IMenuService, MenuService>();

            services.AddScoped<IVendorService, VendorService>();

            #endregion

            return services;
        }
    }
}
