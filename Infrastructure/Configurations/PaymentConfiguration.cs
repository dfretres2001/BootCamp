﻿
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> entity)
    {
        entity
            .HasKey(e => e.Id)
            .HasName("Payment_pkey");
        entity
            .Property(e => e.Description)
            .HasMaxLength(300);
        entity
            .Property(e => e.Amount)
            .HasPrecision(20, 5);
        entity.HasOne(d => d.Account)
            .WithMany(p => p.Payments)
            .HasForeignKey(d => d.AccountId);
        entity.HasOne(d => d.Service)
            .WithMany(p => p.Payments)
            .HasForeignKey(d => d.ServiceId);
    }
}
