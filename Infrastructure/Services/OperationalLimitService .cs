
using Core.Interfaces.Services;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class OperationalLimitService : IOperationalLimitService
{
    private readonly BootcampContext _context;

    public OperationalLimitService(BootcampContext context)
    {
        _context = context;
    }
    public async Task<(bool isValid, string message)> IsValidDeposit(int accountId, decimal amount)
    {
        var account = await _context.Accounts
            .Include(m => m.CurrentAccount)
            .Where(m => m.Id == accountId)
            .FirstOrDefaultAsync();
        if (account == null || account.CurrentAccount == null)
        {
            return (false, "La cuenta no existe o no tiene un límite operacional asignado.");
        }
        var currentMonth = DateTime.Now.Month;
        var currentYear = DateTime.Now.Year;
        var depositsThisMonth = await _context.Deposits
            .Where(m => m.AccountId == accountId && m.DepositDateTime.Month == currentMonth && 
            m.DepositDateTime.Year == currentYear)
            .SumAsync(m => m.Amount);
        if (depositsThisMonth + amount > account.CurrentAccount.OperationalLimit)
        {
            return (false, $"El depósito supera el límite operacional mensual de " +
                $"{account.CurrentAccount.OperationalLimit}.");
        }
        return (true, string.Empty);
    }
}