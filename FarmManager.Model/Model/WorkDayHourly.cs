using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class WorkdayHourly : BaseEntity
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    private decimal _hours;
    public decimal Hours
    {
        get => _hours;
        set => _hours = Math.Round(value, 2);
    }

    private decimal _price;
    public decimal Price
    {
        get => _price;
        set => _price = Math.Round(value, 2);
    }

    private decimal _remainingToPay;
    public decimal RemainingToPay
    {
        get => _remainingToPay;
        set => _remainingToPay = Math.Round(value, 2);
    }

    public int WorkdayId { get; set; }
    public Workday Workday { get; set; }
}
