using Clicco.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clicco.Infrastructure.EntityConfigurations
{
    public class AddressEntityConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.City).HasMaxLength(50);

            builder.Property(x => x.Street).HasMaxLength(50);

            builder.Property(x => x.State).HasMaxLength(50);

            builder.Property(x => x.Country).HasMaxLength(50);

            builder.Property(x => x.ZipCode).HasMaxLength(50);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Addresses)
                .HasForeignKey(x => x.UserId);
        }
    }
}
