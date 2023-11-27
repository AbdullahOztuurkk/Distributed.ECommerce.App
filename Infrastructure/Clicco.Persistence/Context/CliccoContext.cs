﻿using Clicco.Domain.Core;
using Clicco.Domain.Model;
using Clicco.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Clicco.Persistence.Context
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

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            Save();
            return await base.SaveChangesAsync();
        }

        public override int SaveChanges()
        {
            Save();
            return base.SaveChanges();
        }

        private void Save()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is BaseEntity entity)
                {
                    switch (entry.State)
                    {
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            entity.IsDeleted = true;

                            break;
                        case EntityState.Added:
                            entity.CreatedDate = DateTime.UtcNow.AddHours(3);
                            break;
                        case EntityState.Modified:
                            entity.UpdatedDate = DateTime.UtcNow.AddHours(3);
                            break;
                    }
                }
            }
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
