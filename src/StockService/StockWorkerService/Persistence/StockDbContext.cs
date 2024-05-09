namespace StockWorkerService.Persistence;
public class StockDbContext : DbContextBase
{
    static string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env}.json", optional: false, reloadOnChange: true)
            .Build();

    public StockDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Domain.Entities.Stock> Stocks { get; set; }

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

public class StockDbContextDesignTimeFactory : IDesignTimeDbContextFactory<StockDbContext>
{
    static string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env}.json", optional: false, reloadOnChange: true)
            .Build();

    public StockDbContext CreateDbContext(string[] args)
    {
        var connStr = configuration.GetConnectionString("SqlConnection");
        ArgumentNullException.ThrowIfNull(connStr, "'SqlConnection' connection string not found!");
        var builder = new DbContextOptionsBuilder<StockDbContext>();
        builder.UseSqlServer(connStr);
        return new StockDbContext(builder.Options);
    }
}