using FarmManager.Model.Model;
using FluentValidation;

namespace FarmManager.App.Helpers.Validators;

public class InvestmentValidator: AbstractValidator<Investment>
{
    public InvestmentValidator()
    {
        RuleFor(i => i.Name)
            .NotEmpty().WithMessage("Nazwa nie może być pusta.")
            .MaximumLength(50).WithMessage("Nazwa nie moze mieć wiecej niż 50 znaków.");
        RuleFor(d => d.PlantId).GreaterThan(0).WithMessage("Wybierz roślinę.");
        RuleFor(i => i.Price)
            .GreaterThan(0).WithMessage("Kwota musi być większa od 0.");
        RuleFor(i => i.Date)
            .NotEmpty().WithMessage("Data nie może być pusta.");
        RuleFor(i => i.Description)
            .MaximumLength(100).WithMessage("Opis nie może mieć więcej niż 100 znaków.");
        
    }
}
