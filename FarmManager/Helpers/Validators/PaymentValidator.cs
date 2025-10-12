using FarmManager.Model.Model;
using FluentValidation;

namespace FarmManager.App.Helpers.Validators;

public class PaymentValidator : AbstractValidator<Payment>
{
    public PaymentValidator()
    {
        RuleFor(p => p.EmployeeId)
            .GreaterThan(0).WithMessage("Należy wybrać pracownika.");
        RuleFor(d => d.Description)
            .MaximumLength(100).WithMessage("Opis nie może mieć więcej niż 100 znaków.");
    }
}
