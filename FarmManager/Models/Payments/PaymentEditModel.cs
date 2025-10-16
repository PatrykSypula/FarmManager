using FarmManager.Model.Model;

namespace FarmManager.App.Models.Payments;

public class PaymentEditModel
{
    public Payment Payment { get; set; } = new Payment();
    public Employee Employee { get; set; } = new Employee();
}
