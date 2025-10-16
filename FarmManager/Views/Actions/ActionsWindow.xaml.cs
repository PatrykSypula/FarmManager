using System.Windows;
using FarmManager.App.ViewModels.Actions;
using Action = FarmManager.Model.Model.Action;

namespace FarmManager.App.Views.Actions;

public partial class ActionsWindow : Window
{
    public ActionsWindow()
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((ActionsViewModel)DataContext).InitializeAsync();
    }
}
