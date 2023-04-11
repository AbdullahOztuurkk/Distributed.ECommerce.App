using Clicco.Domain.Core;
using Clicco.Domain.Model;
using Clicco.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Clicco.Infrastructure.Context
{
    public class CliccoContext : DbContext
    {
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
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Category> Categories { get; set; }

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
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-2QF0S4K;Initial Catalog=ParamECommerce;Integrated Security=True;", opt =>
            {
                opt.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
            });
        }
    }
}
