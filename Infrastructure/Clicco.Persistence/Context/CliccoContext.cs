using Clicco.Domain.Core;
using Clicco.Domain.Model;
using Clicco.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Clicco.Infrastructure.Context
{
    public class CliccoContext : DbContext
    {
        public IConfiguration configuration
        {
            get
            {
                return new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables()
                    .Build();
            }
        }

        public CliccoContext(DbContextOptions<CliccoContext> options) : base(options)
        {

        }

        public CliccoContext()
        {

        }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionDetail> TransactionDetails { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Vendor> Vendors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AddressEntityConfiguration).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is ISoftDeletable deletable && entry.State == EntityState.Deleted)
                {
                    // Mark the entity as modified instead of deleting it
                    entry.State = EntityState.Modified;
                    // Set the Deleted flag to true
                    deletable.IsDeleted = true;
                }
            }
            return base.SaveChangesAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("CliccoContext"), opt =>
            {
                opt.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
            });
        }
    }
}
