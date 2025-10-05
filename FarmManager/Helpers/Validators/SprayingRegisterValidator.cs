using FarmManager.Model.Model;
using FluentValidation;

namespace FarmManager.App.Helpers.Validators;
public class SprayingRegisterValidator : AbstractValidator<Fertilizer>
{
    private readonly double _registerQuantity;

    public SprayingRegisterValidator(double availableQuantity)
    {
        _registerQuantity = availableQuantity;

        //RuleFor(f => f.Quantity)
        //    .GreaterThanOrEqualTo(_registerQuantity)
        //    .WithMessage($"Nie ma tyle nawozów w magazynie.");
    }
}
