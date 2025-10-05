using System.Collections.ObjectModel;
using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.Workdays;
using FarmManager.App.Views;
using FarmManager.App.Views.ChooseEntity;
using FarmManager.App.Views.Workdays.WorkdaysCollecting;
using FarmManager.App.Views.Workdays.WorkdaysHourly;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Workdays;

public class WorkdayAddViewModel(IWorkdayService workdayService, IHarvestService harvestService, IUnitOfWork unitOfWork) : BaseViewModel
{
    #region Properties

    public event Action<Workday>? RequestClose;
    public WorkdayAddModel Model = new WorkdayAddModel();

    public ObservableCollection<WorkdayCollecting> WorkdaysCollecting
    {
        get { return Model.WorkdaysCollecting; }
        set
        {
            Model.WorkdaysCollecting = value;
            OnPropertyChanged();
        }
    }
    public WorkdayCollecting SelectedWorkdayCollecting
    {
        get { return Model.SelectedWorkdayCollecting; }
        set
        {
            Model.SelectedWorkdayCollecting = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<WorkdayHourly> WorkdaysHourly
    {
        get { return Model.WorkdaysHourly; }
        set
        {
            Model.WorkdaysHourly = value;
            OnPropertyChanged();
        }
    }
    public WorkdayHourly SelectedWorkdayHourly
    {
        get { return Model.SelectedWorkdayHourly; }
        set
        {
            Model.SelectedWorkdayHourly = value;
            OnPropertyChanged();
        }
    }
    public string? Plant
    {
        get
        {
            return Model.Plant.Name;
        }
        set
        {
            if (Model.Plant != null)
                Model.Plant.Name = value ?? string.Empty;
            OnPropertyChanged();
        }
    }
    public double CollectingQuantity
    {
        get { return Model.Harvest.CollectingQuantity; }
        set
        {
            Model.Harvest.CollectingQuantity = value;

            OnPropertyChanged();
        }
    }
    public double CollectingQuantityAdditional
    {
        get { return Model.Harvest.CollectingQuantityAdditional; }
        set
        {
            Model.Harvest.CollectingQuantityAdditional = value;

            OnPropertyChanged();
        }
    }
    public double HourlyQuantity
    {
        get { return Model.Harvest.HourlyQuantity; }
        set
        {
            Model.Harvest.HourlyQuantity = value;

            OnPropertyChanged();
        }
    }
    public string? Description
    {
        get
        {
            return Model.Workday.Description;
        }
        set
        {
            Model.Workday.Description = value;
            OnPropertyChanged();
        }
    }
    public async Task InitializeAsync(DateOnly date)
    {
        Model.Workday.Date = date;
    }

    #endregion

    #region WorkdayCollecting

    public RelayCommand OpenWorkdayCollectingAddAll => new RelayCommand(execute => OpenWorkdayCollectingAddAllAsync());
    private void OpenWorkdayCollectingAddAllAsync()
    {
        var window = new WorkdayCollectingAddAllWindow(Model.Workday.WorkdaysCollecting.Select(wc => wc.Employee.Id).ToList());
        if (window.ShowDialog() == true && window.WorkdaysCollecting != null)
        {
            foreach (var wc in window.WorkdaysCollecting)
            {
                Model.Workday.WorkdaysCollecting.Add(wc);
                Model.WorkdaysCollecting.Add(wc);
            }
            OnPropertyChanged(nameof(WorkdaysCollecting));
            CalculateCollectingQuantity();
        }
    }

    public RelayCommand OpenWorkdayCollectingAddOne => new RelayCommand(execute => OpenWorkdayCollectingAddOneAsync());
    private void OpenWorkdayCollectingAddOneAsync()
    {
        var window = new WorkdayCollectingAddOneWindow(Model.Workday.WorkdaysCollecting.Select(wc => wc.Employee.Id).ToList());
        if (window.ShowDialog() == true && window.WorkdayCollecting != null)
        {
            Model.Workday.WorkdaysCollecting.Add(window.WorkdayCollecting);
            Model.WorkdaysCollecting.Add(window.WorkdayCollecting);
            OnPropertyChanged(nameof(WorkdaysCollecting));
            CalculateCollectingQuantity();
        }
    }
    public RelayCommand OpenWorkdayCollectingEdit => new RelayCommand(execute => OpenWorkdayCollectingEditAsync());
    private void OpenWorkdayCollectingEditAsync()
    {
        var window = new WorkdayCollectingEditWindow(SelectedWorkdayCollecting, Model.Workday.WorkdaysCollecting.Select(wc => wc.Employee.Id).ToList());

        if (window.ShowDialog() == true && window.WorkdayCollecting != null)
        {
            var edited = window.WorkdayCollecting;

            if (window.WorkdayCollecting.IsDeleted)
            {
                var toRemove1 = Model.Workday.WorkdaysCollecting
                    .FirstOrDefault(wc => wc.Employee.Id == edited.Employee.Id);
                if (toRemove1 != null)
                    Model.Workday.WorkdaysCollecting.Remove(toRemove1);

                var toRemove2 = Model.WorkdaysCollecting
                    .FirstOrDefault(wc => wc.Employee.Id == edited.Employee.Id);
                if (toRemove2 != null)
                    Model.WorkdaysCollecting.Remove(toRemove2);
            }
            else
            {
                var index1 = Model.Workday.WorkdaysCollecting
                    .ToList().FindIndex(wc => wc.Employee.Id == edited.Employee.Id);
                if (index1 >= 0)
                {
                    Model.Workday.WorkdaysCollecting.Remove(Model.Workday.WorkdaysCollecting.ElementAt(index1));
                    Model.Workday.WorkdaysCollecting.Add(edited);
                }

                var index2 = Model.WorkdaysCollecting
                    .ToList().FindIndex(wc => wc.Employee.Id == edited.Employee.Id);
                if (index2 >= 0)
                {
                    Model.WorkdaysCollecting[index2] = edited;
                }
            }

            Model.WorkdaysCollecting = new ObservableCollection<WorkdayCollecting>(Model.WorkdaysCollecting);
            OnPropertyChanged(nameof(WorkdaysCollecting));
            CalculateCollectingQuantity();
        }

    }

    #endregion

    #region WorkdayHourly

    public RelayCommand OpenWorkdayHourlyAddAll => new RelayCommand(execute => OpenWorkdayHourlyAddAllAsync());

    private void OpenWorkdayHourlyAddAllAsync()
    {
        var window = new WorkdayHourlyAddAllWindow(Model.Workday.WorkdaysHourly.Select(wc => wc.Employee.Id).ToList());
        if (window.ShowDialog() == true && window.WorkdaysHourly != null)
        {
            foreach (var wc in window.WorkdaysHourly)
            {
                Model.Workday.WorkdaysHourly.Add(wc);
                Model.WorkdaysHourly.Add(wc);
            }
            OnPropertyChanged(nameof(WorkdaysHourly));
        }
    }

    public RelayCommand OpenWorkdayHourlyAddOne => new RelayCommand(execute => OpenWorkdayHourlyAddOneAsync());
    private void OpenWorkdayHourlyAddOneAsync()
    {
        var window = new WorkdayHourlyAddOneWindow(Model.Workday.WorkdaysHourly.Select(wc => wc.Employee.Id).ToList());
        if (window.ShowDialog() == true && window.WorkdayHourly != null)
        {
            Model.Workday.WorkdaysHourly.Add(window.WorkdayHourly);
            Model.WorkdaysHourly.Add(window.WorkdayHourly);
            OnPropertyChanged(nameof(WorkdayHourly));
        }
    }
    public RelayCommand OpenWorkdayHourlyEdit => new RelayCommand(execute => OpenWorkdayHourlyEditAsync());
    private void OpenWorkdayHourlyEditAsync()
    {
        var window = new WorkdayHourlyEditWindow(SelectedWorkdayHourly,
            Model.Workday.WorkdaysHourly.Select(wc => wc.Employee.Id).ToList()
        );

        if (window.ShowDialog() == true && window.WorkdayHourly != null)
        {
            var edited = window.WorkdayHourly;

            if (window.WorkdayHourly.IsDeleted)
            {
                var toRemove1 = Model.Workday.WorkdaysHourly
                    .FirstOrDefault(wc => wc.Employee.Id == edited.Employee.Id);
                if (toRemove1 != null)
                    Model.Workday.WorkdaysHourly.Remove(toRemove1);

                var toRemove2 = Model.WorkdaysHourly
                    .FirstOrDefault(wc => wc.Employee.Id == edited.Employee.Id);
                if (toRemove2 != null)
                    Model.WorkdaysHourly.Remove(toRemove2);
            }
            else
            {
                var index1 = Model.Workday.WorkdaysHourly
                    .ToList().FindIndex(wc => wc.Employee.Id == edited.Employee.Id);
                if (index1 >= 0)
                {
                    Model.Workday.WorkdaysHourly.Remove(Model.Workday.WorkdaysHourly.ElementAt(index1));
                    Model.Workday.WorkdaysHourly.Add(edited);
                }

                var index2 = Model.WorkdaysHourly
                    .ToList().FindIndex(wc => wc.Employee.Id == edited.Employee.Id);
                if (index2 >= 0)
                {
                    Model.WorkdaysHourly[index2] = edited;
                }
            }

            Model.WorkdaysHourly = new ObservableCollection<WorkdayHourly>(Model.WorkdaysHourly);
            OnPropertyChanged(nameof(WorkdaysHourly));
        }
    }

    #endregion

    public RelayCommand OpenPlant => new RelayCommand(execute => OpenSelectPlantAsync());
    private void OpenSelectPlantAsync()
    {
        var window = new ChoosePlantWindow();
        if (window.ShowDialog() == true && window.Plant != null)
        {
            Model.Plant = window.Plant;
            Model.Workday.PlantId = window.Plant.Id;
            OnPropertyChanged(nameof(Plant));
        }
    }

    public RelayCommand Add => new RelayCommand(async execute => await AddWorkdayAsync());

    private async Task AddWorkdayAsync()
    {
        WorkdayHarvestCollectingValidator validator = new WorkdayHarvestCollectingValidator();
        Model.Workday.Description = string.IsNullOrEmpty(Model.Workday.Description) ? null : Model.Workday.Description;
        var result = validator.Validate(Model.Workday);
        if (!result.IsValid)
        {
            new CustomMessageBoxOk(result).ShowDialog();
        }
        else
        {
            var harvestId = await harvestService.Add(Model.Harvest);
            Model.Workday.HarvestId = harvestId;
            await workdayService.Add(Model.Workday);
            await unitOfWork.SaveChangesAsync();
            RequestClose?.Invoke(Model.Workday);
        }
    }

    private void CalculateCollectingQuantity()
    {
        double total = 0;
        foreach (var wc in Model.Workday.WorkdaysCollecting)
        {
            total += wc.Quantity;
        }
        CollectingQuantity = total;
        OnPropertyChanged(nameof(CollectingQuantity));
    }
}
