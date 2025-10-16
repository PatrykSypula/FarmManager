using System.Windows.Documents;
using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.Sells;
using FarmManager.App.Models.Sprayings;
using FarmManager.App.Views;
using FarmManager.App.Views.ChooseEntity;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;
using FarmManager.Services.Services;

namespace FarmManager.App.ViewModels.Sells;
public class SellAddViewModel(ISellService sellService, IUnitOfWork unitOfWork) : BaseViewModel
{
    #region Properties

    public event Action<Sell>? RequestClose;
    public SellAddModel Model = new SellAddModel();

    public string? Plant
    {
        get
        {
            return Model.Plant.Name;
        }
        set
        {
            if (Model.Plant != null)
                Model.Plant.Name = value ?? string.Empty;
            OnPropertyChanged();
        }
    }
    public string? Deposit
    {
        get { return Model.Deposit?.Name; }
        set
        {
            if (Model.Deposit != null)
                Model.Deposit.Name = value ?? string.Empty;
            OnPropertyChanged();
        }
    }
    public DateOnly Date
    {
        get
        {
            return Model.Sell.Date;
        }
        set
        {
            Model.Sell.Date = value;
            OnPropertyChanged();
        }
    }
    public decimal Quantity
    {
        get
        {
            return Model.Sell.Quantity;
        }
        set
        {
            Model.Sell.Quantity = value;
            OnPropertyChanged();
        }
    }
    public decimal Price
    {
        get
        {
            return Model.Sell.Price;
        }
        set
        {
            Model.Sell.Price = value;
            OnPropertyChanged();
        }
    }
    public string? Description
    {
        get
        {
            return Model.Sell.Description;
        }
        set
        {
            Model.Sell.Description = value;
            OnPropertyChanged();
        }
    }
    public async Task InitializeAsync()
    {
        Model.Sell.Date = DateOnly.FromDateTime(DateTime.Now);
        OnPropertyChanged(nameof(Date));
    }

    #endregion

    public RelayCommand Add => new RelayCommand(async execute => await AddSprayingAsync());

    private async Task AddSprayingAsync()
    {
        if (Model.Sell.Price == 0)
        {
            var wasteResult = new CustomMessageBoxYesNo("Cena jest ustawiona na 0. Czy chcesz aby ten zbiór był policzony jako straty?").ShowDialog();
            if (wasteResult == false)
            {
                return;
            }

        }

        SellValidator validator = new SellValidator();
        Model.Sell.Description = string.IsNullOrEmpty(Model.Sell.Description) ? null : Model.Sell.Description;
        var result = validator.Validate(Model.Sell);
        if (!result.IsValid)
        {
            new CustomMessageBoxOk(result).ShowDialog();
        }
        else
        {
            var availableQuantity = await sellService.GetAvailableQuantity(Model.Plant.Id);
            if (availableQuantity >= Model.Sell.Quantity)
            {
                Model.Sell.HarvestQuantity = await sellService.AdjustRemainingQuantity(Model.Sell.Quantity, Model.Plant.Id);
                await sellService.Add(Model.Sell);
                await unitOfWork.SaveChangesAsync();
                RequestClose?.Invoke(Model.Sell);
            }
            else
            {
                new CustomMessageBoxOk("Nie ma tyle zbiorów na stanie.").ShowDialog();
            }
        }

    }

    public RelayCommand OpenDeposit => new RelayCommand(execute => OpenSelectDepositAsync());
    private void OpenSelectDepositAsync()
    {
        var window = new ChooseDepositWindow();
        if (window.ShowDialog() == true && window.Deposit != null)
        {
            Model.Deposit = window.Deposit;
            Model.Sell.DepositId = window.Deposit.Id;
            OnPropertyChanged(nameof(Deposit));
        }
    }

    public RelayCommand OpenPlant => new RelayCommand(execute => OpenSelectPlantAsync());
    private void OpenSelectPlantAsync()
    {
        var window = new ChoosePlantWindow();
        if (window.ShowDialog() == true && window.Plant != null)
        {
            Model.Plant = window.Plant;
            Model.Sell.PlantId = window.Plant.Id;
            OnPropertyChanged(nameof(Plant));
        }
    }
}
