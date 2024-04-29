using Core.Constants;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
namespace Infrastructure.Repositories;

public class MovementRepository : IMovementRepository
{
    private readonly BootcampContext _context;
    public MovementRepository(BootcampContext context)
    {
        _context = context;
    }

    public async Task<MovementDTO> Add(CreateMovementModel model)
    {
        var validationResult = await ValidateTransactionRules(model);
        if (!validationResult.isValid)
        {
            throw new InvalidOperationException(validationResult.message);
        }
        var movement = model.Adapt<Movement>();
        movement.TransferredDateTime = DateTime.UtcNow;
        _context.Movements.Add(movement);
        await _context.SaveChangesAsync();
        var originalAccount = await _context.Accounts.FindAsync(model.OriginalAccountId);
        originalAccount.Balance -= model.Amount;
        var destinationAccount = await FindDestinationAccount(model);
        destinationAccount.Balance += model.Amount;
        if (originalAccount.Type == AccountType.Current)
        {
            originalAccount.CurrentAccount!.OperationalLimit -= model.Amount;
        }
        await _context.SaveChangesAsync();
        var createdMovement = await _context.Movements
            .Include(a => a.Account)
            .ThenInclude(a => a.Currency)
            .Include(a => a.Account)
            .ThenInclude(a => a.Customer)
            .ThenInclude(c => c.Bank)
            .FirstOrDefaultAsync(a => a.Id == movement.Id);
        return createdMovement.Adapt<MovementDTO>();
    }

    public async Task<(bool isValid, string message)> ValidateTransactionRules(CreateMovementModel model)
    {
        var originalAccount = await _context.Accounts
            .Include(a => a.Currency) 
            .Include(a => a.CurrentAccount)
            .Include(a => a.SavingAccount)
            .SingleOrDefaultAsync(a => a.Id == model.OriginalAccountId);

        if (originalAccount == null || originalAccount.Status != AccountStatus.Active)
        {
            return (false, "Invalid original account.");
        }
        var destinationAccount = await _context.Accounts
            .Include(a => a.Currency)
            .Include(a => a.CurrentAccount)
            .Include(a => a.SavingAccount)
            .SingleOrDefaultAsync(a => a.Id == model.DestinationAccountId);
        if (destinationAccount == null)
        {
            return (false, "Invalid destination account.");
        }

        if (originalAccount.Type != destinationAccount.Type)
        {
            return (false, "Incompatible account types.");
        }
        if (originalAccount.CurrencyId != destinationAccount.CurrencyId)
        {
            return (false, "Incompatible currencies. " +
                "The origin and destination accounts must have the same currency.");
        }
        if (originalAccount.Type == AccountType.Current)
        {
            var totalAmountOperationsOATransfers = _context.Movements
                                                                    .Where(t => t.OriginalAccountId == originalAccount.Id &&
                                                                    t.TransferredDateTime!.Value.Month 
                                                                    == model.TransferredDateTime.Month)
                                                                    .Sum(t => t.Amount);

            var totalAmountOperationsOADeposits = _context.Deposits
                                                                 .Where(d => d.AccountId == originalAccount.Id &&
                                                                 d.DepositDateTime.Month == 
                                                                 model.TransferredDateTime.Month)
                                                                 .Sum(d => d.Amount);

            var totalAmountOperationsOAExtractions = _context.Withdrawals
                                                                  .Where(e => e.AccountId == originalAccount.Id &&
                                                                  e.DepositDateTime.Month == 
                                                                  model.TransferredDateTime.Month)
                                                                  .Sum(e => e.Amount);

            var totalAmountOperationsOA = totalAmountOperationsOATransfers + totalAmountOperationsOADeposits + 
                totalAmountOperationsOAExtractions + model.Amount;

            if (totalAmountOperationsOA > originalAccount.CurrentAccount!.OperationalLimit)
            {
                throw new Exception("OriginAccount exceeded the operational limit.");
            }
            var totalAmountOperationsDATransfers = _context.Movements
                                            .Where(t => t.DestinationAccountId == destinationAccount.Id &&
                                            t.TransferredDateTime!.Value.Month == model.TransferredDateTime.Month)
                                            .Sum(t => t.Amount);
            var totalAmountOperationsDADeposits = _context.Deposits
                                                      .Where(d => d.AccountId == destinationAccount.Id &&
                                                      d.DepositDateTime.Month == model.TransferredDateTime.Month)
                                                      .Sum(d => d.Amount);

            var totalAmountOperationsDAExtractions = _context.Withdrawals
                                                      .Where(e => e.AccountId == destinationAccount.Id &&
                                                      e.DepositDateTime.Month == model.TransferredDateTime.Month)
                                                      .Sum(e => e.Amount);
            var totalAmountOperationsDA = totalAmountOperationsDATransfers + totalAmountOperationsDADeposits + 
                totalAmountOperationsDAExtractions;
            if ((model.Amount + totalAmountOperationsDA) > destinationAccount.CurrentAccount!.OperationalLimit)
            {
                throw new Exception("DestinationAccount exceeded the operational limit.");
            }
        }
        if (model.Amount <= 0)
        {
            return (false, "Invalid amount.");
        }
        if (model.TransferredDateTime == null)
        {
            return (false, "Invalid transfer date and time.");
        }
        if (originalAccount.Customer != null && originalAccount.Customer.Bank.Id
            == model.DestinationBankId)
        {
            if (string.IsNullOrEmpty(model.DestinationAccountNumber) || 
                string.IsNullOrEmpty(model.DestinationAccountNumber) || 
                model.DestinationBankId == 0)
            {
                return (false, "Document number, account number, and destiny bank ID " +
                    "are required when transferring within the same bank.");
            }
        }
        await _context.SaveChangesAsync();
        return (true, "Transaction is valid.");
    }
    public async Task<Account?> FindDestinationAccount(CreateMovementModel model)
    {
        if (model.DestinationAccountId != 0)
        {
            return await _context.Accounts.FindAsync(model.DestinationAccountId);
        }
        return await _context.Accounts
            .Include(a => a.Currency)
            .Include(a => a.CurrentAccount)
            .Include(a => a.SavingAccount)
            .Include(a => a.Customer)
            .ThenInclude(c => c.Bank)
            .SingleOrDefaultAsync(a =>
            a.Number == model.DestinationAccountNumber &&
            a.Customer.DocumentNumber == model.DestinationDocumentNumber &&
            a.CurrencyId == model.CurrencyId &&
            a.Customer.Bank.Id == model.DestinationBankId);
    }
    public async Task<MovementDTO> GetById(int id)
    {
        var movement = await _context.Movements
            .Include(a => a.Account)
            .ThenInclude(a => a.Currency)
            .Include(a => a.Account)
            .ThenInclude(a => a.CurrentAccount)
            .Include(a => a.Account)
            .ThenInclude(a => a.SavingAccount)
            .Include(a => a.Account)
            .ThenInclude(a => a.Customer)
            .ThenInclude(c => c.Bank)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (movement == null)
        {
            throw new NotFoundException($"Movement with ID {id} not found.");
        }
        return movement.Adapt<MovementDTO>();
    }
}
