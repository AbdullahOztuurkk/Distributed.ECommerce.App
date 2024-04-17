using CommerceService.Domain.Concrete;
using CoreLib.DataAccess.Concrete;
using CoreLib.Entity.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CommerceService.Persistence.Context;

public class ECommerceContext : DbContextBase
{
    static string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env}.json", optional: false, reloadOnChange: true)
            .Build();

    public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options)
    {

    }

    public DbSet<Review> Reviews { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Menu> Menus { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Vendor> Vendors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Review>().HasQueryFilter(x => x.Status == StatusType.ACTIVE);
        modelBuilder.Entity<Product>().HasQueryFilter(x => x.Status == StatusType.ACTIVE);
        modelBuilder.Entity<Menu>().HasQueryFilter(x => x.Status == StatusType.ACTIVE);
        modelBuilder.Entity<Address>().HasQueryFilter(x => x.Status == StatusType.ACTIVE);
        modelBuilder.Entity<Transaction>().HasQueryFilter(x => x.Status == StatusType.ACTIVE);
        modelBuilder.Entity<Coupon>().HasQueryFilter(x => x.Status == StatusType.ACTIVE);
        modelBuilder.Entity<Category>().HasQueryFilter(x => x.Status == StatusType.ACTIVE);
        modelBuilder.Entity<Vendor>().HasQueryFilter(x => x.Status == StatusType.ACTIVE);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connStr = configuration.GetConnectionString("SqlConnection");
        ArgumentNullException.ThrowIfNull(connStr, "'SqlConnection' connection string not found!");
        optionsBuilder.UseSqlServer(connStr, opt =>
        {
            opt.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
        });
    }
}

public class ECommerceContextDesignTimeFactory : IDesignTimeDbContextFactory<ECommerceContext>
{
    static string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env}.json", optional: false, reloadOnChange: true)
            .Build();

    public ECommerceContext CreateDbContext(string[] args)
    {
        var connStr = configuration.GetConnectionString("SqlConnection");
        ArgumentNullException.ThrowIfNull(connStr, "'SqlConnection' connection string not found!");
        var builder = new DbContextOptionsBuilder<ECommerceContext>();
        builder.UseSqlServer(connStr);
        return new ECommerceContext(builder.Options);
    }
}
