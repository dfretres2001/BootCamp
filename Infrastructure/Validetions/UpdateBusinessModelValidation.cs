﻿

using Core.Constants;
using Core.Request;
using FluentValidation;

namespace Infrastructure.Validetions;

public class UpdateBusinessModelValidation : AbstractValidator<UpdateBusinessModel>
{
    public UpdateBusinessModelValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Account ID cannot be empty")
            .Must(x => x > 0).WithMessage("Invalid Account ID");
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name cannot be null")
            .NotEmpty().WithMessage("Name cannot be empty")
            .MinimumLength(5).WithMessage("Name must have at least 5 characters");
        RuleFor(x => x.Email)
            .EmailAddress();
        RuleFor(x => x.Phone)
            .NotNull().WithMessage("Phone cannot be null")
            .NotEmpty().WithMessage("Phone cannot be empty");
    }
}
