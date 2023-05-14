using Clicco.AuthServiceAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clicco.AuthServiceAPI.Data.EntityConfigurations
{
    public class ResetCodeEntityConfiguration : IEntityTypeConfiguration<ResetCode>
    {
        public void Configure(EntityTypeBuilder<ResetCode> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Code)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.UserId)
                .IsRequired();
        }
    }
}
