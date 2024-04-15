
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Infrastructure.Contexts;
using Mapster;

namespace Infrastructure.Repositories;

public class BusinessRepository : IBusinessRepository
{
    private readonly BootcampContext _context;
    public BusinessRepository(BootcampContext context)
    {
        _context = context;
    }

    public async Task<BusinessDTO> Add(CreateBusinessModel model)
    {
        var businessToCreate = model.Adapt<Business>();
        _context.Businesses.Add(businessToCreate);

        await _context.SaveChangesAsync();
        var businessDTO = businessToCreate.Adapt<BusinessDTO>();
        return businessDTO;
    }

    public async Task<bool> Delete(int id)
    {
        var business = await _context.Businesses.FindAsync(id);

        if (business is null) throw new Exception("Business not found");

        _context.Businesses.Remove(business);

        var result = await _context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<BusinessDTO> GetById(int id)
    {
        var business = await _context.Businesses.FindAsync(id);

        if (business is null) throw new NotFoundException($"Business with id: {id} doest not exist");

        var businessDTO = business.Adapt<BusinessDTO>();

        return businessDTO;
    }

    public async Task<List<BusinessDTO>> GetFiltered(FilterBusinessModel filter)
    {
        throw new NotImplementedException();
    }

    public async Task<BusinessDTO> Update(UpdateBusinessModel model)
    {
        var business = await _context.Businesses.FindAsync(model.Id);

        if (business is null) throw new Exception("Business was not found");

        model.Adapt(business);

        _context.Businesses.Update(business);

        await _context.SaveChangesAsync();

        var businessDTO = business.Adapt<BusinessDTO>();
        return businessDTO;
    }
}
