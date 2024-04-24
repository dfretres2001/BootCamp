
using Core.Models;
using Core.Request;

namespace Core.Interfaces.Services;

public interface IDepositService
{
    Task<DepositDTO> Add(CreateDepositModel model);
    Task<(bool isValid, string message)> DataValidationForDeposit(CreateDepositModel model);
}
