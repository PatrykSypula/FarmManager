using System.Collections.ObjectModel;
using FarmManager.Model.Model;
using Action = FarmManager.Model.Model.Action;

namespace FarmManager.App.Models.Workdays.WorkdaysHourly;

public class WorkdayHarvestHourlyAddModel
{
    public Workday Workday { get; set; } = new Workday();
    public ObservableCollection<WorkdayHourly> WorkdaysHourly { get; set; } = new ObservableCollection<WorkdayHourly>();
    public WorkdayHourly SelectedWorkdayHourly { get; set; }
    public Harvest Harvest { get; set; } = new Harvest();
    public Plant Plant { get; set; } = new Plant();
    public Action Action { get; set; } = new Action();
}
