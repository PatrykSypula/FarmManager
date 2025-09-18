using System.Windows;
using FarmManager.App.ViewModels.ChooseEntity;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.ChooseEntity;

public partial class ChooseFertilizerWindow : Window
{
    public Fertilizer? Fertilizer { get; private set; }
    public ChooseFertilizerWindow()
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((ChooseFertilizerViewModel)DataContext).InitializeAsync();
        if (DataContext is ChooseFertilizerViewModel vm)
        {
            vm.RequestClose += fertilizer =>
            {
                Fertilizer = fertilizer;
                DialogResult = true;
            };
        }
    }
}
