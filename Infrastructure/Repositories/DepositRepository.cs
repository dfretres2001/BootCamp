
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Mapster;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

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
        var (isValid, message) = await DataValidationForDeposit(model);
        if (!isValid)
        {
            throw new ArgumentException(message);
        }
        model.DepositDateTime = model.DepositDateTime.ToLocalTime();
        await CheckOperationalLimit(model.AccountId, model.Amount, model.DepositDateTime);
        var depositToCreate = model.Adapt<Deposit>();
        _context.Deposits.Add(depositToCreate);
        var account = await _context.Accounts
            .Include(m => m.CurrentAccount)
            .Include(m => m.SavingAccount)
            .Include(m => m.Customer)
            .Where(m => m.Id == model.AccountId)
            .FirstOrDefaultAsync();
        account.Balance += model.Amount;
        await _context.SaveChangesAsync();
        var depositDTO = depositToCreate.Adapt<DepositDTO>();
        return depositDTO;
    }

    public async Task<(bool isValid, string message)> DataValidationForDeposit(CreateDepositModel model)
    {
        // Check if the account exists
        var account = await _context.Accounts.FindAsync(model.AccountId);
        if (account == null)
        {
            return (false, "The account does not exist.");
        }

        // Check if the bank exists (assuming you have a Bank class and a DbSet<Bank> _banks)
        var bank = await _context.Banks.FindAsync(model.BankId);
        if (bank == null)
        {
            return (false, "The bank does not exist.");
        }
        //// Check if the deposit date is not in the past
        //if (model.DepositDateTime < DateTime.Now)
        //{
        //    return (false, "The deposit date cannot be in the past.");
        //}
        // Check if the amount is not negative
        if (model.Amount < 0)
        {
            return (false, "The deposit amount cannot be negative.");
        }
        if (account.CurrentAccount?.OperationalLimit < model.Amount)
        {
            return (false, "The deposit amount exceeds the operational limit.");
        }

        return (true, "Validations passed.");
    }

    public async Task CheckOperationalLimit(int accountId, decimal amount, DateTime transactionDate)
    {
        // Calculate the first and last date of the month for the transaction date
        var firstDateOfMonth = new DateTime(transactionDate.Year, transactionDate.Month, 1);
        var lastDateOfMonth = firstDateOfMonth.AddMonths(1).AddDays(-1);
        // Get the OperationalLimit for the associated CurrentAccount
        var account = await _context.Accounts
            .Include(m => m.CurrentAccount)
            .Where(m => m.Id == accountId)
            .FirstOrDefaultAsync();

        if (account?.CurrentAccount?.OperationalLimit.HasValue == true)
        {
            // Calculate the total deposits for the current month for the specified AccountId
            decimal currentMonthDeposits = await _context.Deposits
                .Where(d => d.AccountId == accountId && d.DepositDateTime >= firstDateOfMonth && 
                d.DepositDateTime <= lastDateOfMonth)
                .SumAsync(d => d.Amount);

            // Check if the new deposit amount + the current month's deposits exceed the OperationalLimit
            if ((currentMonthDeposits + amount) > account.CurrentAccount.OperationalLimit.Value)
            {
                throw new ArgumentException($"The deposit amount exceeds the operational limit of ${account.CurrentAccount.OperationalLimit.Value:F2} for the current month.");
            }
        }
    }

}