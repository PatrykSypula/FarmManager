using FarmManager.Model.Model;
using FluentValidation;

namespace FarmManager.App.Helpers.Validators;

public class WorkdayHarvestCollectingValidator : AbstractValidator<Workday>  
{
    public WorkdayHarvestCollectingValidator()
    {
        RuleFor(x => x.Date).NotEmpty().WithMessage("Data nie może być pusta.");
        RuleFor(x => x.Description).MaximumLength(100).WithMessage("Opis nie może mieć więcej niż 100 znaków.");
        RuleFor(x => x.PlantId).GreaterThan(0).WithMessage("Należy wybrać roślinę.");
        RuleFor(x => x.ActionId).Null().WithMessage("Nie można wybrać akcji dla dnia zbiorowego.");
        RuleForEach(x => x.WorkdaysCollecting)
            .Cascade(CascadeMode.Stop)
            .SetValidator(new WorkdayCollectingValidator());
        RuleFor(x => x)
            .Must(x => x.WorkdaysCollecting.Any())
            .WithMessage("Dzień musi miec conajmniej jednego pracownika.");
    }
}
