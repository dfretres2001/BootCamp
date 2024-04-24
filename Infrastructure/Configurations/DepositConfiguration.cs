﻿

using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configurations;

public class DepositConfiguration : IEntityTypeConfiguration<Deposit>
{
    public void Configure(EntityTypeBuilder<Deposit> entity)
    {
        entity
           .HasKey(e => e.Id)
           .HasName("Deposit_pkey");

        entity
         .Property(e => e.Amount)
         .HasPrecision(20, 5);

        entity.HasOne(d => d.Account)
           .WithMany(p => p.Deposits)
           .HasForeignKey(d => d.AccountId);

        entity
            .Property(e => e.DepositDateTime)
            .HasColumnType("timestamp")
            .IsRequired();
    }
}