

using Core.Models;
using Core.Request;

namespace Core.Interfaces.Services;

public interface IAccountService
{
    Task<List<AccountDTO>> GetFiltered(FilterAccountModel filter);
    Task<AccountDTO> GetById(int id);
    Task<AccountDTO> Add(CreateAccountRequest model);
    Task<AccountDTO> Update(UpdateAccountModel model);
    Task<bool> Delete(int id);
}
