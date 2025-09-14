using System.Collections.ObjectModel;
using FarmManager.App.Helpers;
using FarmManager.App.Models.Deposits;
using FarmManager.App.Models.Vendors;
using FarmManager.App.Views.Deposits;
using FarmManager.App.Views.Vendors;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Vendors;

public class VendorsViewModel(IVendorService vendorService) : BaseViewModel
{
    #region Properties

    public VendorsModel Model = new VendorsModel();

    public ObservableCollection<Vendor> Vendors
    {
        get { return Model.Vendors; }
        set
        {
            Model.Vendors = value;
            OnPropertyChanged();
        }
    }

    public async Task InitializeAsync()
    {
        Vendors = new ObservableCollection<Vendor>(await vendorService.GetAll(false));
    }

    public Vendor SelectedItem
    {
        get { return Model.SelectedItem; }
        set
        {
            Model.SelectedItem = value;
            OnPropertyChanged();
        }
    }

    #endregion

    public RelayCommand Create => new RelayCommand(execute => OpenVendorAddWindow());
    private void OpenVendorAddWindow()
    {
        var window = new VendorAddWindow();
        if (window.ShowDialog() == true && window.Vendor != null)
        {
            Vendors.Add(window.Vendor);
            OnPropertyChanged(nameof(Vendors));
        }
    }

    public RelayCommand Edit => new RelayCommand(execute => OpenVendorEditWindow());
    private void OpenVendorEditWindow()
    {
        var window = new VendorEditWindow(SelectedItem.Id);
        if (window.ShowDialog() == true && window.Vendor != null)
        {
            var vendor = window.Vendor;
            var index = Vendors.ToList().FindIndex(d => d.Id == vendor.Id);

            if (index >= 0)
            {
                if (vendor.IsDeleted)
                {
                    Vendors.RemoveAt(index);
                }
                else
                {
                    Vendors.RemoveAt(index);
                    Vendors.Insert(index, vendor);
                }
            }
            OnPropertyChanged(nameof(Vendors));
        }
    }
}
