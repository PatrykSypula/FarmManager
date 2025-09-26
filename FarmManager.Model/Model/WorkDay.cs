using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Workday : BaseEntity
{
    public DateTimeOffset Date { get; set; }
    public ICollection<WorkdayCollecting> WorkdaysCollecting { get; set; } = [];
    public bool IsCollectingPayed { get; set; } = false;
    public ICollection<WorkdayHourly> WorkdaysHourly { get; set; } = [];
    public bool IsHourlyPayed { get; set; } = false;
    public string? Description { get; set; }
}
