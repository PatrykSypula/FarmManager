using FarmManager.Model.Model;
using FluentValidation;

namespace FarmManager.App.Helpers.Validators;
internal class PlantValidator : AbstractValidator<Plant>
{
    public PlantValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Nazwa nie może być pusta.")
            .MaximumLength(50).WithMessage("Nazwa nie moze mieć wiecej niż 50 znaków");
        RuleFor(p => p.Description)
            .MaximumLength(100).WithMessage("Opis nie może mieć więcej niż 100 znaków");
        RuleFor(p => p.Variety.Name)
            .NotEmpty().WithMessage("Należy wybrać odmianę.");
    }
}
