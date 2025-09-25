using System.Collections.ObjectModel;
using FarmManager.App.Helpers;
using FarmManager.App.Models.ChooseEntity;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.ChooseEntity;

public class ChooseVendorViewModel(IVendorService vendorService) : BaseViewModel
{
    #region Properties

    public event Action<Vendor>? RequestClose;

    public ChooseVendorModel Model = new ChooseVendorModel();

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
        Vendors = new ObservableCollection<Vendor>(await vendorService.GetAll());
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

    public RelayCommand Select => new RelayCommand(execute => SelectPlant());
    private void SelectPlant()
    {
        RequestClose?.Invoke(Model.SelectedItem);
    }
}
