

using Core.Models;
using Core.Request;

namespace Core.Interfaces.Repositories;

public interface IDepositRepository
{
    Task<DepositDTO> Add(CreateDepositModel model);
    Task<(bool isValid, string message)> DataValidationForDeposit(CreateDepositModel model);
    Task CheckOperationalLimit(int accountId, decimal amount, DateTime transactionDate);
}
