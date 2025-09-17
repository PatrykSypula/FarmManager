using FarmManager.Model.Model;
using FluentValidation;

namespace FarmManager.App.Helpers.Validators;

public class SeasonValidator : AbstractValidator<Season>
{
    public SeasonValidator()
    {
        RuleFor(s => s.Name)
            .NotEmpty().WithMessage("Nazwa nie może być pusta.")
            .MaximumLength(50).WithMessage("Nazwa nie moze mieć wiecej niż 50 znaków");
        RuleFor(s => s.Description)
            .MaximumLength(100).WithMessage("Opis nie może mieć więcej niż 100 znaków");
        RuleFor(s => s.StartDate)
            .NotEmpty().WithMessage("Należy wybrać datę rozpoczęcia.");
        RuleFor(s => s.EndDate)
            .NotEmpty().WithMessage("Należy wybrać datę zakończenia.")
            .GreaterThan(s => s.StartDate).WithMessage("Data zakończenia musi być późniejsza niż data rozpoczęcia.");
        RuleFor(s => s.Plant)
            .NotNull().WithMessage("Należy wybrać roślinę.");
    }
}
