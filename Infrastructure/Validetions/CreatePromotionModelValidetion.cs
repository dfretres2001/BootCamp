﻿using Core.Request;
using Core.Requests;
using FluentValidation;

namespace Infrastructure.Validations;

public class CreatePromotionModelValidation : AbstractValidator<CreatePromotionModel>
{
    public CreatePromotionModelValidation()
    {
        
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name cannot be null")
            .NotEmpty().WithMessage("Name cannot be empty")
            .MinimumLength(5).WithMessage("Name must have at least 5 characters");
    }
}