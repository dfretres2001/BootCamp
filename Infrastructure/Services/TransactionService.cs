

using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Request;

namespace Infrastructure.Services;

public class TransactionService : ITransactionService
{
    private readonly IWithdrawalRepository _withdrawalRepository;

    public TransactionService(IWithdrawalRepository withdrawalRepository)
    {
        _withdrawalRepository = withdrawalRepository;
    }

    public Task<List<TransactionDTO>> GetFilteredTransactions(FilterTransactionModel filters)
    {
        throw new NotImplementedException();
    }
}
