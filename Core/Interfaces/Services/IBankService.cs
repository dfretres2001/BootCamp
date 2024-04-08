﻿using Core.Models;
using Core.Request;
using System.Threading.Tasks;

namespace Core.Interfaces.Services;

public interface IBankService
{
    Task<BankDTO> Add(Request.CreateBankModel model);
    Task<BankDTO> GetById(int id);
    Task<BankDTO> Update(Request.UpdateBankModel model);
    Task<bool> Delete(int id);
    Task<List<BankDTO>> GetAll();
}
