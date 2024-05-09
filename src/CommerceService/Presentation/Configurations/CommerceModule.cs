using CommerceService.Application.Services.Concrete;
using CommerceService.Persistence.Context;
using CoreLib.DataAccess;
using CoreLib.Utilities.Cache;

namespace CommerceService.API.Configurations;

public class CommerceModule : Module
{
    private readonly IConfiguration _configuration;

    public CommerceModule(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(BaseService).Assembly)
            .Where(t => t.Name.EndsWith("Service"))
            .AsImplementedInterfaces()
            .PropertiesAutowired()
            .InstancePerLifetimeScope();

        builder.AddUnitOfWorkContext<ECommerceContext>(_configuration.GetConnectionString("SqlConnection"));

        //builder.AddRedisCache(_configuration.GetConnectionString("Redis"));
    }
}
