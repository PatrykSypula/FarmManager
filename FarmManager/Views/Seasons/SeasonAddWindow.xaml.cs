using System.Windows;
using FarmManager.App.ViewModels.Seasons;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Seasons;

public partial class SeasonAddWindow : Window
{
    public Season? Season { get; private set; }
    public SeasonAddWindow()
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((SeasonAddViewModel)DataContext).InitializeAsync();
        if (DataContext is SeasonAddViewModel vm)
        {
            vm.RequestClose += season =>
            {
                Season = season;
                DialogResult = true;
            };
        }
    }
}
