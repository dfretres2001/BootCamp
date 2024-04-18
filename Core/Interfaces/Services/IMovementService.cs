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
}
