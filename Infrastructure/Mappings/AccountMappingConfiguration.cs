﻿

using Core.Entities;
using Core.Models;
using Core.Request;
using Mapster;

namespace Infrastructure.Mappings;

public class AccountMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateAccountModel, Account>()
        .Map(dest => dest.Holder, src => src.Holder)
        .Map(dest => dest.Number, src => src.Number)
        .Map(dest => dest.Type, src => src.Type)
        .Map(dest => dest.CurrencyId, src => src.CurrencyId)
        .Map(dest => dest.CustomerId, src => src.CustomerId);

        config.NewConfig<Account, AccountDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Holder, src => src.Holder)
            .Map(dest => dest.Number, src => src.Number)
            .Map(dest => dest.Type, src => src.Type)
            .Map(dest => dest.Balance, src => src.Balance)
            .Map(dest => dest.Status, src => src.Status)
            .Map(dest => dest.Currency, src => src.Currency)
            .Map(dest => dest.Customer, src => src.Customer);
    }
}