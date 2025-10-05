using FarmManager.Model.Model;
using FluentValidation;

namespace FarmManager.App.Helpers.Validators;

public class WorkdayHourlyWorkValidator : AbstractValidator<Workday>
{
    public WorkdayHourlyWorkValidator()
    {
        RuleFor(x => x.Date).NotEmpty().WithMessage("Data nie może być pusta.");
        RuleFor(x => x.Description).MaximumLength(100).WithMessage("Opis nie może mieć więcej niż 100 znaków.");
        RuleFor(x => x.ActionId).GreaterThan(0).WithMessage("Należy wybrać akcję.");
        RuleForEach(x => x.WorkdaysHourly)
            .Cascade(CascadeMode.Stop)
            .SetValidator(new WorkdayHourlyValidator());
        RuleFor(x => x)
            .Must(x => x.WorkdaysHourly.Any())
            .WithMessage("Dzień musi miec conajmniej jednego pracownika.");
    }
}
