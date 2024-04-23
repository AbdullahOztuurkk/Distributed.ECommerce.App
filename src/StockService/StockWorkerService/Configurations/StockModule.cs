using Autofac;
using CoreLib.DataAccess;
using StockWorkerService.Application.Services.Concrete;
using StockWorkerService.Persistence;

namespace StockWorkerService.Configurations;
public class StockModule : Module
{
    private readonly IConfiguration _configuration;

    public StockModule(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.AddUnitOfWorkContext<StockDbContext>(_configuration.GetConnectionString("SqlConnection"));

        builder.RegisterAssemblyTypes(typeof(BaseService).Assembly)
            .Where(t => t.Name.EndsWith("Service"))
            .AsImplementedInterfaces()
            .PropertiesAutowired()
            .InstancePerLifetimeScope();
    }
}
