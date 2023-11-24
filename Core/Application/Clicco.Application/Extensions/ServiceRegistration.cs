using Clicco.Application.Profiles;
using Clicco.Application.Services.Abstract;
using Clicco.Application.Services.Concrete;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Clicco.Application.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<GeneralProfile>();
                cfg.AddProfile<ResponseProfile>();
            });

            services.AddScoped<IUserSessionService, UserSessionService>();

            services.AddScoped<IAddressService, AddressService>();

            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<ICouponService, CouponService>();

            return services;
        }
    }
}
