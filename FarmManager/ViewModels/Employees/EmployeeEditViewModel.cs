using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.Employees;
using FarmManager.App.Views;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Employees;

public class EmployeeEditViewModel(IEmployeeService employeeService, IUnitOfWork unitOfWork) : BaseViewModel
{
    #region Properties

    public event Action<Employee>? RequestClose;
    public EmployeeEditModel Model = new EmployeeEditModel();

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

    public async Task InitializeAsync(int id)
    {
        Model.Employee = await employeeService.Get(id);
        OnPropertyChanged(nameof(FirstName));
        OnPropertyChanged(nameof(LastName));
        OnPropertyChanged(nameof(Nickname));
        OnPropertyChanged(nameof(IsRentable));
        OnPropertyChanged(nameof(BaseRent));
        OnPropertyChanged(nameof(PhoneNumber));
        OnPropertyChanged(nameof(Email));
        OnPropertyChanged(nameof(IsActive));
    }

    #endregion

    public RelayCommand Delete => new RelayCommand(async execute => await DeleteEmployeeAsync());
    private async Task DeleteEmployeeAsync()
    {
        var result = new CustomMessageBoxYesNo("Czy na pewno chcesz usunąć tego pracownika?").ShowDialog();
        if (result == true)
        {
            await employeeService.Delete(Model.Employee.Id);
            Model.Employee.IsDeleted = true;
            await unitOfWork.SaveChangesAsync();
            RequestClose?.Invoke(Model.Employee);
        }
    }

    public RelayCommand Update => new RelayCommand(async execute => await UpdateEmployeeAsync());
    private async Task UpdateEmployeeAsync()
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
            await employeeService.Update(Model.Employee);
            await unitOfWork.SaveChangesAsync();
            RequestClose?.Invoke(Model.Employee);

        }
    }
}
