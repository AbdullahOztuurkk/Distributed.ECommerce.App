using Clicco.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clicco.Infrastructure.EntityConfigurations
{
    public class MenuEntityConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.SlugUrl)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasOne(x => x.Category)
                .WithOne(x => x.Menu)
                .HasForeignKey<Category>(x => x.MenuId);
        }
    }
}
