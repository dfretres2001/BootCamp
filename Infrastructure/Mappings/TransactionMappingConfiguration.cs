using Core.Entities;
using Core.Models;
using Core.Request;
using Mapster;
using System.Reflection;

namespace Infrastructure.Mappings;

public class TransactionMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Movement, TransactionDTO>()
        .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Type, src => "Movement")
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.TransferredDateTime, src => src.TransferredDateTime)
            .Map(dest => dest.AccountId, src => src.OriginalAccountId) //poner que sea el origen account
            .Map(dest => dest.AccountId, src => src.DestinationAccountId); //que se sea destination
            ;

        config.NewConfig<Payment, TransactionDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Type, src => "Payment")
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.AccountId, src => src.AccountId);

        config.NewConfig<Deposit, TransactionDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Type, src => "Deposit")
            .Map(dest => dest.Description, src => "Deposit")
            .Map(dest => dest.Amount, src => src.Amount)
            //.Map(dest => dest.TransferredDateTime, src => src.DepositDateTime) OJO
            .Map(dest => dest.AccountId, src => src.AccountId);

        config.NewConfig<Withdrawal, TransactionDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Type, src => "Withdrawal")
            .Map(dest => dest.Description, src => "Withdrawal")
            .Map(dest => dest.Amount, src => src.Amount)
            //.Map(dest => dest.TransferredDateTime, src => src.WithdrawalDateTime) ojo
            .Map(dest => dest.AccountId, src => src.AccountId);


    }
}
