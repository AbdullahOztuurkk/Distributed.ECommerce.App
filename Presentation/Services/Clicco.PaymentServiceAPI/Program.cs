using Clicco.PaymentServiceAPI;
using Clicco.PaymentServiceAPI.Configurations;
using Clicco.PaymentServiceAPI.Services.Contracts;
using Clicco.PaymentServiceAPI.Services.External;
using Clicco.PaymentServiceAPI.Services.Factory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBankServiceFactory, BankServiceFactory>();
builder.Services.AddScoped<IQueueService, RabbitMqService>();
builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection(nameof(RabbitMqSettings)));
builder.Services.AddHostedService<PaymentWorker>();

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
