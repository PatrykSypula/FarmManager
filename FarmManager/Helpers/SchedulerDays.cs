namespace FarmManager.App.Helpers;

public class SchedulerEvent
{
    public string Text { get; set; } = string.Empty;
    public bool IsPaid { get; set; }
}

public class SchedulerDay
{
    public DateTime Date { get; set; }
    public bool IsCurrentMonth { get; set; }
    public List<SchedulerEvent> Events { get; set; } = new();
}

