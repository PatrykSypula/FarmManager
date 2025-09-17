using System.Windows;
using FarmManager.App.ViewModels.Seasons;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Seasons;

public partial class SeasonChoosePlantWindow : Window
{
    public Plant? Plant { get; private set; }
    public SeasonChoosePlantWindow()
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((SeasonChoosePlantViewModel)DataContext).InitializeAsync();
        if (DataContext is SeasonChoosePlantViewModel vm)
        {
            vm.RequestClose += plant =>
            {
                Plant = plant;
                DialogResult = true;
            };
        }
    }
}
