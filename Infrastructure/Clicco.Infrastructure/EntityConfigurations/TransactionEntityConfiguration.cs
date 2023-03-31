using Clicco.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clicco.Infrastructure.EntityConfigurations
{
    public class TransactionEntityConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Code)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Dealer)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.CreatedDate).IsRequired();

            builder.Property(x => x.DeliveryDate).IsRequired();

            builder.Property(x => x.UserId).IsRequired();

            builder.Property(x => x.AddressId).IsRequired();

            builder.Property(x => x.TotalAmount).IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Address)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.AddressId);

            builder.HasOne(x => x.Coupon)
                .WithOne(x => x.Transaction)
                .HasForeignKey<Transaction>(x => x.CouponId);
        }
    }
}
