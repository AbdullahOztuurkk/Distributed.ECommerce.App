using Clicco.Application.Extensions;
using Clicco.Domain.Core.Extensions;
using Clicco.Infrastructure.Extensions;
using Clicco.Persistence.Extensions;
using Clicco.WebAPI.Filters;
using Clicco.WebAPI.Middlewares;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddJsonFile("appsettings" + EnvironmentExtensions.Env + ".json", true, true)
    .Build();

builder.Configuration.AddConfiguration(configuration);
// Add services to the container.

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<RequestValidationFilter>();
})
    .AddFluentValidation()
    .ConfigureApiBehaviorOptions(opt => opt.SuppressModelStateInvalidFilter = true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddSingleton(sp => sp.ConfigureRedis(builder.Configuration));
builder.Services.ConfigureAuth(builder.Configuration);
builder.Services.AddScoped<SystemAdministratorFilter>();
builder.Services.AddScoped<ExceptionHandlingMiddleware>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Clicco API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
