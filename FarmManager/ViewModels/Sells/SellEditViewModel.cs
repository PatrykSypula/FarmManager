using FarmManager.App.Helpers;
using FarmManager.App.Models.Sells;
using FarmManager.App.Models.Sprayings;
using FarmManager.App.Views;
using FarmManager.App.Views.ChooseEntity;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Sells;

public class SellEditViewModel(ISellService sellService, IDepositService depositService, IPlantService plantService, IUnitOfWork unitOfWork) : BaseViewModel
{
    #region Properties

    public event Action<Sell>? RequestClose;
    public SellEditModel Model = new SellEditModel();

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

    public async Task InitializeAsync(int id)
    {
        Model.Sell = await sellService.Get(id);
        Model.Plant = await plantService.Get(Model.Sell.PlantId);
        Model.Deposit = await depositService.Get(Model.Sell.DepositId);
        OnPropertyChanged(nameof(Plant));
        OnPropertyChanged(nameof(Deposit));
        OnPropertyChanged(nameof(Date));
        OnPropertyChanged(nameof(Quantity));
        OnPropertyChanged(nameof(Price));
        OnPropertyChanged(nameof(Description));
    }

    #endregion

    public RelayCommand Delete => new RelayCommand(async execute => await DeleteSellAsync());
    private async Task DeleteSellAsync()
    {
        var result = new CustomMessageBoxYesNo("Czy na pewno chcesz usunąć tą sprzedaż?").ShowDialog();
        if (result == true)
        {
            await sellService.RevertRemainingQuantity(Model.Sell.HarvestQuantity);
            await sellService.Delete(Model.Sell.Id);
            Model.Sell.IsDeleted = true;
            await unitOfWork.SaveChangesAsync();
            RequestClose?.Invoke(Model.Sell);
        }
    }

    public RelayCommand Update => new RelayCommand(async execute => await UpdateSprayingAsync());
    private async Task UpdateSprayingAsync()
    {
        new CustomMessageBoxOk("Edycja zakupu jest obecnie niedostępna ze względu na złożoność związaną z zużywaniem zakupionego produktu.\nWszekie poprawki należy rozwiązywać dodawaniem kolejnych zakupów lub ich usuwaniem.").ShowDialog();
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
}
