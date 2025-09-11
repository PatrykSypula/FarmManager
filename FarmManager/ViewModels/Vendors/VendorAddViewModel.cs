using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.Vendors;
using FarmManager.App.Views;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Vendors;

public class VendorAddViewModel(IVendorService vendorService) : BaseViewModel
{
    #region Properties

    public event Action<Vendor>? RequestClose;
    public VendorAddModel Model = new VendorAddModel();

    public string Name
    {
        get
        {
            return Model.Vendor.Name;
        }
        set
        {
            Model.Vendor.Name = value;
            OnPropertyChanged();
        }
    }

    public string? PhoneNumber
    {
        get
        {
            return Model.Vendor.PhoneNumber;
        }
        set
        {
            Model.Vendor.PhoneNumber = value;
            OnPropertyChanged();
        }
    }
    public string? Email
    {
        get
        {
            return Model.Vendor.Email;
        }
        set
        {
            Model.Vendor.Email = value;
            OnPropertyChanged();
        }
    }
    public string? Description
    {
        get
        {
            return Model.Vendor.Description;
        }
        set
        {
            Model.Vendor.Description = value;
            OnPropertyChanged();
        }
    }

    #endregion

    public RelayCommand Add => new RelayCommand(async execute => await AddVendorAsync());

    private async Task AddVendorAsync()
    {
        VendorValidator validator = new VendorValidator();
        Model.Vendor.PhoneNumber = string.IsNullOrEmpty(Model.Vendor.PhoneNumber) ? null : Model.Vendor.PhoneNumber;
        Model.Vendor.Email = string.IsNullOrEmpty(Model.Vendor.Email) ? null : Model.Vendor.Email;
        Model.Vendor.Description = string.IsNullOrEmpty(Model.Vendor.Description) ? null : Model.Vendor.Description;
        var result = validator.Validate(Model.Vendor);
        if (!result.IsValid)
        {
            new CustomMessageBoxOk(result).ShowDialog();
        }
        else
        {
            await vendorService.Add(Model.Vendor);
            RequestClose?.Invoke(Model.Vendor);

        }
    }
}
