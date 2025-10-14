using System.Collections.ObjectModel;
using FarmManager.App.Helpers;
using FarmManager.App.Models.Sprayings;
using FarmManager.App.Models.Workdays;
using FarmManager.App.Views.Sprayings;
using FarmManager.App.Views.Workdays;
using FarmManager.App.Views.Workdays.WorkdaysCollecting;
using FarmManager.App.Views.Workdays.WorkdaysHourly;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;
using FarmManager.Services.Services;

namespace FarmManager.App.ViewModels.Workdays;
public class WorkdaysViewModel(IWorkdayService workdayService) : BaseViewModel
{
    #region Properties

    public WorkdaysModel Model = new WorkdaysModel();

    public ObservableCollection<Workday> Workdays
    {
        get { return Model.Workdays; }
        set
        {
            Model.Workdays = value;
            OnPropertyChanged();
        }
    }

    public async Task InitializeAsync(DateOnly date)
    {
        Model.Date = date;
        var workdays = await workdayService.GetWorkdays(Model.Date);
        foreach(var workday in workdays)
        {
            if(!HasDebt(workday))
            {
                workday.IsPaid = true;
            }
        }
        Workdays = new ObservableCollection<Workday>(workdays);
        foreach(var workday in Workdays)
        {
            if(workday.WorkdayType == WorkdayType.HarvestCollecting || workday.WorkdayType == WorkdayType.HarvestHourly)
            {
                AddAction(workday);
            }
        }
    }

    public Workday SelectedItem
    {
        get { return Model.SelectedItem; }
        set
        {
            Model.SelectedItem = value;
            OnPropertyChanged();
        }
    }

    #endregion

    public RelayCommand Create => new RelayCommand(execute => OpenSelectWorkdayTypeWindow());

    private void OpenSelectWorkdayTypeWindow()
    {
        var window = new WorkdaysSelectTypeWindow();
        if (window.ShowDialog() == true && window.WorkdayType != null)
        {
            switch (window.WorkdayType)
            {
                case WorkdayType.HarvestCollecting:
                    OpenHarvestCollectingWindow();
                    break;
                case WorkdayType.HarvestHourly:
                    OpenHarvestHourlyWindow();
                    break;
                case WorkdayType.HourlyWork:
                    OpenHourlyWorkWindow();
                    break;
            }
        }
    }
    
    private async void OpenHarvestCollectingWindow()
    {
        var window = new WorkdayHarvestCollectingAddWindow(Model.Date, WorkdayType.HarvestCollecting);
        if (window.ShowDialog() == true && window.Workday != null)
        {
            AddAction(window.Workday);
            Workdays.Add(window.Workday);
            OnPropertyChanged(nameof(Workdays));
        }
    }
    private async void OpenHarvestHourlyWindow()
    {
        var window = new WorkdayHarvestHourlyAddWindow(Model.Date, WorkdayType.HarvestHourly);
        if (window.ShowDialog() == true && window.Workday != null)
        {
            AddAction(window.Workday);
            Workdays.Add(window.Workday);
            OnPropertyChanged(nameof(Workdays));
        }
    }
    private async void OpenHourlyWorkWindow()
    {
        var window = new WorkdayHourlyWorkAddWindow(Model.Date, WorkdayType.HourlyWork);
        if (window.ShowDialog() == true && window.Workday != null)
        {
            AddAction(window.Workday);
            Workdays.Add(window.Workday);
            OnPropertyChanged(nameof(Workdays));
        }
    }

    public RelayCommand Edit => new RelayCommand(execute => OpenWorkdayEditWindow());
    private void OpenWorkdayEditWindow()
    {
        switch (SelectedItem.WorkdayType)
        {
            case WorkdayType.HarvestCollecting:
                OpenWorkdayHarvestCollectingEditWindow();
                break;
            case WorkdayType.HarvestHourly:
                OpenWorkdayHarvestHourlyEditWindow();
                break;
            case WorkdayType.HourlyWork:
                OpenWorkdayHourlyWorkEditWindow();
                break;
        }
    }
    private void OpenWorkdayHarvestCollectingEditWindow()
    {
        var window = new WorkdayHarvestCollectingEditWindow(SelectedItem.Id);
        if (window.ShowDialog() == true && window.Workday != null)
        {
            var workday = window.Workday;
            if (!HasDebt(workday))
            {
                workday.IsPaid = true;
            }
            AddAction(window.Workday);
            var index = Workdays.ToList().FindIndex(d => d.Id == workday.Id);

            if (index >= 0)
            {
                if (workday.IsDeleted)
                {
                    Workdays.RemoveAt(index);
                }
                else
                {
                    Workdays.RemoveAt(index);
                    Workdays.Insert(index, workday);
                }
            }
            OnPropertyChanged(nameof(Workdays));
        }
    }
    private void OpenWorkdayHarvestHourlyEditWindow()
    {
        var window = new WorkdayHarvestHourlyEditWindow(SelectedItem.Id);
        if (window.ShowDialog() == true && window.Workday != null)
        {
            var workday = window.Workday;
            if (!HasDebt(workday))
            {
                workday.IsPaid = true;
            }
            AddAction(window.Workday);
            var index = Workdays.ToList().FindIndex(d => d.Id == workday.Id);
            if (index >= 0)
            {
                if (workday.IsDeleted)
                {
                    Workdays.RemoveAt(index);
                }
                else
                {
                    Workdays.RemoveAt(index);
                    Workdays.Insert(index, workday);
                }
            }
            OnPropertyChanged(nameof(Workdays));
        }
    }
    private void OpenWorkdayHourlyWorkEditWindow()
    {
        var window = new WorkdayHourlyWorkEditWindow(SelectedItem.Id);
        if (window.ShowDialog() == true && window.Workday != null)
        {
            var workday = window.Workday;
            if (!HasDebt(workday))
            {
                workday.IsPaid = true;
            }
            var index = Workdays.ToList().FindIndex(d => d.Id == workday.Id);
            if (index >= 0)
            {
                if (workday.IsDeleted)
                {
                    Workdays.RemoveAt(index);
                }
                else
                {
                    Workdays.RemoveAt(index);
                    Workdays.Insert(index, workday);
                }
            }
            OnPropertyChanged(nameof(Workdays));
        }
    }
    private void AddAction(Workday workday)
    {
        if(workday.Action == null)
            {
            workday.Action = new Model.Model.Action { Name = "Rwanie" };
        }
    }
    private bool HasDebt(Workday w)
    {
        return w.WorkdaysCollecting.Any(c => c.RemainingToPay > 0) ||
               w.WorkdaysHourly.Any(h => h.RemainingToPay > 0);
    }
}
