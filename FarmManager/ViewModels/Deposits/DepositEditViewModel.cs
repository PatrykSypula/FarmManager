using FarmManager.App.Helpers;
using FarmManager.App.Models.Deposits;
using FarmManager.App.Views;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;
using FarmManager.Services.Validators;

namespace FarmManager.App.ViewModels.Deposits;

public class DepositEditViewModel(IDepositService depositService) : BaseViewModel
{
    public event Action<Deposit>? RequestClose;
    public DepositEditModel Model = new DepositEditModel();

    #region Properties
    public string Name
    {
        get
        {
            return Model.Deposit.Name;
        }
        set
        {
            Model.Deposit.Name = value;
            OnPropertyChanged();
        }
    }

    public string? PhoneNumber
    {
        get
        {
            return Model.Deposit.PhoneNumber;
        }
        set
        {
            Model.Deposit.PhoneNumber = value;
            OnPropertyChanged();
        }
    }
    public string? Email
    {
        get
        {
            return Model.Deposit.Email;
        }
        set
        {
            Model.Deposit.Email = value;
            OnPropertyChanged();
        }
    }
    public string? Description
    {
        get
        {
            return Model.Deposit.Description;
        }
        set
        {
            Model.Deposit.Description = value;
            OnPropertyChanged();
        }
    }

    public bool IsActive
    {
        get
        {
            return Model.Deposit.IsActive;
        }
        set
        {
            Model.Deposit.IsActive = value;
            OnPropertyChanged();
        }
    } 
    #endregion


    public async Task InitializeAsync(int id)
    {
        Model.Deposit = await depositService.Get(id);
        OnPropertyChanged(nameof(Name));
        OnPropertyChanged(nameof(PhoneNumber));
        OnPropertyChanged(nameof(Email));
        OnPropertyChanged(nameof(Description));
        OnPropertyChanged(nameof(IsActive));
    }

    public RelayCommand Delete => new RelayCommand(async execute => await DeleteDepositAsync());
    private async Task DeleteDepositAsync()
    {
        var result = new CustomMessageBoxYesNo("Czy na pewno chcesz usunąć ten depozyt?").ShowDialog();
        if (result == true)
        {
            await depositService.Delete(Model.Deposit.Id);
            Model.Deposit.IsDeleted = true;
            RequestClose?.Invoke(Model.Deposit);
        }
    }

    public RelayCommand Update => new RelayCommand(async execute => await UpdateDepositAsync());
    private async Task UpdateDepositAsync()
    {
        DepositValidator validator = new DepositValidator();
        Model.Deposit.PhoneNumber = string.IsNullOrEmpty(Model.Deposit.PhoneNumber) ? null : Model.Deposit.PhoneNumber;
        Model.Deposit.Email = string.IsNullOrEmpty(Model.Deposit.Email) ? null : Model.Deposit.Email;
        Model.Deposit.Description = string.IsNullOrEmpty(Model.Deposit.Description) ? null : Model.Deposit.Description;
        var result = validator.Validate(Model.Deposit);
        if (!result.IsValid)
        {
            new CustomMessageBoxOk(result).ShowDialog();
        }
        else
        {
            await depositService.Update(Model.Deposit);
            RequestClose?.Invoke(Model.Deposit);
        }
    }
}
