using Clicco.AuthAPI.Data.EntityConfigurations;
using Clicco.AuthAPI.Models;
using Clicco.AuthServiceAPI.Data.EntityConfigurations;
using Clicco.AuthServiceAPI.Models;
using Clicco.Domain.Core.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Clicco.AuthAPI.Data.Context
{
    public class AuthContext : DbContext
    {
        public IConfiguration Configuration => new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile("appsettings" + EnvironmentExtensions.Env + ".json", true, true)
                    .Build();

        public AuthContext(DbContextOptions<AuthContext> options) : base(options) { }
        public AuthContext()
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<ResetCode> ResetCodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("AuthContext"));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ResetCodeEntityConfiguration());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<User>())
            {
                if (entry.State == EntityState.Deleted)
                {
                    // Mark the entity as modified instead of deleting it
                    entry.State = EntityState.Modified;
                    // Set the Deleted flag to true
                    entry.Entity.IsDeleted = true;
                }
            }
            return base.SaveChangesAsync();
        }
    }
}
