namespace FarmManager.Model.Model;

public class Report
{
    public string SeasonName { get; set; } = string.Empty;
    public string PlantName { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
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

    private decimal _investment;
    public decimal Investment
    {
        get => _investment;
        set => _investment = Math.Round(value, 2);
    }

    private decimal _countedInvestment;
    public decimal CountedInvestment
    {
        get => _countedInvestment;
        set => _countedInvestment = Math.Round(value, 2);
    }

    private decimal _countedIncome;
    public decimal CountedIncome
    {
        get => _countedIncome;
        set => _countedIncome = Math.Round(value, 2);
    }

}
