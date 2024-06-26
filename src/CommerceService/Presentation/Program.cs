using CommerceService.API.Extensions;
using Consul;
using CoreLib.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new CoreModule(builder.Configuration)));
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new CommerceModule(builder.Configuration)));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureAuth(builder.Configuration);
builder.Services.AddCustomMapster(typeof(Address).Assembly);
builder.Services.AddMassTransitWithConsumers();
builder.Services.AddServiceDiscovery(builder.Configuration);
builder.Services.AddSingleton(s => s.ConfigureRedis(builder.Configuration));


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ECommerce API",
        Version = "v1"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.ConfigureTechBuddyExceptionHandling();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.RegisterToConsul(app.Lifetime, app.Configuration);

app.Run();
