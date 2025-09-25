using FarmManager.App.Helpers;
using FarmManager.App.Models.Deposits;
using FarmManager.App.Views;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;
using FarmManager.Services.Validators;

namespace FarmManager.App.ViewModels.Deposits;

public class DepositAddViewModel(IDepositService depositService, IUnitOfWork unitOfWork) : BaseViewModel
{
    #region Properties

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

    #endregion

    public RelayCommand Add => new RelayCommand(async execute => await AddDepositAsync());

    private async Task AddDepositAsync()
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
            await depositService.Add(Model.Deposit);
            await unitOfWork.SaveChangesAsync();
            RequestClose?.Invoke(Model.Deposit);
            
        }
    }
}
