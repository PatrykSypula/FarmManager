using FarmManager.App.ViewModels;
using FarmManager.App.ViewModels.Deposits;
using FarmManager.App.ViewModels.Diseases;
using FarmManager.App.ViewModels.Employees;
using FarmManager.App.ViewModels.Fertilizers;
using Microsoft.Extensions.DependencyInjection;

namespace FarmManager.App.Helpers;

public class ViewModelLocator
{
    public MainWindowViewModel MainWindowViewModel =>
        MyApp.ServiceProvider.GetRequiredService<MainWindowViewModel>();

    // Deposits
    public DepositsViewModel DepositsViewModel =>
        MyApp.ServiceProvider.GetRequiredService<DepositsViewModel>();
    public DepositAddViewModel DepositAddViewModel =>
        MyApp.ServiceProvider.GetRequiredService<DepositAddViewModel>();
    public DepositEditViewModel DepositEditViewModel =>
        MyApp.ServiceProvider.GetRequiredService<DepositEditViewModel>();

    // Diseases
    public DiseasesViewModel DiseasesViewModel =>
        MyApp.ServiceProvider.GetRequiredService<DiseasesViewModel>();
    public DiseaseAddViewModel DiseaseAddViewModel =>
        MyApp.ServiceProvider.GetRequiredService<DiseaseAddViewModel>();
    public DiseaseEditViewModel DiseaseEditViewModel =>
        MyApp.ServiceProvider.GetRequiredService<DiseaseEditViewModel>();

    // Employees
    public EmployeesViewModel EmployeesViewModel =>
        MyApp.ServiceProvider.GetRequiredService<EmployeesViewModel>();
    public EmployeeAddViewModel EmployeeAddViewModel =>
        MyApp.ServiceProvider.GetRequiredService<EmployeeAddViewModel>();
    public EmployeeEditViewModel EmployeeEditViewModel =>
        MyApp.ServiceProvider.GetRequiredService<EmployeeEditViewModel>();

    // Fertilizers

    public FertilizersViewModel FertilizersViewModel =>
        MyApp.ServiceProvider.GetRequiredService<FertilizersViewModel>();
    public FertilizerAddViewModel FertilizerAddViewModel =>
        MyApp.ServiceProvider.GetRequiredService<FertilizerAddViewModel>();
    public FertilizerEditViewModel FertilizerEditViewModel =>
        MyApp.ServiceProvider.GetRequiredService<FertilizerEditViewModel>();

}
