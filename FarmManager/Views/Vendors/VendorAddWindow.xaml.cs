using System.Windows;
using FarmManager.App.ViewModels.Vendors;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Vendors;

public partial class VendorAddWindow : Window
{
    public Vendor? Vendor { get; private set; }
    public VendorAddWindow()
    {
        InitializeComponent();
        if (DataContext is VendorAddViewModel vm)
        {
            vm.RequestClose += vendor =>
            {
                Vendor = vendor;
                DialogResult = true;
            };
        }
    }
}
