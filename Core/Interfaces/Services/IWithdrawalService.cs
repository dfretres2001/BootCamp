
using Core.Models;
using Core.Request;

namespace Core.Interfaces.Services;

public interface IWithdrawalService
{
    Task<WithdrawalDTO> Add(CreateWithdrawalModel model);
    Task<(bool isValid, string message)> DataValidationForWithdrawal(CreateWithdrawalModel model);
}
