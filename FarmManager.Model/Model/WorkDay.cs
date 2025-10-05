using System.ComponentModel.DataAnnotations.Schema;
using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Workday : BaseEntity
{
    public DateOnly Date { get; set; }
    public int? PlantId { get; set; }
    public Plant? Plant { get; set; }
    public int? HarvestId { get; set; }
    public Harvest? Harvest { get; set; }
    public int? ActionId { get; set; }
    public Action? Action { get; set; }
    public ICollection<WorkdayCollecting> WorkdaysCollecting { get; set; } = [];
    public ICollection<WorkdayHourly> WorkdaysHourly { get; set; } = [];
    public WorkdayType WorkdayType { get; set; }
    public string? Description { get; set; }

    [NotMapped]
    public string Name => Plant?.Name ?? Action?.Name ?? string.Empty;
    
}
public enum WorkdayType
{
    HarvestCollecting,
    HarvestHourly,
    HourlyWork
}
