using Payment.Service.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransitWithConsumers();
builder.Services.AddScoped<IBankServiceFactory, BankServiceFactory>();
builder.Services.AddHostedService<PaymentWorker>();
builder.Services.AddServiceDiscovery(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.RegisterToConsul(app.Lifetime, app.Configuration);

app.Run();
