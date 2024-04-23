

using Core.Models;
using Core.Request;

namespace Core.Interfaces.Repositories;

public interface IPaymentRepository
{
    Task<PaymentDTO> Add(CreatePaymentModel model);
}
