using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.Buys;
using FarmManager.App.Views;
using FarmManager.App.Views.ChooseEntity;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;
using FarmManager.Services.Services;

namespace FarmManager.App.ViewModels.Buys;

public class BuyEditViewModel(IBuyService buyService, IVendorService vendorService, IFertilizerService fertilizerService) : BaseViewModel
{
    #region Properties

    public event Action<Buy>? RequestClose;
    public BuyEditModel Model = new BuyEditModel();

    public string? Fertilizer
    {
        get { return Model.Fertilizer.Name; }
        set
        {
            if (Model.Fertilizer != null)
                Model.Fertilizer.Name = value ?? string.Empty;
            OnPropertyChanged();
        }
    }
    public string? Vendor
    {
        get { return Model.Vendor.Name; }
        set
        {
            if (Model.Vendor != null)
                Model.Vendor.Name = value ?? string.Empty;
            OnPropertyChanged();
        }
    }
    public double Price
    {
        get { return Model.Buy.Price; }
        set
        {
            Model.Buy.Price = value;
            OnPropertyChanged();
        }
    }
    public double Quantity
    {
        get { return Model.Buy.Quantity; }
        set
        {
            Model.Buy.Quantity = value;
            OnPropertyChanged();
        }
    }
    public double RemainingQuantity
    {
        get { return Model.Buy.RemainingQuantity; }
        set
        {
            Model.Buy.RemainingQuantity = value;
            OnPropertyChanged();
        }
    }
    public string? Description
    {
        get
        {
            return Model.Fertilizer.Description;
        }
        set
        {
            Model.Fertilizer.Description = value;
            OnPropertyChanged();
        }
    }

    public async Task InitializeAsync(int id)
    {
        Model.Buy = await buyService.Get(id);
        Model.Vendor = await vendorService.Get(Model.Buy.VendorId);
        Model.Fertilizer = await fertilizerService.Get(Model.Buy.FertilizerId);
        OnPropertyChanged(nameof(Fertilizer));
        OnPropertyChanged(nameof(Vendor));
        OnPropertyChanged(nameof(Price));
        OnPropertyChanged(nameof(Quantity));
        OnPropertyChanged(nameof(RemainingQuantity));
        OnPropertyChanged(nameof(Description));
    }

    #endregion

    public RelayCommand Delete => new RelayCommand(async execute => await DeleteBuyAsync());
    private async Task DeleteBuyAsync()
    {
        var result = new CustomMessageBoxYesNo("Czy na pewno chcesz usunąć ten zakup?").ShowDialog();
        if (result == true)
        {
            await buyService.Delete(Model.Buy.Id);
            Model.Buy.IsDeleted = true;
            await fertilizerService.AdjustQuantity(Model.Fertilizer.Id, -Model.Buy.RemainingQuantity);
            RequestClose?.Invoke(Model.Buy);
        }
    }

    // The logic behind updating a buy is complex due to the need to adjust fertilizer quantity based on changes.
    // Possibly to uncomment that to implement at least editing description.

    public RelayCommand Update => new RelayCommand(async execute => await UpdateBuyAsync());
    private async Task UpdateBuyAsync()
    {
        new CustomMessageBoxOk("Edycja zakupu jest obecnie niedostępna ze względu na złożoność związaną z zużywaniem zakupionego produktu.\nWszekie poprawiki należy rozwiązywać dodawaniem kolejnych zakupów lub ich usuwaniem.").ShowDialog();
        //BuyValidator validator = new BuyValidator();
        //Model.Buy.Description = string.IsNullOrEmpty(Model.Buy.Description) ? null : Model.Buy.Description;
        //var result = validator.Validate(Model.Buy);
        //if (!result.IsValid)
        //{
        //    new CustomMessageBoxOk(result).ShowDialog();
        //}
        //else
        //{
        //    await buyService.Update(Model.Buy);
        //    RequestClose?.Invoke(Model.Buy);
        //}
    }

    public RelayCommand OpenFertilizer => new RelayCommand(execute => OpenSelectFertilizerAsync());
    private void OpenSelectFertilizerAsync()
    {
        var window = new ChooseFertilizerWindow();
        if (window.ShowDialog() == true && window.Fertilizer != null)
        {
            Model.Fertilizer = window.Fertilizer;
            Model.Buy.FertilizerId = window.Fertilizer.Id;
            OnPropertyChanged(nameof(Fertilizer));
        }
    }
    public RelayCommand OpenVendor => new RelayCommand(execute => OpenSelectVendorAsync());
    private void OpenSelectVendorAsync()
    {
        var window = new ChooseVendorWindow();
        if (window.ShowDialog() == true && window.Vendor != null)
        {
            Model.Vendor = window.Vendor;
            Model.Buy.VendorId = window.Vendor.Id;
            OnPropertyChanged(nameof(Vendor));
        }
    }
}
