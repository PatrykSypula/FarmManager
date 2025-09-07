using FarmManager.App.Helpers;
using FarmManager.App.Models.Deposits;
using FarmManager.App.Views;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;
using FarmManager.Services.Validators;

namespace FarmManager.App.ViewModels.Deposits;

public class DepositAddViewModel(IDepositService depositService) : BaseViewModel
{
    public event Action<Deposit>? RequestClose;
    public DepositAddModel Model = new DepositAddModel();
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
    public RelayCommand Add => new RelayCommand(async execute => await AddDepositAsync());

    private async Task AddDepositAsync()
    {
        DepositValidator validator = new DepositValidator();
        var result = validator.Validate(Model.Deposit);
        if (!result.IsValid)
        {
            new CustomMessageBoxOk(result).ShowDialog();
        }
        else
        {
            await depositService.Add(Model.Deposit);
            RequestClose?.Invoke(Model.Deposit);
            
        }
    }
}
