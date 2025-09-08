using FarmManager.App.ViewModels;
using FarmManager.App.ViewModels.Deposits;
using FarmManager.App.ViewModels.Diseases;
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

}
