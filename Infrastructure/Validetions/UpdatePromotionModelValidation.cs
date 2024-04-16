
using Core.Request;
using FluentValidation;

namespace Infrastructure.Validetions;

public class UpdatePromotionModelValidation : AbstractValidator<UpdatePromotionModel>
{
    public UpdatePromotionModelValidation()
    {
        RuleFor(x => x.Id)
             .NotEmpty().WithMessage("Account ID cannot be empty")
             .Must(x => x > 0).WithMessage("Invalid Account ID");
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name cannot be null")
            .NotEmpty().WithMessage("Name cannot be empty")
            .MinimumLength(5).WithMessage("Name must have at least 5 characters");
    }
}
