using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class WorkdayCollecting : BaseEntity
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    private decimal _quantity;
    public decimal Quantity
    {
        get => _quantity;
        set => _quantity = Math.Round(value, 2);
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
