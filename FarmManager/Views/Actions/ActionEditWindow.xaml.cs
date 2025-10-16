using System.Windows;
using FarmManager.App.ViewModels.Actions;
using Action = FarmManager.Model.Model.Action;

namespace FarmManager.App.Views.Actions;

public partial class ActionEditWindow : Window
{
    public Action? Action { get; private set; }
    public ActionEditWindow(int id)
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((ActionEditViewModel)DataContext).InitializeAsync(id);
        if (DataContext is ActionEditViewModel vm)
        {
            vm.RequestClose += action =>
            {
                Action = action;
                DialogResult = true;
            };
        }
    }
}
