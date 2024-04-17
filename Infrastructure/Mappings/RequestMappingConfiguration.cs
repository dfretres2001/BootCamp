

using Core.Entities;
using Core.Models;
using Core.Request;
using Mapster;

namespace Infrastructure.Mappings;

public class RequestMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateRequestModel, Request>()
            .Map(m => m.Amount, r => r.Amount)
            .Map(m => m.Term, r => r.Term)
            .Map(m => m.Brand, r => r.Brand)
            .Map(m => m.CurrencyId, r => r.CurrencyId)
            .Map(m => m.ProductType, r => r.ProductType)
            .Map(m => m.CustomerId, r => r.CustomerId);
    }
}