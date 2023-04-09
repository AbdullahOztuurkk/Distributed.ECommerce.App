using Clicco.Application.Extensions;
using Clicco.Infrastructure.Extensions;
using Clicco.WebAPI.Models;
using Clicco.WebAPI.NewFolder;
using FluentValidation;
using TechBuddy.Extensions.AspNetCore.ExceptionHandling;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddSingleton(sp => sp.ConfigureRedis(builder.Configuration));
builder.Services.ConfigureAuth(builder.Configuration);
builder.Services.AddSingleton<SystemAdministratorFilter>();

builder.Services.AddSwaggerGen();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
