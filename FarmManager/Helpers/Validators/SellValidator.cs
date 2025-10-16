using FarmManager.Model.Model;
using FluentValidation;

namespace FarmManager.App.Helpers.Validators;

public class SellValidator : AbstractValidator<Sell>
{
    public SellValidator()
    {
        RuleFor(sell => sell.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Należy podać cenę.");
        RuleFor(sell => sell.Quantity)
            .GreaterThan(0).WithMessage("Należy podać ilość.");
        RuleFor(sell => sell.DepositId)
            .GreaterThan(0).WithMessage("Należy wybrać depozyt.");
    }
}
