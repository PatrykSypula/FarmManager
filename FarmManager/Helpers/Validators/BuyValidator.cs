using FarmManager.Model.Model;
using FluentValidation;

namespace FarmManager.App.Helpers.Validators;

public class BuyValidator : AbstractValidator<Buy>
{
    public BuyValidator()
    {
        RuleFor(buy => buy.Price)
            .GreaterThan(0).WithMessage("Cena musi być większa niż 0.");
        RuleFor(buy => buy.Quantity)
            .GreaterThan(0).WithMessage("Ilość musi być większa niż 0.");
        RuleFor(buy => buy.RemainingQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Pozostała ilość nie może być mniejsza niż 0.");
        RuleFor(buy => buy.VendorId)
            .NotNull().WithMessage("Należy wybrać sprzedawcę.");
        RuleFor(buy => buy.FertilizerId)
            .NotNull().WithMessage("Należy wybrać nawóz.");
    }
}
