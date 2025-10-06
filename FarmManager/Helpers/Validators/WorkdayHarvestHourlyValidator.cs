using FarmManager.Model.Model;
using FluentValidation;

namespace FarmManager.App.Helpers.Validators;

public class WorkdayHarvestHourlyValidator : AbstractValidator<Workday>
{
    public WorkdayHarvestHourlyValidator()
    {
        RuleFor(x => x.Date).NotEmpty().WithMessage("Data nie może być pusta.");
        RuleFor(x => x.Description).MaximumLength(100).WithMessage("Opis nie może mieć więcej niż 100 znaków.");
        RuleFor(x => x.PlantId)
            .NotNull().WithMessage("Należy wybrać roślinę.")
            .GreaterThan(0).WithMessage("Należy wybrać roślinę.");
        RuleFor(x => x.ActionId).Null().WithMessage("Nie można wybrać akcji dla dnia zbiorowego.");
        RuleFor(x => x.Harvest.HourlyQuantity).GreaterThan(0).WithMessage("Zbiór musi być większy od 0.");
        RuleForEach(x => x.WorkdaysHourly)
            .Cascade(CascadeMode.Stop)
            .SetValidator(new WorkdayHourlyValidator());
        RuleFor(x => x)
            .Must(x => x.WorkdaysHourly.Any())
            .WithMessage("Dzień musi miec conajmniej jednego pracownika.");
    }
}
