using Clicco.AuthAPI.Data.EntityConfigurations;
using Clicco.AuthAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Clicco.AuthAPI.Data.Context
{
    public class AuthContext : DbContext
    {
        public IConfiguration Configuration => new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

        public AuthContext(DbContextOptions<AuthContext> options) : base(options) { }
        public AuthContext()
        {
            
        }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("AuthContext"));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is User user && entry.State == EntityState.Deleted)
                {
                    // Mark the entity as modified instead of deleting it
                    entry.State = EntityState.Modified;
                    // Set the Deleted flag to true
                    user.IsDeleted = true;
                }
            }
            return base.SaveChangesAsync();
        }

    }
}
