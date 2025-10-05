using FarmManager.Model.Model;
using FluentValidation;

namespace FarmManager.App.Helpers.Validators;

public class EmployeeValidator : AbstractValidator<Employee>
{
    public EmployeeValidator()
    {
        RuleFor(e => e.FirstName)
            .NotEmpty().WithMessage("Imię jest wymagane.")
            .MaximumLength(50).WithMessage("Imię nie może przekraczać 50 znaków.");
        RuleFor(e => e.LastName)
            .NotEmpty().WithMessage("Nazwisko jest wymagane.")
            .MaximumLength(50).WithMessage("Nazwisko nie może przekraczać 50 znaków.");
        RuleFor(e => e.IdNumber)
            .MaximumLength(20).WithMessage("Numer identyfikacyjny nie może przekraczać 20 znaków.");
        RuleFor(e => e.BaseRent)
            .GreaterThanOrEqualTo(0).When(e => e.IsRentable)
            .WithMessage("Podstawowa stawka wynajmu musi być większa lub równa zero, jeśli pracownik jest wynajmowany.");
        RuleFor(e => e.PhoneNumber)
           .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Numer telefonu nie jest poprawny.")
           .When(e => !string.IsNullOrEmpty(e.PhoneNumber));
        RuleFor(e => e.Email)
            .EmailAddress().When(e => !string.IsNullOrEmpty(e.Email))
            .WithMessage("Nieprawidłowy format adresu email.")
            .MaximumLength(50).WithMessage("Adres email nie może przekraczać 50 znaków.");
    }
}
