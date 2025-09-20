using FarmManager.Model.Model;
using FluentValidation;

namespace FarmManager.App.Helpers.Validators;

public class SprayingValidator : AbstractValidator<Spraying>
{
    public SprayingValidator()
    {
        RuleFor(d => d.PlantId).NotNull().WithMessage("Wybierz roślinę.");
        RuleFor(d => d.FertilizerId).NotNull().WithMessage("Wybierz nawóz.");
        RuleFor(d => d.Quantity).GreaterThan(0).WithMessage("Podaj ilość większą od 0.");
    }
}
