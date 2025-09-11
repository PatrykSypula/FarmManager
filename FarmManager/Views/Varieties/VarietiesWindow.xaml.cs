using System.Windows;
using FarmManager.App.ViewModels.Varieties;

namespace FarmManager.App.Views.Varieties;

public partial class VarietiesWindow : Window
{
    public VarietiesWindow()
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((VarietiesViewModel)DataContext).InitializeAsync();
    }
}
