using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.Buys;
using FarmManager.App.Views;
using FarmManager.App.Views.ChooseEntity;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Buys;

public class BuyAddViewModel(IBuyService buyService, IFertilizerService fertilizerService) : BaseViewModel
{
    #region Properties

    public event Action<Buy>? RequestClose;
    public BuyAddModel Model = new BuyAddModel();

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

    #endregion

    public RelayCommand Add => new RelayCommand(async execute => await AddBuyAsync());

    private async Task AddBuyAsync()
    {
        BuyValidator validator = new BuyValidator();
        Model.Buy.Description = string.IsNullOrEmpty(Model.Buy.Description) ? null : Model.Buy.Description;
        var result = validator.Validate(Model.Buy);
        if (!result.IsValid)
        {
            new CustomMessageBoxOk(result).ShowDialog();
        }
        else
        {
            Model.Buy.RemainingQuantity = Model.Buy.Quantity;
            await buyService.Add(Model.Buy);
            await fertilizerService.AdjustQuantity(Model.Fertilizer.Id, Model.Buy.Quantity);
            RequestClose?.Invoke(Model.Buy);
        }
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
