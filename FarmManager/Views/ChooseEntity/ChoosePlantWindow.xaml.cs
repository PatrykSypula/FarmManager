using System.Windows;
using FarmManager.App.ViewModels.ChooseEntity;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.ChooseEntity;

public partial class ChoosePlantWindow : Window
{
    public Plant? Plant { get; private set; }
    public ChoosePlantWindow()
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((ChoosePlantViewModel)DataContext).InitializeAsync();
        if (DataContext is ChoosePlantViewModel vm)
        {
            vm.RequestClose += plant =>
            {
                Plant = plant;
                DialogResult = true;
            };
        }
    }
}
