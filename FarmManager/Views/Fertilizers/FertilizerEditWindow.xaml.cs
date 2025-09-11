using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FarmManager.App.ViewModels.Fertilizers;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Fertilizers;

public partial class FertilizerEditWindow : Window
{
    public Fertilizer? Fertilizer { get; private set; }
    public FertilizerEditWindow(int id)
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((FertilizerEditViewModel)DataContext).InitializeAsync(id);
        if (DataContext is FertilizerEditViewModel vm)
        {
            vm.RequestClose += fertilizer =>
            {
                Fertilizer = fertilizer;
                DialogResult = true;
            };
        }
    }
}
