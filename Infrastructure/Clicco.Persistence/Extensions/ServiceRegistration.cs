using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.Interfaces.UnitOfWork;
using Clicco.Infrastructure.Context;
using Clicco.Infrastructure.Repositories;
using Clicco.Infrastructure.Services;
using Clicco.Persistence.Services;
using Clicco.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clicco.Infrastructure.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CliccoContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("CliccoContext"), sqlOpt =>
                {
                    sqlOpt.EnableRetryOnFailure(3, TimeSpan.FromSeconds(10), null);
                });
                opt.EnableSensitiveDataLogging();
            });

            services.AddScoped<ICacheManager, RedisCacheManager>();

            services.AddScoped<IUnitOfWork, UnitOfWork<CliccoContext>>();

            services.AddHttpContextAccessor();

            #region Repositories

            services.AddScoped<IMenuRepository, MenuRepository>();

            #endregion

            #region Services

            services.AddScoped<ITransactionDetailService, TransactionDetailService>();

            services.AddScoped<ITransactionService, TransactionService>();

            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<ICouponManagementHelper, CouponManagementHelper>();

            services.AddScoped<IAddressService, AddressService>();

            services.AddScoped<IMenuService, MenuService>();

            #endregion

            return services;
        }
    }
}
