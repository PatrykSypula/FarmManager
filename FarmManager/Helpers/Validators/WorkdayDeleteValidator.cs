using FarmManager.Model.Model;
using FluentValidation;

namespace FarmManager.App.Helpers.Validators;

public class WorkdayDeleteValidator : AbstractValidator<Workday>
{
    public WorkdayDeleteValidator()
    {
        RuleFor(workday => workday.Harvest)
            .Must(h => h == null || (
                h.RemainingCollectingQuantity == h.CollectingQuantity &&
                h.RemainingQuantityAdditional == h.CollectingQuantityAdditional &&
                h.RemainingHourlyQuantity == h.HourlyQuantity
            ))
            .WithMessage("Nie można usunąć dnia pracy, ponieważ zbiory zostały już sprzedane.");

        RuleFor(workday => workday.WorkdaysCollecting)
            .Cascade(CascadeMode.Stop)
            .Must(list => list.All(c => c.RemainingToPay == c.Quantity * c.Price))
            .WithMessage("Nie można usunąć dnia pracy, ponieważ niektórzy pracownicy zostali już opłaceni.");

        RuleFor(workday => workday.WorkdaysHourly)
            .Cascade(CascadeMode.Stop)
            .Must(list => list.All(h => h.RemainingToPay == h.Hours * h.Price))
            .WithMessage("Nie można usunąć dnia pracy, ponieważ niektórzy pracownicy zostali już opłaceni.");
    }
}
