using FarmManager.Model.Model;
using FluentValidation;

namespace FarmManager.App.Helpers.Validators;

public class FertilizerValidator : AbstractValidator<Fertilizer>
{
    public FertilizerValidator()
    {
        RuleFor(f => f.Name)
            .NotEmpty().WithMessage("Nazwa nie może być pusta.")
            .MaximumLength(50).WithMessage("Nazwa nie moze mieć wiecej niż 50 znaków.");
        RuleFor(f => f.Description)
            .MaximumLength(100).WithMessage("Opis nie może mieć więcej niż 100 znaków.");
    }
}
