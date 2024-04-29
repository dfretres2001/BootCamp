using Core.Entities;
using Core.Models;
using Core.Request;
using Mapster;
using System.Reflection;
using System.Security.Cryptography.Xml;

namespace Infrastructure.Mappings;

public class TransactionMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //Del Creation object hacia la entidad
        config.NewConfig<CreateTransferModel, Movement>()
            .Map(dest => dest.OriginalAccountId, src => src.OriginAccountId)
            .Map(dest => dest.Account.CurrencyId, src => src.CurrencyId)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.TransferredDateTime, src => DateTime.Now)
            .Map(dest => dest.Description, src => src.Concept);

        config.NewConfig<Movement, TransactionDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Description, src => "Transfer")
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.TransferredDateTime, src => src.TransferredDateTime)
            .Map(dest => dest.AccountId, src => src.OriginalAccountId)
            .Map(dest => dest.AccountId, src => src.DestinationAccountId);

        config.NewConfig<Payment, TransactionDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Description, src => "Payment")
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.AccountId, src => src.AccountId);

        config.NewConfig<Deposit, TransactionDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Description, src => "Deposit")
            .Map(dest => dest.Amount, src => src.Amount)
            //.Map(dest => dest.TransferredDateTime, src => src.DepositDateTime) OJO
            .Map(dest => dest.AccountId, src => src.AccountId);

        config.NewConfig<Withdrawal, TransactionDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Description, src => "Withdrawal")
            .Map(dest => dest.Amount, src => src.Amount)
            //.Map(dest => dest.TransferredDateTime, src => src.WithdrawalDateTime) ojo
            .Map(dest => dest.AccountId, src => src.AccountId);


    }
}
