using System.Windows;
using FarmManager.App.ViewModels.Plants;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Plants;

public partial class PlantEditWindow : Window
{
    public Plant? Plant { get; private set; }
    public PlantEditWindow(int id)
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((PlantEditViewModel)DataContext).InitializeAsync(id);
        if (DataContext is PlantEditViewModel vm)
        {
            vm.RequestClose += plant =>
            {
                Plant = plant;
                DialogResult = true;
            };
        }
    }
}
