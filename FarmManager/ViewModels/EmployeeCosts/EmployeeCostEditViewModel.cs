using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.EmployeeCosts;
using FarmManager.App.Views;
using FarmManager.App.Views.ChooseEntity;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.EmployeeCosts;

public class EmployeeCostEditViewModel(IEmployeeCostService employeeCostService, IEmployeeService employeeService, IUnitOfWork unitOfWork) : BaseViewModel
{
    #region Properties

    public event Action<EmployeeCost>? RequestClose;
    public EmployeeCostAddModel Model = new EmployeeCostAddModel();

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
    public decimal Quantity
    {
        get { return Model.EmployeeCost.Quantity; }
        set
        {
            Model.EmployeeCost.Quantity = value;

            OnPropertyChanged();
        }
    }
    public DateTimeOffset Date
    {
        get
        {
            return Model.EmployeeCost.Date;
        }
        set
        {
            Model.EmployeeCost.Date = value;
            OnPropertyChanged();
        }
    }
    public string? Description
    {
        get
        {
            return Model.EmployeeCost.Description;
        }
        set
        {
            Model.EmployeeCost.Description = value;
            OnPropertyChanged();
        }
    }
    public async Task InitializeAsync(int id)
    {
        Model.EmployeeCost = await employeeCostService.Get(id);
        Model.Employee = await employeeService.Get(Model.EmployeeCost.EmployeeId);
        OnPropertyChanged(nameof(Employee));
        OnPropertyChanged(nameof(Quantity));
        OnPropertyChanged(nameof(Date));
        OnPropertyChanged(nameof(Description));
    }

    #endregion

    public RelayCommand Delete => new RelayCommand(async execute => await DeleteEmployeeCostAsync());
    private async Task DeleteEmployeeCostAsync()
    {
        var result = new CustomMessageBoxYesNo("Czy na pewno chcesz usunąć tę pożyczkę?").ShowDialog();
        if (result == true)
        {
            await employeeCostService.Delete(Model.EmployeeCost.Id);
            Model.EmployeeCost.IsDeleted = true;
            await unitOfWork.SaveChangesAsync();
            RequestClose?.Invoke(Model.EmployeeCost);
        }
    }

    public RelayCommand Update => new RelayCommand(async execute => await UpdateEmployeeCostAsync());
    private async Task UpdateEmployeeCostAsync()
    {
        EmployeeCostValidator validator = new EmployeeCostValidator();
        Model.EmployeeCost.Description = string.IsNullOrEmpty(Model.EmployeeCost.Description) ? null : Model.EmployeeCost.Description;
        var result = validator.Validate(Model.EmployeeCost);
        if (!result.IsValid)
        {
            new CustomMessageBoxOk(result).ShowDialog();
        }
        else
        {
            await employeeCostService.Update(Model.EmployeeCost);
            await unitOfWork.SaveChangesAsync();
            RequestClose?.Invoke(Model.EmployeeCost);
        }
    }

    public RelayCommand OpenEmployee => new RelayCommand(execute => OpenSelectEmployeeAsync());
    private void OpenSelectEmployeeAsync()
    {
        var window = new ChooseEmployeeWindow();
        if (window.ShowDialog() == true && window.Employee != null)
        {
            Model.Employee = window.Employee;
            Model.EmployeeCost.EmployeeId = window.Employee.Id;
            OnPropertyChanged(nameof(Employee));
        }
    }
}
