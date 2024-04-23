using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Infrastructure.Contexts;

namespace Infrastructure.Repositories;

public class WithdrawalRepository : IWithdrawalRepository
{
    private readonly BootcampContext _context;

    public WithdrawalRepository(BootcampContext context)
    {
        _context = context;
    }

    public async Task<WithdrawalDTO> Add(CreateWithdrawalModel model)
    {
        throw new NotImplementedException();
    }
}
