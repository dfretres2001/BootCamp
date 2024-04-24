using Core.Request;
using FluentValidation;

namespace Infrastructure.Validetions;

public class CreateDepositModelValidation : AbstractValidator<CreateDepositModel>
{
    public CreateDepositModelValidation()
    {
        RuleFor(x => x.AccountId)
         .NotNull().WithMessage("AccountId cannot be null")
         .NotEmpty().WithMessage("AccountId cannot be empty");

        RuleFor(x => x.BankId)
          .NotNull().WithMessage("BankId cannot be null")
          .NotEmpty().WithMessage("BankId cannot be empty");

        RuleFor(x => x.Amount)
         .NotNull().WithMessage("Amount cannot be null")
         .NotEmpty().WithMessage("Amount cannot be empty")
         .GreaterThanOrEqualTo(0)
         .WithMessage("The deposit amount cannot be negative.");

        RuleFor(x => x.DepositDateTime)
         .NotNull().WithMessage("DepositDateTime cannot be null")
         .NotEmpty().WithMessage("DepositDateTime cannot be empty")
         .GreaterThanOrEqualTo(DateTime.Now)
         .WithMessage("The deposit date cannot be in the past.");
    }
}
