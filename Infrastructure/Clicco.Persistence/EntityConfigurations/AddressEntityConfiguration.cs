using Clicco.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clicco.Persistence.EntityConfigurations
{
    public class AddressEntityConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.City).HasMaxLength(50);

            builder.Property(x => x.Street).HasMaxLength(50);

            builder.Property(x => x.State).HasMaxLength(50);

            builder.Property(x => x.Country).HasMaxLength(50);

            builder.Property(x => x.ZipCode).HasMaxLength(50);

        }
    }
}
