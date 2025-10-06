//using FarmManager.Model.Model;
//using FluentValidation;

//namespace FarmManager.App.Helpers.Validators;

//public class WorkdayCollectingRemainingValidator : AbstractValidator<WorkdayCollecting>
//{
//    public WorkdayCollectingRemainingValidator()
//    {
//        RuleFor(x => x.RemainingToPay)
//            .Must((entity, remaining) => remaining == entity.Quantity * entity.Price)
//            .WithMessage("Nie można edytować pracy która została już zapłacona.");
//    }
//}
