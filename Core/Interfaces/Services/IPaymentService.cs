

using Core.Models;
using Core.Request;

namespace Core.Interfaces.Services;

public interface IPaymentService
{
    Task<PaymentDTO> Add(CreatePaymentModel model);
}
