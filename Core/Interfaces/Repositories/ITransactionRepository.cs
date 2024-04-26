
using Core.Models;
using Core.Request;

namespace Core.Interfaces.Repositories;

public interface ITransactionRepository
{
    Task<List<TransactionDTO>> GetFiltered(FilterTransactionModel filter);
}
