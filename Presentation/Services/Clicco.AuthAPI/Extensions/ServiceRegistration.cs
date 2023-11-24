using Clicco.AuthAPI.Data.Context;
using Clicco.AuthAPI.Data.Contracts;
using Clicco.AuthAPI.Data.Repositories;
using Clicco.AuthAPI.Models;
using Clicco.AuthAPI.Services;
using Clicco.AuthAPI.Services.Contracts;
using Clicco.AuthServiceAPI.Data.Contracts;
using Clicco.AuthServiceAPI.Data.Repositories;
using Clicco.AuthServiceAPI.Services;
using Clicco.AuthServiceAPI.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Clicco.AuthAPI.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddFundamentalServices(this IServiceCollection services, IConfiguration configuration)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AUTH_API_KEY"]));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddDbContext<AuthContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("AuthContext"), sqlOpt =>
                {
                    sqlOpt.EnableRetryOnFailure(3, TimeSpan.FromSeconds(10), null);
                });
                opt.EnableSensitiveDataLogging();
            });

            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IQueueService, RabbitMqService>();

            services.AddScoped<IResetCodeRepository, ResetCodeRepository>();

            services.AddScoped<ITokenHandler<User>, Services.TokenHandler>();

            return services;
        }
    }
}
