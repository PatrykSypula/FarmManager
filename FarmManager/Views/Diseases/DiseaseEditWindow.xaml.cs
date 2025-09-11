using System.Windows;
using FarmManager.App.ViewModels.Diseases;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Diseases;

public partial class DiseaseEditWindow : Window
{
    public Disease? Disease { get; private set; }
    public DiseaseEditWindow(int id)
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((DiseaseEditViewModel)DataContext).InitializeAsync(id);
        if (DataContext is DiseaseEditViewModel vm)
        {
            vm.RequestClose += disease =>
            {
                Disease = disease;
                DialogResult = true;
            };
        }
    }
}
