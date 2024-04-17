namespace EmailWorkerService.Persistence;

public class EmailDbContext : DbContextBase
{
    static string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env}.json", optional: false, reloadOnChange: true)
            .Build();

    public EmailDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Email> Emails { get; set; }

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
public class EmailDbContextDesignTimeFactory : IDesignTimeDbContextFactory<EmailDbContext>
{
    static string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env}.json", optional: false, reloadOnChange: true)
            .Build();

    public EmailDbContext CreateDbContext(string[] args)
    {
        var connStr = configuration.GetConnectionString("SqlConnection");
        ArgumentNullException.ThrowIfNull(connStr, "'SqlConnection' connection string not found!");
        var builder = new DbContextOptionsBuilder<EmailDbContext>();
        builder.UseSqlServer(connStr);
        return new EmailDbContext(builder.Options);
    }
}
