using System;
using System.Windows;
using FarmManager.App.ViewModels.Actions;
using FarmManager.Model.Model;
using Action = FarmManager.Model.Model.Action;

namespace FarmManager.App.Views.Actions;

public partial class ActionAddWindow : Window
{
    public Action? Action { get; private set; }
    public ActionAddWindow()
    {
        InitializeComponent();
        if (DataContext is ActionAddViewModel vm)
        {
            vm.RequestClose += action =>
            {
                Action = action;
                DialogResult = true;
            };
        }
    }
}
