using FarmManager.Model.Model;
using FluentValidation;

namespace FarmManager.App.Helpers.Validators;

public class SprayingValidator : AbstractValidator<Spraying>
{
    public SprayingValidator()
    {
        RuleFor(d => d.PlantId).GreaterThan(0).WithMessage("Wybierz roślinę.");
        RuleFor(d => d.FertilizerId).GreaterThan(0).WithMessage("Wybierz nawóz.");
        RuleFor(d => d.Quantity).GreaterThan(0).WithMessage("Podaj ilość większą od 0.");
    }
}
