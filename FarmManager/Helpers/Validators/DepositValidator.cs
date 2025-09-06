using FarmManager.Model.Model;
using FluentValidation;

namespace FarmManager.Services.Validators;

public class DepositValidator : AbstractValidator<Deposit>
{
    public DepositValidator()
    {
        RuleFor(d => d.Name)
            .NotEmpty().WithMessage("Nazwa nie może być pusta.")
            .MaximumLength(50).WithMessage("Nazwa nie moze mieć wiecej niż 50 znaków");
        RuleFor(d => d.Description)
            .MaximumLength(100).WithMessage("Opis nie może mieć więcej niż 100 znaków");
        RuleFor(d => d.PhoneNumber)
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Numer telefonu nie jest poprawny")
            .When(d => !string.IsNullOrEmpty(d.PhoneNumber));
        RuleFor(d => d.Email)
            .EmailAddress().WithMessage("Adres email nie jest poprawny")
            .When(d => !string.IsNullOrEmpty(d.Email));
    }
}
