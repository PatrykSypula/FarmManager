//using FarmManager.Model.Model;
//using FluentValidation;

//namespace FarmManager.App.Helpers.Validators;

//public class WorkdayHourlyRemainingValidator : AbstractValidator<WorkdayHourly>
//{
//    public WorkdayHourlyRemainingValidator()
//    {
//        RuleFor(x => x.RemainingToPay)
//            .Must((entity, remaining) => remaining == entity.Hours * entity.Price)
//            .WithMessage("Nie można edytować pracy która została już zapłacona.");
//    }
//}
