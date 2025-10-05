using FluentValidation;
using Action = FarmManager.Model.Model.Action;

namespace FarmManager.App.Helpers.Validators;

internal class ActionValidator : AbstractValidator<Action>
{
    public ActionValidator()
    {
        RuleFor(d => d.Name)
            .NotEmpty().WithMessage("Nazwa nie może być pusta.")
            .MaximumLength(50).WithMessage("Nazwa nie moze mieć wiecej niż 50 znaków.");
        RuleFor(d => d.Description)
            .MaximumLength(100).WithMessage("Opis nie może mieć więcej niż 100 znaków.");
    }
}
