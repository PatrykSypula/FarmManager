using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Payment : BaseEntity
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    private decimal _quantity;
    public decimal Quantity
    {
        get => _quantity;
        set => _quantity = Math.Round(value, 2);
    }

    private decimal _employeeCost;
    public decimal EmployeeCost
    {
        get => _employeeCost;
        set => _employeeCost = Math.Round(value, 2);
    }

    private decimal _paymentQuantity;
    public decimal PaymentQuantity
    {
        get => _paymentQuantity;
        set => _paymentQuantity = Math.Round(value, 2);
    }

    private decimal _rentCost;
    public decimal RentCost
    {
        get => _rentCost;
        set => _rentCost = Math.Round(value, 2);
    }

    public ICollection<PaymentWorkdayQuantity> WorkdayQuantity { get; set; } = [];
    public ICollection<int> EmployeeCosts { get; set; } = []; 
    public string? Description { get; set; }
    public DateOnly Date { get; set; }
}
