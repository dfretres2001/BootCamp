

namespace Core.Interfaces.Services;

public interface IOperationalLimitService
{
    Task<(bool isValid, string message)> IsValidDeposit(int accountId, decimal amount);
}
