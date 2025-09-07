using System;
using System.Windows;
using FarmManager.App.ViewModels.Deposits;

namespace FarmManager.App.Views.Deposits;

public partial class DepositsWindow : Window
{
    public DepositsWindow()
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((DepositsViewModel)DataContext).InitializeAsync();
    }
}
