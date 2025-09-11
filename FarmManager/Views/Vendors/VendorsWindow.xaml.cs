using System.Windows;
using FarmManager.App.ViewModels.Vendors;

namespace FarmManager.App.Views.Vendors;

public partial class VendorsWindow : Window
{
    public VendorsWindow()
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((VendorsViewModel)DataContext).InitializeAsync();
    }
}
