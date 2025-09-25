using FarmManager.Model.Model;
using FluentValidation;

namespace FarmManager.App.Helpers.Validators;

public class BuyDeleteValidator : AbstractValidator<Buy>
{
    public BuyDeleteValidator()
    {
        RuleFor(buy => buy.RemainingQuantity).Equal(buy => buy.Quantity).WithMessage("Nie można usunąć zakupu który został już użyty.\nNależy najpierw usunąć pryskania.");
    }
}
