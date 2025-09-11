using System.Windows;
using FarmManager.App.ViewModels.Vendors;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Vendors;

public partial class VendorEditWindow : Window
{
    public Vendor? Vendor { get; private set; }
    public VendorEditWindow(int id)
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((VendorEditViewModel)DataContext).InitializeAsync(id);
        if (DataContext is VendorEditViewModel vm)
        {
            vm.RequestClose += vendor =>
            {
                Vendor = vendor;
                DialogResult = true;
            };
        }
    }
}
