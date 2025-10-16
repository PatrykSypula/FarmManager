using System.Windows;
using FarmManager.App.ViewModels.ChooseEntity;
using Action = FarmManager.Model.Model.Action;

namespace FarmManager.App.Views.ChooseEntity;

public partial class ChooseActionWindow : Window
{
    public Action? Action { get; private set; }
    public ChooseActionWindow()
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((ChooseActionViewModel)DataContext).InitializeAsync();
        if (DataContext is ChooseActionViewModel vm)
        {
            vm.RequestClose += action =>
            {
                Action = action;
                DialogResult = true;
            };
        }
    }
}
