using Clicco.Application.Interfaces.Helpers;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.UnitOfWork;
using Clicco.Persistence.Context;
using Clicco.Persistence.Repositories;
using Clicco.Persistence.Services;
using Clicco.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clicco.Persistence.Extensions
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

            services.AddScoped<IUnitOfWork, UnitOfWork<CliccoContext>>();

            services.AddHttpContextAccessor();

            #region Repositories

            services.AddScoped<IMenuRepository, MenuRepository>();

            #endregion

            #region Services

            services.AddScoped<ICouponManagementHelper, CouponManagementHelper>();

            #endregion

            return services;
        }
    }
}
