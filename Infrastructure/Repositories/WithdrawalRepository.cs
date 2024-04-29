using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class WithdrawalRepository : IWithdrawalRepository
{
    private readonly BootcampContext _context;
    public WithdrawalRepository(BootcampContext context)
    {
        _context = context;
    }

    public async Task<WithdrawalDTO> Add(CreateWithdrawalModel model)
    {
        var (isValid, message) = await DataValidationForWithdrawal(model);
        if (!isValid)
        {
            throw new ArgumentException(message);
        }
        var withdrawalToCreate = model.Adapt<Withdrawal>();
        _context.Withdrawals.Add(withdrawalToCreate);
        var account = await _context.Accounts
            .Include(m => m.CurrentAccount)
            .Include(m => m.SavingAccount)
            .Include(m => m.Customer)
            .Where(m => m.Id == model.AccountId)
            .FirstOrDefaultAsync();
        decimal newBalance = account.Balance - model.Amount;
        if (newBalance < 0)
        {
            throw new ArgumentException("The withdrawal amount exceeds the account balance.");
        }
        account.Balance = newBalance;
        await _context.SaveChangesAsync();
        var withdrawalDTO = withdrawalToCreate.Adapt<WithdrawalDTO>();
        return withdrawalDTO;
    }
    public async Task<(bool isValid, string message)> DataValidationForWithdrawal(CreateWithdrawalModel model)
    {
        var account = await _context.Accounts
            .Include(m => m.CurrentAccount)
            .Include(m => m.SavingAccount)
            .Include(m => m.Customer)
            .Where(m => m.Id == model.AccountId)
            .FirstOrDefaultAsync();
        if (model.Amount < 0)
        {
            throw new ArgumentException("The deposit amount cannot be negative.");
        }
        if (model.DepositDateTime < DateTime.Now)
        {
            throw new ArgumentException("The deposit date cannot be in the past.");
        }
        if (account == null)
        {
            return (false, "The account does not exist.");
        }
        if (account.CurrentAccount == null || model.Amount > account.CurrentAccount.OperationalLimit)
        {
            return (false, "The withdrawal amount exceeds the operational limit.");
        }
        if (account.Customer.BankId != model.BankId)
        {
            return (false, "The destination bank does not match the entered bank.");
        }
        return (true, "Validations passed.");
    }
}
