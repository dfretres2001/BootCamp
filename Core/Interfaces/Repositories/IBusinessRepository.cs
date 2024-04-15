

using Core.Models;
using Core.Request;

namespace Core.Interfaces.Repositories;

public interface IBusinessRepository
{
    Task<List<BusinessDTO>> GetFiltered(FilterBusinessModel filter);
    Task<BusinessDTO> GetById(int id);
    Task<BusinessDTO> Add(CreateBusinessModel model);
    Task<BusinessDTO> Update(UpdateBusinessModel model);
    Task<bool> Delete(int id);
}
