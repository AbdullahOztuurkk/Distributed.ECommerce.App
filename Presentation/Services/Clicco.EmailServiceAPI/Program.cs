using Clicco.Domain.Core.Extensions;
using Clicco.EmailServiceAPI;
using Clicco.EmailServiceAPI.Configurations;
using Clicco.EmailServiceAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddJsonFile("appsettings" + EnvironmentExtensions.Env + ".json", true, true)
    .Build();

builder.Configuration.AddConfiguration(configuration);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEmailServices();

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection(nameof(EmailSettings)));
builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection(nameof(RabbitMqSettings)));
builder.Services.AddHostedService<EmailWorker>();

builder.Configuration.AddEnvironmentVariables();

var app = builder.Build();

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
