using Email.Service.Extensions;
using EmailWorkerService.Application;
using EmailEntity = EmailWorkerService.Domain.Email;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new CoreModule(builder.Configuration)));
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new EmailModule(builder.Configuration)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServiceDiscovery(builder.Configuration);
builder.Services.AddCustomMapster(typeof(EmailEntity).Assembly);
builder.Services.AddHostedService<EmailWorker>();

builder.Services.AddMasstransitWithConsumers();

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
