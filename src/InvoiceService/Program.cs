using Shared.Domain.Constant;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationDependencies(builder.Configuration);
builder.Services.AddMongoDb();

builder.Services.AddHostedService<InvoiceWorker>();

builder.Services.AddMassTransit(conf =>
{
    conf.AddConsumer<CreateInvoiceRequestConsumer>();
    conf.AddConsumer<SendInvoiceDetailEmailRequestConsumer>();
    conf.UsingRabbitMq((context, busConf) =>
    {
        busConf.Host(RabbitMqConstant.Host, RabbitMqConstant.Port, h =>
        {
            h.Username(RabbitMqConstant.Username);
            h.Password(RabbitMqConstant.Password);
        });

        busConf.ReceiveEndpoint(QueueNames.SendInvoiceDetailEmailRequestQueue, h =>
        {
            h.ConfigureConsumer<SendInvoiceDetailEmailRequestConsumer>(context);
        });

        busConf.ReceiveEndpoint(QueueNames.CreateInvoiceRequestQueue, h =>
        {
            h.ConfigureConsumer<CreateInvoiceRequestConsumer>(context);
        });
    });
});

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
