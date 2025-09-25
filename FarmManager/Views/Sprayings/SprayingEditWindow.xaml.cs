using System.Windows;
using FarmManager.App.ViewModels.Sprayings;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Sprayings;

public partial class SprayingEditWindow : Window
{
    public Spraying? Spraying { get; private set; }
    public SprayingEditWindow(int id)
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((SprayingEditViewModel)DataContext).InitializeAsync(id);
        if (DataContext is SprayingEditViewModel vm)
        {
            vm.RequestClose += spraying =>
            {
                Spraying = spraying;
                DialogResult = true;
            };
        }
    }
}
