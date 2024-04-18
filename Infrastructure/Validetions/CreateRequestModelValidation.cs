

using Core.Entities;
using Core.Request;
using FluentValidation;

namespace Infrastructure.Validetions;

public class CreateRequestModelValidation : AbstractValidator<CreateRequestModel>
{
    public CreateRequestModelValidation()
    {
        //RuleFor(x => x.Amount)
        //    .GreaterThan(0).WithMessage("Amount must be greater than zero");

        //RuleFor(x => x.Term)
        //    .GreaterThan(0).WithMessage("Term must be greater than zero")
        //   .When(x => x.ProductId == 1);

        //RuleFor(x => x.Brand)
        //    .NotNull().WithMessage("Brand cannot be null")
        //    .NotEmpty().WithMessage("Brand cannot be empty")
        //    .When(x => x.ProductId == 2)
        //    .MaximumLength(50).WithMessage("Brand cannot be longer than 50 characters");

        RuleFor(x => x.ProductId)
            .NotEqual(default(int)).WithMessage("Product ID cannot be default");

        RuleFor(x => x.CurrencyId)
            .NotEqual(default(int)).WithMessage("Currency ID cannot be default");

        RuleFor(x => x.CustomerId)
            .NotEqual(default(int)).WithMessage("Customer ID cannot be default");
    }
}
