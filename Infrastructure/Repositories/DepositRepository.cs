
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Mapster;
using Infrastructure.Contexts;

namespace Infrastructure.Repositories;

public class DepositRepository : IDepositRepository
{
    private readonly BootcampContext _context;

    public DepositRepository(BootcampContext context)
    {
        _context = context;
    }
    public async Task<DepositDTO> Add(CreateDepositModel model)
    {
        var depositToCreate = model.Adapt<Deposit>();
        _context.Deposits.Add(depositToCreate);
        // Check if the account exists
        var account = await _context.Accounts.FindAsync(model.AccountId);
        if (account == null)
        {
            throw new ArgumentException("The account does not exist.");
        }
        // Check if the bank exists (assuming you have a Bank class and a DbSet<Bank> _banks)
        var bank = await _context.Banks.FindAsync(model.BankId);
        if (bank == null)
        {
            throw new ArgumentException("The bank does not exist.");
        }
        // Check if the deposit date is not in the past
        if (model.DepositDateTime < DateTime.Now)
        {
            throw new ArgumentException("The deposit date cannot be in the past.");
        }

        // Check if the amount is not negative
        if (model.Amount < 0)
        {
            throw new ArgumentException("The deposit amount cannot be negative.");
        }

        if (account.CurrentAccount?.OperationalLimit < model.Amount)
        {
            throw new ArgumentException("The deposit amount exceeds the operational limit.");
        }
        account.Balance += model.Amount;
        await _context.SaveChangesAsync();
        var depositDTO= depositToCreate.Adapt<DepositDTO>();
        return depositDTO;
    }
}
