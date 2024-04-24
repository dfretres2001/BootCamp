
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Request;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class WithdrawalService : IWithdrawalService
{
    private readonly IWithdrawalRepository _withdrawalRepository;

    //CTOR
    public WithdrawalService(IWithdrawalRepository withdrawalRepository)
    {
        _withdrawalRepository = withdrawalRepository;
    }

    public async Task<WithdrawalDTO> Add(CreateWithdrawalModel model)
    {
        return await _withdrawalRepository.Add(model);
    }

    public async Task<(bool isValid, string message)> DataValidationForWithdrawal(CreateWithdrawalModel model)
    {
        return await _withdrawalRepository.DataValidationForWithdrawal(model);
    }
}
