

using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly BootcampContext _context;
    public PaymentRepository(BootcampContext context)
    {
        _context = context;
    }
    public async Task<PaymentDTO> Add(CreatePaymentModel model)
    {
        var account = await _context.Accounts
            .Include(a => a.Customer)
            .FirstOrDefaultAsync(a => a.Id == model.AccountId);

        if (account == null)
        {
            throw new Exception("Account not found.");
        }
        var service = await _context.Services.FindAsync(model.ServiceId);
        if (service == null)
        {
            throw new Exception("Service not found.");
        }
        account.Balance -= model.Amount;
        var payment = new Payment
        {
            DocumentNumber = model.DocumentNumber,
            Description = model.Description,
            Amount = model.Amount,
            ServiceId = model.ServiceId,
            AccountId = model.AccountId,
            Account = account
        };
        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();
        var paymentDTO = payment.Adapt<PaymentDTO>();
        return paymentDTO;
    }
}
