using System.Collections.ObjectModel;
using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.Workdays;
using FarmManager.App.Views;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Workdays;

public class WorkdayAddViewModel(IWorkdayService workdayService, IUnitOfWork unitOfWork) : BaseViewModel
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

    public ObservableCollection<WorkdayHourly> WorkdaysHourly
    {
        get { return Model.WorkdaysHourly; }
        set
        {
            Model.WorkdaysHourly = value;
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

    #endregion

    public RelayCommand Add => new RelayCommand(async execute => await AddWorkdayAsync());

    private async Task AddWorkdayAsync()
    {
        WorkdayValidator validator = new WorkdayValidator();
        Model.Workday.Description = string.IsNullOrEmpty(Model.Workday.Description) ? null : Model.Workday.Description;
        var result = validator.Validate(Model.Workday);
        if (!result.IsValid)
        {
            new CustomMessageBoxOk(result).ShowDialog();
        }
        else
        {
            await workdayService.Add(Model.Workday);
            await unitOfWork.SaveChangesAsync();
            RequestClose?.Invoke(Model.Workday);

        }
    }
}
