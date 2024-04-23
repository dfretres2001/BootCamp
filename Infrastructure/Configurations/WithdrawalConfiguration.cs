

using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class WithdrawalConfiguration : IEntityTypeConfiguration<Withdrawal>
{
    public void Configure(EntityTypeBuilder<Withdrawal> entity)
    {
        entity
           .HasKey(e => e.Id)
           .HasName("Withdrawal_pkey");

        entity
         .Property(e => e.Amount)
         .HasPrecision(20, 5);

        entity.HasOne(d => d.Account)
           .WithMany(p => p.Withdrawals)
           .HasForeignKey(d => d.AccountId);
    }
}
