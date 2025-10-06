using System.Collections.ObjectModel;
using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.Workdays.WorkdaysHourly;
using FarmManager.App.Views;
using FarmManager.App.Views.ChooseEntity;
using FarmManager.App.Views.Workdays.WorkdaysHourly;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;
using FarmManager.Services.Services;

namespace FarmManager.App.ViewModels.Workdays.WorkdaysHourly;

public class WorkdayHourlyWorkEditViewModel(IWorkdayService workdayService, IPlantService plantService, IActionService actionService, IUnitOfWork unitOfWork) : BaseViewModel
{
    #region Properties

    public event Action<Workday>? RequestClose;
    public WorkdayHourlyWorkEditModel Model = new WorkdayHourlyWorkEditModel();

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
    public string? Action
    {
        get
        {
            return Model.Action.Name;
        }
        set
        {
            if (Model.Action != null)
                Model.Action.Name = value ?? string.Empty;
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
        if (Model.Workday.ActionId != null)
        {
            Model.Action = await actionService.Get(Model.Workday.ActionId.Value);
        }
        Model.WorkdaysHourly = new ObservableCollection<WorkdayHourly>(await workdayService.GetWorkdaysHourly(id));
        if (Model.Workday.PlantId != null)
        {
            Model.Plant = await plantService.Get(Model.Workday.PlantId.Value);
        }
        OnPropertyChanged(nameof(Plant));
        OnPropertyChanged(nameof(Action));
        OnPropertyChanged(nameof(WorkdaysHourly));
        OnPropertyChanged(nameof(Description));
    }

    #endregion

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
            OnPropertyChanged(nameof(WorkdaysHourly));
        }
    }
    public RelayCommand OpenWorkdayHourlyEdit => new RelayCommand(execute => OpenWorkdayHourlyEditAsync());
    private void OpenWorkdayHourlyEditAsync()
    {
        var window = new WorkdayHourlyEditWindow(SelectedWorkdayHourly, Model.Workday.WorkdaysHourly.Select(wc => wc.Employee.Id).ToList());

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
    public RelayCommand OpenAction => new RelayCommand(execute => OpenSelectActionAsync());
    private void OpenSelectActionAsync()
    {
        var window = new ChooseActionWindow();
        if (window.ShowDialog() == true && window.Action != null)
        {
            Model.Action = window.Action;
            Model.Workday.ActionId = window.Action.Id;
            OnPropertyChanged(nameof(Action));
        }
    }

    public RelayCommand Delete => new RelayCommand(async execute => await DeleteWorkdayAsync());
    private async Task DeleteWorkdayAsync()
    {
        var result = new CustomMessageBoxYesNo("Czy na pewno chcesz usunąć ten dzień?").ShowDialog();
        if (result == true)
        {
            await workdayService.Delete(Model.Workday.Id);
            Model.Workday.IsDeleted = true;
            await unitOfWork.SaveChangesAsync();
            RequestClose?.Invoke(Model.Workday);
        }
    }

    public RelayCommand Update => new RelayCommand(async execute => await UpdateWorkdayAsync());
    private async Task UpdateWorkdayAsync()
    {
        WorkdayHourlyWorkValidator validator = new WorkdayHourlyWorkValidator();
        Model.Workday.Description = string.IsNullOrEmpty(Model.Workday.Description) ? null : Model.Workday.Description;
        var result = validator.Validate(Model.Workday);
        if (!result.IsValid)
        {
            new CustomMessageBoxOk(result).ShowDialog();
        }
        else
        {
            foreach (var wh in Model.Workday.WorkdaysHourly)
            {
                wh.Employee = null;
                wh.Workday = null;
            }
            Model.Workday.Action = null;
            Model.Workday.Harvest = null;
            Model.Workday.Plant = null;
            await workdayService.Update(Model.Workday);
            await unitOfWork.SaveChangesAsync();
            await workdayService.Detach(Model.Workday);
            Model.Workday.Plant = Model.Plant;
            Model.Workday.Action = Model.Action;
            RequestClose?.Invoke(Model.Workday);
        }
    }


}
