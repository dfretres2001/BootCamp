using Core.Models;
using Core.Request;

namespace Core.Interfaces.Services;

public interface ITransactionService
{
    Task<List<TransactionDTO>> GetFilteredTransactions(FilterTransactionModel filters);
}
