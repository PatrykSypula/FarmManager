using System.Windows;
using FarmManager.App.ViewModels.Plants;

namespace FarmManager.App.Views.Plants;

public partial class PlantsWindow : Window
{
    public PlantsWindow()
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((PlantsViewModel)DataContext).InitializeAsync();
    }
}
