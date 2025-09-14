using System.Windows;
using FarmManager.App.ViewModels.Varieties;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Varieties;

public partial class VarietyAddWindow : Window
{
    public Variety? Variety { get; private set; }
    public VarietyAddWindow()
    {
        InitializeComponent();
        if (DataContext is VarietyAddViewModel vm)
        {
            vm.RequestClose += variety =>
            {
                Variety = variety;
                DialogResult = true;
            };
        }
    }
}
