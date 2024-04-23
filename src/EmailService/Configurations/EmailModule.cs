using Autofac;
using CoreLib.DataAccess;
using EmailWorkerService.Persistence;

namespace EmailWorkerService.Configurations;

public class EmailModule : Autofac.Module
{
    private readonly IConfiguration _configuration;

    public EmailModule(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.AddUnitOfWorkContext<EmailDbContext>(_configuration.GetConnectionString("SqlConnection"));

        builder.RegisterAssemblyTypes(typeof(BaseService).Assembly)
            .Where(t => t.Name.EndsWith("Service"))
            .AsImplementedInterfaces()
            .PropertiesAutowired()
            .InstancePerLifetimeScope();

        builder.RegisterType<ResourceService>()
            .As<IResourceService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<ContentBuilder>()
            .As<IContentBuilder>()
            .InstancePerLifetimeScope();
    }
}
