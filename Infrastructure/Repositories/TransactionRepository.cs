using Core.Constants;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Infrastructure.Contexts;
using System.Net.Mime;

namespace Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly BootcampContext _context;
    public TransactionRepository(BootcampContext context)
    {
        _context = context;
    }

    public async Task<List<TransactionDTO>> GetFiltered(FilterTransactionModel filter)
    {
        var transfers = _context.Movements
                        .Where(t => filter.ConceptType == TransactionType.All ||
                        filter.ConceptType == TransactionType.Transfer)
                        .ToList();

        var paymentServices = _context.Payments
                                      .Where(ps => filter.ConceptType == TransactionType.All ||
                                      filter.ConceptType == TransactionType.Payment)
                                      .ToList();

        var deposits = _context.Deposits
                               .Where(d => filter.ConceptType == TransactionType.All ||
                               filter.ConceptType == TransactionType.Deposit)
                               .ToList();

        var extractions = _context.Withdrawals
                                  .Where(e => filter.ConceptType == TransactionType.All ||
                                  filter.ConceptType == TransactionType.Withdrawal)
                                  .ToList();

        List<TransactionDTO> transactions = new List<TransactionDTO>();

        transfers.ForEach(t => {
            transactions.Add(new TransactionDTO()
            {
                Id = t.Id,
                Description = "Transfer",
                Amount = t.Amount,
                TransferredDateTime = (DateTime)t.TransferredDateTime!,
                Concept = t.Description,
                CurrencyId = null,
                AccountId = t.OriginalAccountId
            });
        });

        paymentServices.ForEach(ps => {
            transactions.Add(new TransactionDTO()
            {
                Id = ps.Id,
                Description = "Payment",
                Amount = ps.Amount,
                Concept = ps.Description,
                CurrencyId = null,
                AccountId = ps.AccountId
            });
        });

        deposits.ForEach(d => {
            transactions.Add(new TransactionDTO()
            {
                Id = d.Id,
                Description = "Deposit",
                Amount = d.Amount,
                TransferredDateTime = d.DepositDateTime,
                Concept = string.Empty,
                CurrencyId = null,
                AccountId = d.AccountId
            });
        });

        extractions.ForEach(e => {
            transactions.Add(new TransactionDTO()
            {
                Id = e.Id,
                Description = "Withdrawal",
                Amount = e.Amount,
                TransferredDateTime = e.DepositDateTime,
                Concept = string.Empty,
                CurrencyId = null,
                AccountId = e.AccountId
            });
        });

        var result = transactions
                        .Where(t => t.AccountId == filter.AccountId &&
                        (filter.Month == null && filter.Year == null ||
                        t.TransferredDateTime.Month == filter.Month && t.TransferredDateTime.Year == filter.Year) &&
                        (filter.FromDate == null && filter.ToDate == null ||
                        t.TransferredDateTime >= filter.FromDate && t.TransferredDateTime <= filter.ToDate))
                        .ToList();

        return result;
    }
}
