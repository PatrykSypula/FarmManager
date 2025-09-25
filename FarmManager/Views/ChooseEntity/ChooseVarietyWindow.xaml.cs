using System.Windows;
using FarmManager.App.ViewModels.ChooseEntity;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.ChooseEntity;

public partial class ChooseVarietyWindow : Window
{
    public Variety? Variety { get; private set; }
    public ChooseVarietyWindow()
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((ChooseVarietyViewModel)DataContext).InitializeAsync();
        if (DataContext is ChooseVarietyViewModel vm)
        {
            vm.RequestClose += variery =>
            {
                Variety = variery;
                DialogResult = true;
            };
        }
    }
}
