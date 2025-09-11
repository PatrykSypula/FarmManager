using FarmManager.Model.Model;
using FluentValidation;

namespace FarmManager.App.Helpers.Validators;

public class VendorValidator : AbstractValidator<Vendor>
{
    public VendorValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Nazwa nie może być pusta.")
            .MaximumLength(50).WithMessage("Nazwa nie moze mieć wiecej niż 50 znaków");
        RuleFor(v => v.Description)
            .MaximumLength(100).WithMessage("Opis nie może mieć więcej niż 100 znaków");
        RuleFor(v => v.PhoneNumber)
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Numer telefonu nie jest poprawny")
            .When(v => !string.IsNullOrEmpty(v.PhoneNumber));
        RuleFor(v => v.Email)
            .EmailAddress().When(v => !string.IsNullOrEmpty(v.Email))
            .WithMessage("Nieprawidłowy format adresu email.")
            .MaximumLength(50).WithMessage("Adres email nie może przekraczać 50 znaków.");
    }
}
