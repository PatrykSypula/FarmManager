namespace FarmManager.Model.Model;

public class Report
{
    public string SeasonName { get; set; } = string.Empty;
    public string PlantName { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public decimal EmployeeEarnings { get; set; }
    public decimal Rent { get; set; }
    public decimal Income { get; set; }
    public decimal SprayingCost { get; set; }
    public decimal CountedIncome { get; set; }
}
