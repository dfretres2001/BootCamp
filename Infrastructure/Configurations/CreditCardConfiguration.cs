using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
{
    public void Configure(EntityTypeBuilder<CreditCard> entity)
    {
        entity
            .HasKey(e => e.Id)
            .HasName("CreditCard_pkey");
        entity
            .Property(e => e.Designation)
            .HasMaxLength(100)
            .IsRequired();
        entity
            .Property(e => e.IssueDate);
        entity
            .Property(e => e.ExpirationDate);
        entity
            .Property(e => e.CardNumber)
            .HasMaxLength(20)
            .IsRequired();
        entity
            .Property(e => e.Cvv)
            .HasMaxLength(3)
            .IsRequired();
        entity
             .Property(e => e.CreditLimit)
             .HasPrecision(20, 5)
             .IsRequired();
        entity
            .Property(e => e.AvailableCredit)
            .HasPrecision(20, 5)
            .IsRequired();
        entity
           .Property(e => e.CurrentDebt)
           .HasPrecision(20, 5);
        entity
           .Property(e => e.InterestRate)
           .HasPrecision(20, 5)
           .IsRequired();
        entity
            .HasOne(cc => cc.Currency)
            .WithMany(c => c.CreditCards)
            .HasForeignKey(cc => cc.CurrencyId);
        entity
            .HasOne(cc => cc.Customer)
            .WithMany(c => c.CreditCards)
            .HasForeignKey(cc => cc.CustomerId);
    }
}
