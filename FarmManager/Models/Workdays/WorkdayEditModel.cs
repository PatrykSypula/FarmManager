using System.Collections.ObjectModel;
using FarmManager.Model.Model;

namespace FarmManager.App.Models.Workdays;

public class WorkdayEditModel
{
    public Workday Workday { get; set; } = new Workday();
    public ObservableCollection<WorkdayCollecting> WorkdaysCollecting { get; set; }
    public ObservableCollection<WorkdayHourly> WorkdaysHourly { get; set; }
}
