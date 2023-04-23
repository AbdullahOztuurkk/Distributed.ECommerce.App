using Clicco.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clicco.Persistence.EntityConfigurations
{
    public class TransactionDetailProductEntityConfiguration : IEntityTypeConfiguration<TransactionDetailProduct>
    {
        public void Configure(EntityTypeBuilder<TransactionDetailProduct> builder)
        {
            builder.HasKey(o => new { o.ProductId, o.TransactionDetailId });

            builder.HasOne(x => x.Product)
                .WithMany(x => x.TransactionDetailProducts)
                .HasForeignKey(x => x.ProductId);

            builder.HasOne(x => x.TransactionDetail)
                .WithMany(x => x.TransactionDetailProducts)
                .HasForeignKey(x => x.TransactionDetailId);
        }
    }
}
