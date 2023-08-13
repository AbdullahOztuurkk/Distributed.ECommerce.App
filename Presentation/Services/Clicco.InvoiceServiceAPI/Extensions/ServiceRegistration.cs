﻿using Clicco.InvoiceServiceAPI.Configurations;
using Clicco.InvoiceServiceAPI.Data.Common;
using Clicco.InvoiceServiceAPI.Data.Context;
using Clicco.InvoiceServiceAPI.Data.Repositories;
using Clicco.InvoiceServiceAPI.Data.Repositories.Contracts;
using Clicco.InvoiceServiceAPI.Services;
using Clicco.InvoiceServiceAPI.Services.Contracts;
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

        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<DbContext, MongoDbContext>();

            services.Configure<MongoDbSettings>(configuration.GetSection(nameof(MongoDbSettings)));
            services.Configure<RabbitMqSettings>(configuration.GetSection(nameof(RabbitMqSettings)));

            services
                .AddScoped<IInvoiceRepository, InvoiceRepository>()
                .AddScoped<IQueueService, RabbitMqService>();

            return services;
        }

    }
}
