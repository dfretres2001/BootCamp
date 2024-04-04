﻿using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> entity)
    {
        entity
            .HasKey(e => e.Id)
            .HasName("Customer_pkey");
        entity
            .Property(e => e.Lastname)
            .HasMaxLength(300);
        entity
            .Property(e => e.Phone)
            .HasMaxLength(150);
        entity
            .Property(e => e.Address)
            .HasMaxLength(400);
        entity
            .Property(e => e.DocumentNumber)
            .HasMaxLength(150)
            .IsRequired();
        entity
            .Property(e => e.Mail)
            .HasMaxLength(100);
        entity
            .Property(e => e.Name)
            .HasMaxLength(300)
            .IsRequired();
        entity
            .HasOne(d => d.Bank)
            .WithMany(p => p.Customers)
            .HasForeignKey(d => d.BankId);
    }
}