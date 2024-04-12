
using Core.Constants;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly BootcampContext _context;
    public AccountRepository(BootcampContext context)
    {
        _context = context;
    }

    public async Task<AccountDTO> Add(CreateAccountModel model)
    {
        var account = model.Adapt<Account>();
        var customer = await _context.Customers.FindAsync(model.CustomerId);
        var customer2 = await _context.Customers
               .Include(c => c.Bank)
               .FirstOrDefaultAsync(c => c.Id == model.CustomerId);

        if (customer != null)
            {
                account.Customer = customer;
            }
            var currency = await _context.Currencies.FindAsync(model.CurrencyId);
            if (currency != null)
            {
                account.Currency = currency;
            }
        account.Status = AccountStatus.Active;
        account.IsDeleted = IsDeletedStatus.False;

        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();
        var accountDTO = account.Adapt<AccountDTO>();
        return accountDTO;
    }
    public async Task<AccountDTO> Update(UpdateAccountModel model)
    {
        var account = await _context.Accounts.FindAsync(model.Id);

        if (account is null) throw new Exception("Account was not found");
        model.Adapt(account);
        _context.Accounts.Update(account);
        await _context.SaveChangesAsync();
        var creditCardDTO = account.Adapt<AccountDTO>();
        return creditCardDTO;
    }
    public async Task<bool> Delete(int id)
    {
        var account = await _context.Accounts.FindAsync(id);

        if (account is null) throw new NotFoundException("Account with ID " + id + " was not found");

        account.IsDeleted = IsDeletedStatus.True;
        //account.Status = AccountStatus.Inactive;

        _context.Accounts.Update(account);
        await _context.SaveChangesAsync();
        var result = await _context.SaveChangesAsync();
        return result>0;
    }

    public async Task<List<AccountDTO>> GetFiltered(FilterAccountModel filter)
    {
        var query = _context.Accounts
                   .Include(a => a.Currency)
                   .Include(a => a.Customer)
                   .ThenInclude(c => c.Bank)
                   .Where(a => a.IsDeleted != IsDeletedStatus.True)
                   .AsQueryable(); 

        //var accounts = await _context.Accounts.Where(a => a.IsDeleted != IsDeletedStatus.False).ToListAsync();

        if (!string.IsNullOrWhiteSpace(filter.Number))
        {
            query = query.Where(a => a.Number == filter.Number);
        }

        if (!string.IsNullOrWhiteSpace(filter.Number))
        {
            query = query.Where(a => a.Number == filter.Number);
        }

        if (filter.Type is not null)
        {
            query = query.Where(a => a.Type == filter.Type);
        }

        if (!string.IsNullOrWhiteSpace(filter.Currency))
        {
            query = query.Where(a => a.Currency.Name == filter.Currency);
        }

        var result = await query.ToListAsync();
        var accountDTO = result.Adapt<List<AccountDTO>>();
        return accountDTO;
    }

}
