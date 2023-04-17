using Clicco.AuthAPI.Data.Context;
using Clicco.AuthAPI.Data.Contracts;
using Clicco.AuthAPI.Data.Repositories;
using Clicco.AuthAPI.Data.Validators;
using Clicco.AuthAPI.Models;
using Clicco.AuthAPI.Services;
using Clicco.AuthAPI.Services.Contracts;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
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

            services.AddValidatorsFromAssembly(typeof(UserValidators).Assembly);

            services.AddHttpClient();

            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IUserService, UserService>();
            
            services.AddScoped<IEmailService,EmailService>();
            
            services.AddScoped<IUserRepository,UserRepository>();

            services.AddScoped<ITokenHandler<User>,Services.TokenHandler>();

            return services;
        }
    }
}
