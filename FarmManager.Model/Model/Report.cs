namespace FarmManager.Model.Model;

public class Report
{
    public string SeasonName { get; set; } = string.Empty;
    public string PlantName { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    private decimal _employeeEarnings;
    public decimal EmployeeEarnings
    {
        get => _employeeEarnings;
        set => _employeeEarnings = Math.Round(value, 2);
    }

    private decimal _rent;
    public decimal Rent
    {
        get => _rent;
        set => _rent = Math.Round(value, 2);
    }

    private decimal _income;
    public decimal Income
    {
        get => _income;
        set => _income = Math.Round(value, 2);
    }

    private decimal _sprayingCost;
    public decimal SprayingCost
    {
        get => _sprayingCost;
        set => _sprayingCost = Math.Round(value, 2);
    }

    private decimal _countedIncome;
    public decimal CountedIncome
    {
        get => _countedIncome;
        set => _countedIncome = Math.Round(value, 2);
    }

}
