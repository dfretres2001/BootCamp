using Core.Models;
using Core.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services;

public interface IMovementService
{
    Task<MovementDTO> GetById(int id);
    Task<MovementDTO> Add(CreateMovementModel model);
    Task<(bool isValid, string message)> ValidateTransactionRules(CreateMovementModel model);
    //Task CheckOperationalLimit(int accountId, decimal amount, DateTime transactionDate);
}
