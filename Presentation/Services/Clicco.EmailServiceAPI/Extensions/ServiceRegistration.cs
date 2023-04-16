using Clicco.EmailServiceAPI.Services;
using Clicco.EmailServiceAPI.Services.Contracts;

namespace Clicco.EmailServiceAPI.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddEmailServices(this IServiceCollection services)
        {
            services.AddScoped<IContentBuilder, ContentBuilder>();
            
            services.AddScoped<ITemplateParser, TemplateParser>();
            
            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<IQueueService, RabbitMqService>();

            return services;
        }
    }
}
