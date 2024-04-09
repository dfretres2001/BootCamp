

using Core.Request;
using FluentValidation;

namespace Infrastructure.Validetions;

public class CreateCurrencyModelValidation : AbstractValidator<CreateCurrencyModel>
{
    public CreateCurrencyModelValidation()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name cannot be null")
            .NotEmpty().WithMessage("Name cannot be empty")
            .MinimumLength(5).WithMessage("Name must have at least 5 characters");

        RuleFor(x => x.BuyValue)
            .NotNull().WithMessage("Name cannot be null")
            .NotEmpty().WithMessage("Name cannot be empty");

        RuleFor(x => x.SellValue)
            .NotNull().WithMessage("Phone cannot be null")
            .NotEmpty().WithMessage("Phone cannot be empty");
    }
}
