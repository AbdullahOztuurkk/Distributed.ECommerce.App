using CoreLib.DataAccess.Concrete;
using IdentityService.API.Domain.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IdentityService.API.Persistence.Context;

public class IdentityDbContext : DbContextBase
{
    static string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    public IConfiguration Configuration => new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings" + env + ".json", true, true)
                .Build();

    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<ResetCode> ResetCodes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SqlConnection"));
        base.OnConfiguring(optionsBuilder);
    }
}

public class IdentityDbContextDesignTimeFactory : IDesignTimeDbContextFactory<IdentityDbContext>
{
    static string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    public IConfiguration Configuration => new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings" + env + ".json", true, true)
                .Build();
    public IdentityDbContext CreateDbContext(string[] args)
    {
        var optionBuilder = new DbContextOptionsBuilder<IdentityDbContext>();
        optionBuilder.UseSqlServer(Configuration.GetConnectionString("SqlConnection"));
        return new IdentityDbContext(optionBuilder.Options);
    }
}
