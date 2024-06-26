﻿
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CreditCardRepository : ICreditCardRepository
{
    private readonly BootcampContext _context;

    public CreditCardRepository(BootcampContext context)
    {
        _context = context;
    }
    public async Task<CreditCardDTO> Add(CreateCreditCardModel model)
    {
        var customer = await _context.Customers
                .Include(c => c.Bank)
                .FirstOrDefaultAsync(c => c.Id == model.CustomerId);
        if (customer == null)
        {
            throw new BusinessLogicException($"The customer with id '{model.CustomerId}' does not exist.");
        }
        var currency = await _context.Currencies.FindAsync(model.CurrencyId);
        if (currency == null)
        {
            throw new BusinessLogicException($"The Currency with id '{model.CurrencyId}' does not exist.");
        }
        var creditCardToCreate = model.Adapt<CreditCard>();
        _context.CreditCards.Add(creditCardToCreate);
        await _context.SaveChangesAsync();
        var creditCardDTO = creditCardToCreate.Adapt<CreditCardDTO>();
        return creditCardDTO;
    }
    public async Task<CreditCardDTO> Update(UpdateCreditCardModel model)
    {
        var customer = await _context.Customers
                .Include(c => c.Bank)
                .FirstOrDefaultAsync(c => c.Id == model.CustomerId);
        if (customer == null)
        {
            throw new BusinessLogicException($"The customer with id '{model.CustomerId}' does not exist.");
        }
        var currency = await _context.Currencies.FindAsync(model.CurrencyId);
        if (currency == null)
        {
            throw new BusinessLogicException($"The Currency with id '{model.CurrencyId}' does not exist.");
        }
        var creditCard = await _context.CreditCards.FindAsync(model.Id);

        if (creditCard is null) throw new Exception("CreditCard was not found");
        model.Adapt(creditCard);
        _context.CreditCards.Update(creditCard);
        await _context.SaveChangesAsync();
        var creditCardDTO = creditCard.Adapt<CreditCardDTO>();
        return creditCardDTO;
    }
    public async Task<bool> Delete(int id)
    {
        var creditCard = await _context.CreditCards.FindAsync(id);
        if (creditCard is null) throw new Exception("CreditCard not found");
        _context.CreditCards.Remove(creditCard);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<CreditCardDTO> GetById(int id)
    {
        var query = _context.CreditCards
            .Include(c => c.Customer)
            .ThenInclude(customer => customer.Bank)
            .Include(c => c.Currency)
            .AsQueryable();
        var creditCard = await _context.CreditCards.FindAsync(id);
        if (creditCard is null) throw new NotFoundException($"Credit Card with id: {id} doest not exist");
        var result = await query.ToListAsync();
        var creditCardDTO = creditCard.Adapt<CreditCardDTO>();
        return creditCardDTO;
    }
    public async Task<List<CreditCardDTO>> GetFiltered(FilterCreditCardModel filter)
    {
        var query = _context.CreditCards
                   .Include(c=> c.Customer)
                   .ThenInclude(Customer => Customer.Bank)
                   .Include(c => c.Currency)
                   .AsQueryable();
        if (!string.IsNullOrWhiteSpace(filter.Designation))
        {
            var designationUpper = filter.Designation.ToUpper();
            query = query.Where(x =>
            x.Designation != null &&
            x.Designation.ToUpper() == designationUpper);
        }
        if (filter.CustomerId is not null) 
        {
            query = query.Where(x =>
            x.Customer != null &&
            x.Customer.Id == filter.CustomerId);
        }
        var result = await query.ToListAsync();
        var creditCardDTO = result.Adapt<List<CreditCardDTO>>();
        return creditCardDTO;
    }
}
