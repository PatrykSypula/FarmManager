using System.Windows;
using FarmManager.App.ViewModels.Diseases;

namespace FarmManager.App.Views.Diseases;

public partial class DiseasesWindow : Window
{
    public DiseasesWindow()
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((DiseasesViewModel)DataContext).InitializeAsync();
    }
}
