using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Infrastructure.Contexts;

namespace Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly BootcampContext _context;
    public TransactionRepository(BootcampContext context)
    {
        _context = context;
    }

    public Task<List<TransactionDTO>> GetFilteredTransactions(FilterTransactionModel filters)
    {
        throw new NotImplementedException();
    }
}
