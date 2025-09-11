using System.Windows;
using FarmManager.App.ViewModels.Diseases;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Diseases;

public partial class DiseaseAddWindow : Window
{
    public Disease? Disease { get; private set; }
    public DiseaseAddWindow()
    {
        InitializeComponent();
        if (DataContext is DiseaseAddViewModel vm)
        {
            vm.RequestClose += disease =>
            {
                Disease = disease;
                DialogResult = true;
            };
        }
    }
}
