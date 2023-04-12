using Clicco.Application.Extensions;
using Clicco.Infrastructure.Extensions;
using Clicco.WebAPI.Models;
using Clicco.WebAPI.NewFolder;
using FluentValidation;
using Microsoft.OpenApi.Models;
using TechBuddy.Extensions.AspNetCore.ExceptionHandling;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices();
builder.Services.AddSingleton(sp => sp.ConfigureRedis(builder.Configuration));
builder.Services.ConfigureAuth(builder.Configuration);
builder.Services.AddSingleton<SystemAdministratorFilter>();

builder.Services.AddSwaggerGen(c => {
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

app.ConfigureTechBuddyExceptionHandling(opt =>
{
    opt.AddCustomHandler<ValidationException>((context, ex, logger) =>
    {
        logger.LogError("Unhandled exception occured");
        var dynamicResponseModel = new  DynamicResponseModel(ErrorMessage: ex.Message, ErrorType: "ValidationException" );

        // we can set the response but don't have to
        return context.Response.WriteAsJsonAsync(dynamicResponseModel);
    });

    // All the other exceptions
    opt.UseCustomHandler(async (context, ex, logger) =>
    {
        logger.LogError("Unhandled exception occured");
        var dynamicResponseModel = new DynamicResponseModel ( ErrorMessage: ex.Message, ErrorType: "Exception");

        // we can set the response but don't have to
        await context.Response.WriteAsJsonAsync(dynamicResponseModel);
    });

});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
