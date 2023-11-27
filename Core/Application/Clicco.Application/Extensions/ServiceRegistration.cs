using Clicco.Application.Interfaces.Helpers;
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
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddScoped<IUserSessionService, UserSessionService>()
                    .AddScoped<IAddressService, AddressService>()
                    .AddScoped<ICategoryService, CategoryService>()
                    .AddScoped<ICouponService, CouponService>()
                    .AddScoped<IMenuService, MenuService>()
                    .AddScoped<IProductService, ProductService>()
                    .AddScoped<IReviewService, ReviewService>()
                    .AddScoped<ITransactionService, TransactionService>()
                    .AddScoped<IVendorService, VendorService>();

            return services;
        }
    }
}
