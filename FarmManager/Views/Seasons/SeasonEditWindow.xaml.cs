using System.Windows;
using FarmManager.App.ViewModels.Seasons;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Seasons;

public partial class SeasonEditWindow : Window
{
    public Season? Season { get; private set; }
    public SeasonEditWindow(int id)
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((SeasonEditViewModel)DataContext).InitializeAsync(id);
        if (DataContext is SeasonEditViewModel vm)
        {
            vm.RequestClose += season =>
            {
                Season = season;
                DialogResult = true;
            };
        }
    }
}
