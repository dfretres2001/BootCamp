

using Core.Entities;
using Core.Models;
using Core.Request;

namespace Core.Interfaces.Repositories;

public interface IWithdrawalRepository
{
    Task<WithdrawalDTO> Add(CreateWithdrawalModel model);
    Task<(bool isValid, string message)> DataValidationForWithdrawal(CreateWithdrawalModel model);
}
