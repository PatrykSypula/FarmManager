using System.Windows;
using FarmManager.App.ViewModels.Varieties;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Varieties;
public partial class VarietyEditWindow : Window
{
    public Variety? Variety { get; private set; }
    public VarietyEditWindow(int id)
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((VarietyEditViewModel)DataContext).InitializeAsync(id);
        if (DataContext is VarietyEditViewModel vm)
        {
            vm.RequestClose += variety =>
            {
                Variety = variety;
                DialogResult = true;
            };
        }
    }
}
