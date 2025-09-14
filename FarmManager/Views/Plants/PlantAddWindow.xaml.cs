using System.Windows;
using FarmManager.App.ViewModels.Plants;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Plants;

public partial class PlantAddWindow : Window
{
    public Plant? Plant { get; private set; }
    public PlantAddWindow()
    {
        InitializeComponent();
        if (DataContext is PlantAddViewModel vm)
        {
            vm.RequestClose += plant =>
            {
                Plant = plant;
                DialogResult = true;
            };
        }
    }
}
