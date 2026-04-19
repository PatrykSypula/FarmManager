using FarmManager.Model.Model;
using FluentValidation;

namespace FarmManager.App.Helpers.Validators;

public class WorkdayCollectingDeleteValidator : AbstractValidator<WorkdayCollecting>
{
    public WorkdayCollectingDeleteValidator()
    {
        RuleFor(x => x)
            .Must(c => c.RemainingToPay == c.Quantity * c.Price)
            .WithMessage("Nie można usunąć pracy pracownika, która została już opłacona.");
    }
}
