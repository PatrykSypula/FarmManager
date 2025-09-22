using FarmManager.App.Helpers;
using FarmManager.App.Helpers.Validators;
using FarmManager.App.Models.Deposits;
using FarmManager.App.Models.Vendors;
using FarmManager.App.Views;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;
using FarmManager.Services.Validators;

namespace FarmManager.App.ViewModels.Vendors;

public class VendorEditViewModel(IVendorService vendorService, IUnitOfWork unitOfWork) : BaseViewModel
{
    #region Properties

    public event Action<Vendor>? RequestClose;
    public VendorEditModel Model = new VendorEditModel();

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

    public bool IsActive
    {
        get
        {
            return Model.Vendor.IsActive;
        }
        set
        {
            Model.Vendor.IsActive = value;
            OnPropertyChanged();
        }
    }
    public async Task InitializeAsync(int id)
    {
        Model.Vendor = await vendorService.Get(id);
        OnPropertyChanged(nameof(Name));
        OnPropertyChanged(nameof(PhoneNumber));
        OnPropertyChanged(nameof(Email));
        OnPropertyChanged(nameof(Description));
        OnPropertyChanged(nameof(IsActive));
    }

    #endregion

    public RelayCommand Delete => new RelayCommand(async execute => await DeleteVendorAsync());
    private async Task DeleteVendorAsync()
    {
        var result = new CustomMessageBoxYesNo("Czy na pewno chcesz usunąć tego sprzedawcę?").ShowDialog();
        if (result == true)
        {
            await vendorService.Delete(Model.Vendor.Id);
            Model.Vendor.IsDeleted = true;
            await unitOfWork.SaveChangesAsync();
            RequestClose?.Invoke(Model.Vendor);
        }
    }

    public RelayCommand Update => new RelayCommand(async execute => await UpdateVendorAsync());
    private async Task UpdateVendorAsync()
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
            await vendorService.Update(Model.Vendor);
            await unitOfWork.SaveChangesAsync();
            RequestClose?.Invoke(Model.Vendor);
        }
    }
}
