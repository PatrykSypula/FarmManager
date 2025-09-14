using FarmManager.App.Helpers;
using FarmManager.App.Models;
using FarmManager.App.Views.Deposits;
using FarmManager.App.Views.Diseases;
using FarmManager.App.Views.Employees;
using FarmManager.App.Views.Fertilizers;
using FarmManager.App.Views.Plants;
using FarmManager.App.Views.Varieties;
using FarmManager.App.Views.Vendors;

namespace FarmManager.App.ViewModels;

public class MainWindowViewModel : BaseViewModel
{
    private MainWindowModel model = new MainWindowModel();
    public string Title
    {
        get
        {
            return model.Title;
        }
        set
        {
            model.Title = value;
            OnPropertyChanged();
        }
    }
    public RelayCommand Deposits => new RelayCommand(execute => OpenDepositsWindow());
    private void OpenDepositsWindow()
    {
        new DepositsWindow().ShowDialog();
    }

    public RelayCommand Diseases => new RelayCommand(execute => OpenDiseasesWindow());
    private void OpenDiseasesWindow()
    {
        new DiseasesWindow().ShowDialog();
    }

    public RelayCommand Employees => new RelayCommand(execute => OpenEmployeesWindow());
    private void OpenEmployeesWindow()
    {
        new EmployeesWindow().ShowDialog();
    }

    public RelayCommand Fertilizers => new RelayCommand(execute => OpenFertilizersWindow());
    private void OpenFertilizersWindow()
    {
        new FertilizersWindow().ShowDialog();
    }

    public RelayCommand Varieties => new RelayCommand(execute => OpenVarietiesWindow());
    private void OpenVarietiesWindow()
    {
        new VarietiesWindow().ShowDialog();
    }

    public RelayCommand Vendors => new RelayCommand(execute => OpenVendorsWindow());
    private void OpenVendorsWindow()
    {
        new VendorsWindow().ShowDialog();
    }

    public RelayCommand Plants => new RelayCommand(execute => OpenPlantsWindow());
    private void OpenPlantsWindow()
    {
        new PlantsWindow().ShowDialog();
    }
}
