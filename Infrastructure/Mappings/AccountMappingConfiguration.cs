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
        config.NewConfig<CreateAccountRequest, Account>()
        .Map(dest => dest.Holder, src => src.Holder)
        .Map(dest => dest.Number, src => src.Number)
        .Map(dest => dest.Type, src => src.AccountType)
        .Map(dest => dest.CurrencyId, src => src.CurrencyId)
        .Map(dest => dest.CustomerId, src => src.CustomerId);

        config.NewConfig<CreateSavingAccount, SavingAccount>()
        .Map(dest => dest.SavingType, src => src.SavingType);

        config.NewConfig<CreateCurrentAccount, CurrentAccount>()
        .Map(dest => dest.OperationalLimit, src => src.OperationalLimit)
        .Map(dest => dest.MonthAverage, src => src.MonthAverage)
        .Map(dest => dest.Interest, src => src.Interest);

        config.NewConfig<Account, AccountDTO>()
        .Map(dest => dest.Id, src => src.Id)
        .Map(dest => dest.Holder, src => src.Holder)
        .Map(dest => dest.Number, src => src.Number)
        .Map(dest => dest.Type, src => src.Type)
        .Map(dest => dest.Balance, src => src.Balance)
        .Map(dest => dest.Status, src => src.Status.ToString())
        .Map(dest => dest.Currency, src => src.Currency)
        .Map(dest => dest.Customer, src => src.Customer)
           .Map(dest => dest.SavingAccount, src =>
                src.SavingAccount != null
                ? src.SavingAccount
                : null)
            .Map(dest => dest.CurrentAccount, src =>
                src.CurrentAccount != null
                ? src.CurrentAccount
                : null);
        config.NewConfig<SavingAccount, SavingAccountDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.SavingType, src => src.SavingType.ToString());

        config.NewConfig<CurrentAccount, CurrentAccountDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.OperationalLimit, src => src.OperationalLimit)
            .Map(dest => dest.MonthAverage, src => src.MonthAverage)
            .Map(dest => dest.Interest, src => src.Interest);
    }
}