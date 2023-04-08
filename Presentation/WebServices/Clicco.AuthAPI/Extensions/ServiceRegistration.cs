using Clicco.AuthAPI.Data.Contracts;
using Clicco.AuthAPI.Data.Repositories;
using Clicco.AuthAPI.Data.Validators;
using Clicco.AuthAPI.Models;
using Clicco.AuthAPI.Services.Contracts;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace Clicco.AuthAPI.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddFundamentalServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Token:Issuer"],
                    ValidAudience = configuration["Token:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddValidatorsFromAssembly(typeof(UserValidators).Assembly);

            services.AddScoped<IAuthRepository, AuthRepository>();
            
            services.AddScoped<IUserRepository,UserRepository>();

            services.AddScoped<ITokenHandler<User>,Services.TokenHandler>();

            return services;
        }
    }
}
