using Core.Models;
using Core.Request;

namespace Core.Interfaces.Repositories;

public interface IBankRepository
{
    Task<BankDTO> Add(CreateBankModel model); //cambio aqui recien
    Task<BankDTO> GetById(int id);
    Task<BankDTO> Update(Request.UpdateBankModel model);//cambio aqui recien
    Task<bool> Delete(int id);
    Task<List<BankDTO>> GetAll();
    Task<bool> NameIsAlreadyTaken(string name);
}
