using System.Windows;
using FarmManager.App.ViewModels.ChooseEntity;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.ChooseEntity;

public partial class ChooseVendorWindow : Window
{
    public Vendor? Vendor { get; private set; }
    public ChooseVendorWindow()
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((ChooseVendorViewModel)DataContext).InitializeAsync();
        if (DataContext is ChooseVendorViewModel vm)
        {
            vm.RequestClose += vendor =>
            {
                Vendor = vendor;
                DialogResult = true;
            };
        }
    }
}
