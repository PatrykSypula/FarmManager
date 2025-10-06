using System.Collections.ObjectModel;
using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.Workdays.WorkdaysCollecting;
using FarmManager.App.Views;
using FarmManager.App.Views.ChooseEntity;
using FarmManager.App.Views.Workdays.WorkdaysCollecting;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;
using FarmManager.Services.Services;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.App.ViewModels.Workdays.WorkdaysCollecting;

public class WorkdayHarvestCollectingEditViewModel(IWorkdayService workdayService, IHarvestService harvestService, IPlantService plantService, IUnitOfWork unitOfWork) : BaseViewModel
{
    #region Properties

    public event Action<Workday>? RequestClose;
    public WorkdayHarvestCollectingEditModel Model = new WorkdayHarvestCollectingEditModel();

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
    public async Task InitializeAsync(int id)
    {
        Model.Workday = await workdayService.GetWorkday(id);
        if(Model.Workday.HarvestId != null)
        {
            Model.Harvest = await harvestService.Get(Model.Workday.HarvestId.Value);
        }
        Model.WorkdaysCollecting = new ObservableCollection<WorkdayCollecting>(await workdayService.GetWorkdaysCollecting(id));
        if(Model.Workday.PlantId != null)
        {
            Model.Plant = await plantService.Get(Model.Workday.PlantId.Value);
        }
        CalculateCollectingQuantity();
        OnPropertyChanged(nameof(Plant));
        OnPropertyChanged(nameof(WorkdaysCollecting));
        OnPropertyChanged(nameof(Description));
        OnPropertyChanged(nameof(CollectingQuantity));
        OnPropertyChanged(nameof(CollectingQuantityAdditional));
    }

    #endregion

    public RelayCommand OpenWorkdayCollectingAddAll => new RelayCommand(execute => OpenWorkdayCollectingAddAllAsync());
    private void OpenWorkdayCollectingAddAllAsync()
    {
        var window = new WorkdayCollectingAddAllWindow(Model.Workday.WorkdaysCollecting.Select(wc => wc.EmployeeId).ToList());
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
        var window = new WorkdayCollectingAddOneWindow(Model.Workday.WorkdaysCollecting.Select(wc => wc.EmployeeId).ToList());
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
        var window = new WorkdayCollectingEditWindow(SelectedWorkdayCollecting, Model.Workday.WorkdaysCollecting.Select(wc => wc.EmployeeId).ToList());

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

    public RelayCommand Delete => new RelayCommand(async execute => await DeleteWorkdayAsync());
    private async Task DeleteWorkdayAsync()
    {
        var result = new CustomMessageBoxYesNo("Czy na pewno chcesz usunąć ten dzień?").ShowDialog();
        if (result == true)
        {
            await harvestService.Delete(Model.Harvest.Id);
            await workdayService.Delete(Model.Workday.Id);
            Model.Workday.IsDeleted = true;
            await unitOfWork.SaveChangesAsync();
            RequestClose?.Invoke(Model.Workday);
        }
    }

    public RelayCommand Update => new RelayCommand(async execute => await UpdateWorkdayAsync());
    private async Task UpdateWorkdayAsync()
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
            foreach (var wc in Model.Workday.WorkdaysCollecting)
            {
                wc.Employee = null;
                wc.Workday = null;
            }
            Model.Workday.Action = null;
            Model.Workday.Harvest = null;
            Model.Workday.Plant = null;
            await harvestService.Update(Model.Harvest);
            await workdayService.Update(Model.Workday);
            await unitOfWork.SaveChangesAsync();
            await workdayService.Detach(Model.Workday);
            Model.Workday.Plant = Model.Plant;
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
