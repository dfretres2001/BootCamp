

using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace Infrastructure.Repositories;

public class RequestRepository : IRequestRepository
{
    private readonly BootcampContext _context;

    public RequestRepository(BootcampContext context)
    {
        _context = context;
    }

    public async Task<RequestDTO> Add(CreateRequestModel model)
    {
        //throw new NotImplementedException();
        var request = model.Adapt<Request>();
        _context.Requests.Add(request);
        await _context.SaveChangesAsync();
        var requestDTO = request.Adapt<RequestDTO>();
        return requestDTO;
    }

    public async Task<RequestDTO> GetById(int id)
    {
        // Retrieve the Request entity from the database based on the id parameter
        var request = await _context.Requests
           .Include(r => r.Currency)
           .Include(r => r.Customer)
           .SingleOrDefaultAsync(r => r.Id == id);

        // Return the Request entity as a RequestDTO
        if (request != null)
        {
            return new RequestDTO();
        }
        else
        {
            return null;
        }
    }
}
