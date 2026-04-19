using FarmManager.Model.Model;
using FluentValidation;

namespace FarmManager.App.Helpers.Validators;

public class WorkdayHourlyDeleteValidator : AbstractValidator<WorkdayHourly>
{
    public WorkdayHourlyDeleteValidator()
    {
        RuleFor(x => x)
            .Must(c => c.RemainingToPay == c.Hours * c.Price)
            .WithMessage("Nie można usunąć pracy pracownika, która została już opłacona.");
    }
}
