using Clicco.Domain.Model;
using Clicco.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Clicco.Infrastructure.Context
{
    public class CliccoContext : DbContext
    {
        public DbSet<User> Users { get; set; }
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("", opt =>
            {
                opt.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
            });
        }
    }
}
