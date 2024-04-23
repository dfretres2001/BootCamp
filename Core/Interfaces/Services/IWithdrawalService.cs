
using Core.Models;
using Core.Request;

namespace Core.Interfaces.Services;

public interface IWithdrawalService
{
    Task<WithdrawalDTO> Add(CreateWithdrawalModel model);
}
