using FarmManager.Model.Model;
using FluentValidation;

namespace FarmManager.App.Helpers.Validators;

public class WorkdayHourlyValidator : AbstractValidator<WorkdayHourly>
{
    public WorkdayHourlyValidator()
    {
        RuleFor(wh => wh.EmployeeId)
            .GreaterThan(0).WithMessage("Należy wybrać pracownika.");
        RuleFor(wh => wh.Hours)
            .GreaterThan(0).WithMessage("Liczba godzin musi być większa niż 0.");
        RuleFor(wh => wh.Price)
            .GreaterThan(0).WithMessage("Stawka godzinowa nie może być zerowa lub ujemna.");
    }
}
