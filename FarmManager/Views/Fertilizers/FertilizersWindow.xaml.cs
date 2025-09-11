using System.Windows;
using FarmManager.App.ViewModels.Fertilizers;

namespace FarmManager.App.Views.Fertilizers;

public partial class FertilizersWindow : Window
{
    public FertilizersWindow()
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((FertilizersViewModel)DataContext).InitializeAsync();
    }
}
