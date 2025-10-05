using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.Workdays.WorkdaysCollecting;
using FarmManager.App.Views;
using FarmManager.App.Views.ChooseEntity;
using FarmManager.Model.Model;

namespace FarmManager.App.ViewModels.Workdays.WorkdaysCollecting;

public class WorkdayCollectingEditViewModel : BaseViewModel
{
    #region Properties

    public event Action<WorkdayCollecting>? RequestClose;
    public WorkdayCollectingEditModel Model = new WorkdayCollectingEditModel();

    public string Employee
    {
        get
        {
            return Model.Employee.FirstName + " " + Model.Employee.LastName;
        }
        set
        {
            if (Model.Employee != null)
                Model.Employee.FirstName = value ?? string.Empty;
            OnPropertyChanged();
        }
    }

    public double Quantity
    {
        get { return Model.WorkdayCollecting.Quantity; }
        set
        {
            Model.WorkdayCollecting.Quantity = value;

            OnPropertyChanged();
        }
    }
    public double Price
    {
        get { return Model.WorkdayCollecting.Price; }
        set
        {
            Model.WorkdayCollecting.Price = value;

            OnPropertyChanged();
        }
    }
    public async Task InitializeAsync(WorkdayCollecting workdayCollecting, ICollection<int> ids)
    {
        Model.WorkdayCollecting = workdayCollecting;
        Model.Employee = workdayCollecting.Employee;
        Model.EmployeeIds = ids;
        OnPropertyChanged(nameof(Employee));
        OnPropertyChanged(nameof(Quantity));
        OnPropertyChanged(nameof(Price));
    }

    #endregion

    public RelayCommand Update => new RelayCommand(async execute => await UpdateWorkdayCollectingAsync());
    private async Task UpdateWorkdayCollectingAsync()
    {
        WorkdayCollectingValidator validator = new WorkdayCollectingValidator();
        var result = validator.Validate(Model.WorkdayCollecting);
        if (!result.IsValid)
        {
            new CustomMessageBoxOk(result).ShowDialog();
        }
        else
        {
            Model.WorkdayCollecting.EmployeeId = Model.Employee.Id;
            RequestClose?.Invoke(Model.WorkdayCollecting);
        }
    }

    public RelayCommand Delete => new RelayCommand(async execute => await DeleteWorkdayCollectingAsync());
    private async Task DeleteWorkdayCollectingAsync()
    {
        var result = new CustomMessageBoxYesNo("Czy na pewno chcesz usunąć pracę tego pracownika?").ShowDialog();
        if (result == true)
        {
            Model.WorkdayCollecting.IsDeleted = true;
            RequestClose?.Invoke(Model.WorkdayCollecting);
        }
    }

    public RelayCommand OpenEmployee => new RelayCommand(execute => OpenSelectEmployeeAsync());
    private void OpenSelectEmployeeAsync()
    {
        var window = new ChooseEmployeeWindow(Model.EmployeeIds);
        if (window.ShowDialog() == true && window.Employee != null)
        {
            Model.Employee = window.Employee;
            Model.WorkdayCollecting.EmployeeId = window.Employee.Id;
            Model.WorkdayCollecting.Employee = window.Employee;
            OnPropertyChanged(nameof(Employee));
        }
    }
}
