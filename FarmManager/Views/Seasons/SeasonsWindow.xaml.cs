using System.Windows;
using FarmManager.App.ViewModels.Seasons;

namespace FarmManager.App.Views.Seasons;

public partial class SeasonsWindow : Window
{
    public SeasonsWindow()
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((SeasonsViewModel)DataContext).InitializeAsync();
    }
}
