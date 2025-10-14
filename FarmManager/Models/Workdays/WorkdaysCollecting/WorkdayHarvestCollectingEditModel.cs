using System.Collections.ObjectModel;
using FarmManager.Model.Model;
using Action = FarmManager.Model.Model.Action;

namespace FarmManager.App.Models.Workdays.WorkdaysCollecting;

public class WorkdayHarvestCollectingEditModel
{
    public Workday Workday { get; set; } = new Workday();
    public ObservableCollection<WorkdayCollecting> WorkdaysCollecting { get; set; } = new ObservableCollection<WorkdayCollecting>();
    public WorkdayCollecting SelectedWorkdayCollecting { get; set; }
    public Harvest Harvest { get; set; } = new Harvest();
    public Plant Plant { get; set; } = new Plant();
    public Action Action { get; set; } = new Action();
    public Harvest PreviousHarvest { get; set; } = new Harvest();
}
