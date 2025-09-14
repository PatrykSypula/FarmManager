using System.Windows;
using FarmManager.App.ViewModels.Plants;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Plants;

public partial class PlantChooseVarietyWindow : Window
{
    public Variety? Variety { get; private set; }
    public PlantChooseVarietyWindow()
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((PlantChooseVarietyViewModel)DataContext).InitializeAsync();
        if (DataContext is PlantChooseVarietyViewModel vm)
        {
            vm.RequestClose += variery =>
            {
                Variety = variery;
                DialogResult = true;
            };
        }
    }
}
