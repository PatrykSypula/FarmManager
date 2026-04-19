using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.Workdays.WorkdaysHourly;
using FarmManager.App.Views;
using FarmManager.App.Views.ChooseEntity;
using FarmManager.Model.Model;

namespace FarmManager.App.ViewModels.Workdays.WorkdaysHourly;

public class WorkdayHourlyEditViewModel : BaseViewModel
{
    #region Properties

    public event Action<WorkdayHourly>? RequestClose;
    public WorkdayHourlyEditModel Model = new WorkdayHourlyEditModel();

    public string Employee
    {
        get
        {
            return string.IsNullOrWhiteSpace(Model.Employee.Nickname)
                ? $"{Model.Employee.FirstName} {Model.Employee.LastName}"
                : $"{Model.Employee.FirstName} {Model.Employee.Nickname} {Model.Employee.LastName}";
        }
        set
        {
            if (Model.Employee != null)
                Model.Employee.FirstName = value ?? string.Empty;
            OnPropertyChanged();
        }
    }

    public decimal Hours
    {
        get { return Model.WorkdayHourly.Hours; }
        set
        {
            Model.WorkdayHourly.Hours = value;

            OnPropertyChanged();
            UpdateRemainingToPay();
        }
    }
    public decimal Price
    {
        get { return Model.WorkdayHourly.Price; }
        set
        {
            Model.WorkdayHourly.Price = value;

            OnPropertyChanged();
            UpdateRemainingToPay();
        }
    }
    public decimal RemainingToPay
    {
        get { return Model.WorkdayHourly.RemainingToPay; }
        set
        {
            Model.WorkdayHourly.RemainingToPay = value;

            OnPropertyChanged();
        }
    }
    public async Task InitializeAsync(WorkdayHourly workdayhourly, ICollection<int> ids)
    {
        Model.WorkdayHourly = workdayhourly;
        Model.Employee = workdayhourly.Employee;
        Model.EmployeeIds = ids;
        Model.IsEditable = Model.WorkdayHourly.RemainingToPay == Model.WorkdayHourly.Price * Model.WorkdayHourly.Hours;
        OnPropertyChanged(nameof(Employee));
        OnPropertyChanged(nameof(Hours));
        OnPropertyChanged(nameof(Price));
        OnPropertyChanged(nameof(RemainingToPay));
    }

    #endregion

    public RelayCommand Update => new RelayCommand(async execute => await UpdateWorkdayCollectingAsync());
    private async Task UpdateWorkdayCollectingAsync()
    {
        if (!Model.IsEditable)
        {
            new CustomMessageBoxOk("Nie można edytować pracy która została już zapłacona.").ShowDialog();
        }
        else
        {
            WorkdayHourlyValidator validator = new WorkdayHourlyValidator();
            var result = validator.Validate(Model.WorkdayHourly);
            if (!result.IsValid)
            {
                new CustomMessageBoxOk(result).ShowDialog();
            }
            else
            {
                Model.WorkdayHourly.EmployeeId = Model.Employee.Id;
                RequestClose?.Invoke(Model.WorkdayHourly);
            }
        }
    }

    public RelayCommand Delete => new RelayCommand(async execute => await DeleteWorkdayCollectingAsync());
    private async Task DeleteWorkdayCollectingAsync()
    {
        var result = new CustomMessageBoxYesNo("Czy na pewno chcesz usunąć pracę tego pracownika?").ShowDialog();
        if (result == true)
        {
            WorkdayHourlyDeleteValidator validator = new WorkdayHourlyDeleteValidator();
            var resultValidation = validator.Validate(Model.WorkdayHourly);
            if (!resultValidation.IsValid)
            {
                new CustomMessageBoxOk(resultValidation).ShowDialog();
            }
            else
            {
                Model.WorkdayHourly.IsDeleted = true;
                RequestClose?.Invoke(Model.WorkdayHourly);
            }
        }
    }

    public RelayCommand OpenEmployee => new RelayCommand(execute => OpenSelectEmployeeAsync());
    private void OpenSelectEmployeeAsync()
    {
        var window = new ChooseEmployeeWindow(Model.EmployeeIds);
        if (window.ShowDialog() == true && window.Employee != null)
        {
            Model.Employee = window.Employee;
            Model.WorkdayHourly.EmployeeId = window.Employee.Id;
            Model.WorkdayHourly.Employee = window.Employee;
            OnPropertyChanged(nameof(Employee));
        }
    }
    private void UpdateRemainingToPay()
    {
        if(Model.IsEditable)
        {
            RemainingToPay = Price * Hours;
            OnPropertyChanged(nameof(RemainingToPay));
        }
    }
}
