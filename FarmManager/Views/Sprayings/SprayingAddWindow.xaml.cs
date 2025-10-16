using System.Windows;
using FarmManager.App.ViewModels.Sells;
using FarmManager.App.ViewModels.Sprayings;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Sprayings;

public partial class SprayingAddWindow : Window
{
    public Spraying? Spraying { get; private set; }
    public SprayingAddWindow()
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((SprayingAddViewModel)DataContext).InitializeAsync();
        if (DataContext is SprayingAddViewModel vm)
        {
            vm.RequestClose += spraying =>
            {
                Spraying = spraying;
                DialogResult = true;
            };
        }
    }
}
