﻿using Clicco.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clicco.Infrastructure.EntityConfigurations
{
    public class CouponEntityConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Type)
                .IsRequired();

            builder.Property(x => x.TypeId)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.DiscountType)
                .IsRequired();

            builder.Property(x => x.DiscountAmount)
                .IsRequired();

            builder.Property(x => x.ExpirationDate)
                .IsRequired();

            builder.HasOne(x => x.Transaction)
                .WithOne(x => x.Coupon)
                .HasForeignKey<Transaction>(x => x.CouponId);
        }
    }
}