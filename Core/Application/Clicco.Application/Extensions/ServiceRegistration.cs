using Clicco.Application.Behaviours;
using Clicco.Application.Profiles;
using FluentValidation;
using MediatR;
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
                cfg.AddProfile<ViewModelProfile>();
            });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            return services;
        }
    }
}
