using FarmManager.Model.Model;
using FluentValidation;

namespace FarmManager.App.Helpers.Validators;

public class VarietyValidator : AbstractValidator<Variety>
{
    public VarietyValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Nazwa nie może być pusta.")
            .MaximumLength(50).WithMessage("Nazwa nie moze mieć wiecej niż 50 znaków");
        RuleFor(v => v.Description)
            .MaximumLength(100).WithMessage("Opis nie może mieć więcej niż 100 znaków");
    }
}
