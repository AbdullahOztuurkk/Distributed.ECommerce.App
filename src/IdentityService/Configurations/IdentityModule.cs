using Autofac;
using CoreLib.DataAccess;
using IdentityService.API.Application.Services.Concrete;
using IdentityService.API.Persistence.Context;

namespace IdentityService.API.Configurations;

public class IdentityModule : Module
{
    private readonly IConfiguration _configuration;
    public IdentityModule(IConfiguration configuration)
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

        builder.AddUnitOfWorkContext<IdentityDbContext>(_configuration.GetConnectionString("SqlConnection"));
    }
}
