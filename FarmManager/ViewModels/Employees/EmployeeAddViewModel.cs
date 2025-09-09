using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.Employees;
using FarmManager.App.Views;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Employees;

public class EmployeeAddViewModel(IEmployeeService employeeService) : BaseViewModel
{
    public event Action<Employee>? RequestClose;
    public EmployeeAddModel Model = new EmployeeAddModel();

    #region Properties
    public string FirstName
    {
        get
        {
            return Model.Employee.FirstName;
        }
        set
        {
            Model.Employee.FirstName = value;
            OnPropertyChanged();
        }
    }

    public string LastName
    {
        get
        {
            return Model.Employee.LastName;
        }
        set
        {
            Model.Employee.LastName = value;
            OnPropertyChanged();
        }
    }

    public string? IdNumber
    {
        get
        {
            return Model.Employee.IdNumber;
        }
        set
        {
            Model.Employee.IdNumber = value;
            OnPropertyChanged();
        }
    }

    public bool IsRentable
    {
        get
        {
            return Model.Employee.IsRentable;
        }
        set
        {
            Model.Employee.IsRentable = value;
            OnPropertyChanged();
        }
    }

    public double? BaseRent
    {
        get
        {
            return Model.Employee.BaseRent;
        }
        set
        {
            Model.Employee.BaseRent = value;
            OnPropertyChanged();
        }
    }

    public string? PhoneNumber
    {
        get
        {
            return Model.Employee.PhoneNumber;
        }
        set
        {
            Model.Employee.PhoneNumber = value;
            OnPropertyChanged();
        }
    }

    public string? Email
    {
        get
        {
            return Model.Employee.Email;
        }
        set
        {
            Model.Employee.Email = value;
            OnPropertyChanged();
        }
    }

    public bool IsActive
    {
        get
        {
            return Model.Employee.IsActive;
        }
        set
        {
            Model.Employee.IsActive = value;
            OnPropertyChanged();
        }
    } 
    #endregion

    public RelayCommand Add => new RelayCommand(async execute => await AddEmployeeAsync());

    private async Task AddEmployeeAsync()
    {
        EmployeeValidator validator = new EmployeeValidator();
        Model.Employee.IdNumber = string.IsNullOrEmpty(Model.Employee.IdNumber) ? null : Model.Employee.IdNumber;
        Model.Employee.BaseRent = Model.Employee.BaseRent == 0 ? null : Model.Employee.BaseRent;
        Model.Employee.PhoneNumber = string.IsNullOrEmpty(Model.Employee.PhoneNumber) ? null : Model.Employee.PhoneNumber;
        Model.Employee.Email = string.IsNullOrEmpty(Model.Employee.Email) ? null : Model.Employee.Email;
        var result = validator.Validate(Model.Employee);
        if (!result.IsValid)
        {
            new CustomMessageBoxOk(result).ShowDialog();
        }
        else
        {
            await employeeService.Add(Model.Employee);
            RequestClose?.Invoke(Model.Employee);

        }
    }
}
