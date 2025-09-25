using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class WorkDay : BaseEntity
{
    public DateTimeOffset Date { get; set; }
    public ICollection<WorkDayCollecting> WorkDayCollectings { get; set; } = [];
    public bool IsCollectingPayed { get; set; } = false;
    public ICollection<WorkDayHourly> WorkDayHourly { get; set; } = [];
    public bool IsHourlyPayed { get; set; } = false;
}
