
using FarmManager.Model.Model;
using FluentValidation;

namespace FarmManager.App.Helpers.Validators;

public class WorkdayCollectingValidator : AbstractValidator<WorkdayCollecting>
{
    public WorkdayCollectingValidator()
    {
        RuleFor(wc => wc.EmployeeId)
            .GreaterThan(0).WithMessage("Należy wybrać pracownika.");
        RuleFor(wc => wc.Quantity)
            .GreaterThan(0).WithMessage("Ilość musi być większa niż 0.");
        RuleFor(wc => wc.Price)
            .GreaterThan(0).WithMessage("Cena nie może być zerowa lub ujemna.");
    }
}
