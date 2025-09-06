using FarmManager.App.Helpers;
using FarmManager.App.Models.Deposits;
using FarmManager.App.Views;
using FarmManager.App.Views.Deposits;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;
using FarmManager.Services.Validators;

namespace FarmManager.App.ViewModels.Deposits;

public class AddDepositViewModel(IDepositService depositService) : ViewModelBase
{
    public event Action RequestClose;
    public AddDepositModel Model = new AddDepositModel();
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

    public string PhoneNumber
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
    public string Email
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
    public string Description
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
    public RelayCommand CreateDeposit => new RelayCommand(execute => CreateDepositService());

    private async void CreateDepositService()
    {
        DepositValidator validator = new DepositValidator();
        var result = validator.Validate(Model.Deposit);
        if (!result.IsValid)
        {
            var errorMessages = string.Join("\n", result.Errors.Select(e => e.ErrorMessage));
            new CustomMessageBoxOk(errorMessages).ShowDialog();
        }
        else
        {
            await depositService.AddDeposit(Model.Deposit);
            RequestClose?.Invoke();
        }
    }
}
