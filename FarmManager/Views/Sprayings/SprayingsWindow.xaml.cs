using System.Windows;
using FarmManager.App.ViewModels.Sprayings;

namespace FarmManager.App.Views.Sprayings;

public partial class SprayingsWindow : Window
{
    public SprayingsWindow()
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((SprayingsViewModel)DataContext).InitializeAsync();
    }
}
