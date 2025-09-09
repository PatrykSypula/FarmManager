using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using FarmManager.App.ViewModels.Fertilizers;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Fertilizers;

public partial class FertilizerAddWindow : Window
{
    public Fertilizer? Fertilizer { get; private set; }
    public FertilizerAddWindow()
    {
        InitializeComponent();
        if (DataContext is FertilizerAddViewModel vm)
        {
            vm.RequestClose += fertilizer =>
            {
                Fertilizer = fertilizer;
                DialogResult = true;
            };
        }
    }
}
