using FarmManager.Model.Model;
using FluentValidation;

namespace FarmManager.App.Helpers.Validators;

public class EmployeeCostValidator : AbstractValidator<EmployeeCost>
{
    public EmployeeCostValidator()
    {
        RuleFor(x => x.EmployeeId).NotNull().WithMessage("Należy wybrać pracownika.");
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Kwota musi być większa od 0.");
        RuleFor(x => x.Date).NotEmpty().WithMessage("Data nie może być pusta.");
    }
}
