using FarmManager.Model.Model;
using FluentValidation;

namespace FarmManager.App.Helpers.Validators;

public class BuyDeleteValidator : AbstractValidator<Buy>
{
    public BuyDeleteValidator()
    {
        RuleFor(buy => buy.RemainingQuantity).Equal(buy => buy.Quantity).WithMessage("Nie można usunąć zakupionego produktu ponieważ został już użyty.\nNależy najpierw usunąć pryskanie, które go użyło.");
    }
}
