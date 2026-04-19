using FarmManager.Model.Model;
using FluentValidation;

namespace FarmManager.App.Helpers.Validators;

public class SellValidator : AbstractValidator<Sell>
{
    public SellValidator()
    {
        RuleFor(sell => sell.PlantId)
            .GreaterThan(0).WithMessage("Należy wybrać roślinę.");
        RuleFor(sell => sell.DepositId)
            .GreaterThan(0).WithMessage("Należy wybrać kupca.");
        RuleFor(sell => sell.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Należy podać cenę.");
        RuleFor(sell => sell.Quantity)
            .GreaterThan(0).WithMessage("Należy podać ilość.");
        
    }
}
