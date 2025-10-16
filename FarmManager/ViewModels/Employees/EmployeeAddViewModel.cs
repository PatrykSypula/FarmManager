using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.Employees;
using FarmManager.App.Views;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Employees;

public class EmployeeAddViewModel(IEmployeeService employeeService, IUnitOfWork unitOfWork) : BaseViewModel
{
    #region Properties

    public event Action<Employee>? RequestClose;
    public EmployeeAddModel Model = new EmployeeAddModel();

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

    public string? Nickname
    {
        get
        {
            return Model.Employee.Nickname;
        }
        set
        {
            Model.Employee.Nickname = value;
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

    public decimal? BaseRent
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

    public string? Description
    {
        get
        {
            return Model.Employee.Description;
        }
        set
        {
            Model.Employee.Description = value;
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
        Model.Employee.Nickname = string.IsNullOrEmpty(Model.Employee.Nickname) ? null : Model.Employee.Nickname;
        Model.Employee.BaseRent = Model.Employee.BaseRent == 0 ? null : Model.Employee.BaseRent;
        Model.Employee.PhoneNumber = string.IsNullOrEmpty(Model.Employee.PhoneNumber) ? null : Model.Employee.PhoneNumber;
        Model.Employee.Description = string.IsNullOrEmpty(Model.Employee.Description) ? null : Model.Employee.Description;
        var result = validator.Validate(Model.Employee);
        if (!result.IsValid)
        {
            new CustomMessageBoxOk(result).ShowDialog();
        }
        else
        {
            await employeeService.Add(Model.Employee);
            await unitOfWork.SaveChangesAsync();
            RequestClose?.Invoke(Model.Employee);

        }
    }
}
