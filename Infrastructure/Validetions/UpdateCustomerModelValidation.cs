﻿using Core.Constants;
using Core.Request;
using Core.Requests;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Validations;

public class UpdateCustomerModelValidation : AbstractValidator<UpdateCustomerModel>

{
    public UpdateCustomerModelValidation()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name cannot be null")
            .NotEmpty().WithMessage("Name cannot be empty");

        RuleFor(x => x.BankId)
            .NotNull().WithMessage("BankId cannot be null")
            .NotEmpty().WithMessage("BankId cannot be empty");
        RuleFor(x => x.DocumentNumber)
            .NotNull().WithMessage("DocumentNumber cannot be null")
            .NotEmpty().WithMessage("DocumentNumber cannot be empty");
        RuleFor(x => x.Mail)
            .NotNull().WithMessage("Mail cannot be null")
            .EmailAddress();
        RuleFor(x => x.CustomerStatus)
            .Must(x => Enum.IsDefined(typeof(CustomerStatus), x))
            .WithMessage("Invalid customer status");
    }
}
