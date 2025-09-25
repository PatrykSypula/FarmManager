using FarmManager.App.ViewModels;
using FarmManager.App.ViewModels.Buys;
using FarmManager.App.ViewModels.ChooseEntity;
using FarmManager.App.ViewModels.Deposits;
using FarmManager.App.ViewModels.Diseases;
using FarmManager.App.ViewModels.EmployeeCosts;
using FarmManager.App.ViewModels.Employees;
using FarmManager.App.ViewModels.Fertilizers;
using FarmManager.App.ViewModels.Plants;
using FarmManager.App.ViewModels.Seasons;
using FarmManager.App.ViewModels.Sprayings;
using FarmManager.App.ViewModels.Varieties;
using FarmManager.App.ViewModels.Vendors;
using FarmManager.App.ViewModels.Workdays;
using Microsoft.Extensions.DependencyInjection;

namespace FarmManager.App.Helpers;

public class ViewModelLocator
{
    public MainWindowViewModel MainWindowViewModel =>
        MyApp.ServiceProvider.GetRequiredService<MainWindowViewModel>();

    //Choose Entity
    public ChooseVarietyViewModel ChooseVarietyViewModel =>
        MyApp.ServiceProvider.GetRequiredService<ChooseVarietyViewModel>();
    public ChooseVendorViewModel ChooseVendorViewModel =>
        MyApp.ServiceProvider.GetRequiredService<ChooseVendorViewModel>();
    public ChooseFertilizerViewModel ChooseFertilizerViewModel =>
        MyApp.ServiceProvider.GetRequiredService<ChooseFertilizerViewModel>();
    public ChoosePlantViewModel ChoosePlantViewModel =>
        MyApp.ServiceProvider.GetRequiredService<ChoosePlantViewModel>();
    public ChooseEmployeeViewModel ChooseEmployeeViewModel =>
        MyApp.ServiceProvider.GetRequiredService<ChooseEmployeeViewModel>();

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

    // Varieties
    public VarietiesViewModel VarietiesViewModel =>
        MyApp.ServiceProvider.GetRequiredService<VarietiesViewModel>();
    public VarietyAddViewModel VarietyAddViewModel =>
        MyApp.ServiceProvider.GetRequiredService<VarietyAddViewModel>();
    public VarietyEditViewModel VarietyEditViewModel =>
        MyApp.ServiceProvider.GetRequiredService<VarietyEditViewModel>();

    // Vendors
    public VendorsViewModel VendorsViewModel =>
        MyApp.ServiceProvider.GetRequiredService<VendorsViewModel>();
    public VendorAddViewModel VendorAddViewModel =>
        MyApp.ServiceProvider.GetRequiredService<VendorAddViewModel>();
    public VendorEditViewModel VendorEditViewModel =>
        MyApp.ServiceProvider.GetRequiredService<VendorEditViewModel>();

    // Plants
    public PlantsViewModel PlantsViewModel =>
        MyApp.ServiceProvider.GetRequiredService<PlantsViewModel>();
    public PlantAddViewModel PlantAddViewModel =>
        MyApp.ServiceProvider.GetRequiredService<PlantAddViewModel>();
    public PlantEditViewModel PlantEditViewModel =>
        MyApp.ServiceProvider.GetRequiredService<PlantEditViewModel>();
    

    // Seasons
    public SeasonsViewModel SeasonsViewModel =>
        MyApp.ServiceProvider.GetRequiredService<SeasonsViewModel>();
    public SeasonAddViewModel SeasonAddViewModel =>
        MyApp.ServiceProvider.GetRequiredService<SeasonAddViewModel>();
    public SeasonEditViewModel SeasonEditViewModel =>
        MyApp.ServiceProvider.GetRequiredService<SeasonEditViewModel>();

    // Buys
    public BuyAddViewModel BuyAddViewModel =>
        MyApp.ServiceProvider.GetRequiredService<BuyAddViewModel>();
    public BuyEditViewModel BuyEditViewModel =>
        MyApp.ServiceProvider.GetRequiredService<BuyEditViewModel>();
    public BuysViewModel BuysViewModel =>
        MyApp.ServiceProvider.GetRequiredService<BuysViewModel>();

    // Sprayings
    public SprayingsViewModel SprayingsViewModel =>
        MyApp.ServiceProvider.GetRequiredService<SprayingsViewModel>();
    public SprayingAddViewModel SprayingAddViewModel =>
        MyApp.ServiceProvider.GetRequiredService<SprayingAddViewModel>();
    public SprayingEditViewModel SprayingEditViewModel =>
        MyApp.ServiceProvider.GetRequiredService<SprayingEditViewModel>();

    //EmployeeCosts
    public EmployeeCostsViewModel EmployeeCostsViewModel =>
        MyApp.ServiceProvider.GetRequiredService<EmployeeCostsViewModel>();
    public EmployeeCostAddViewModel EmployeeCostAddViewModel =>
        MyApp.ServiceProvider.GetRequiredService<EmployeeCostAddViewModel>();
    public EmployeeCostEditViewModel EmployeeCostEditViewModel =>
        MyApp.ServiceProvider.GetRequiredService<EmployeeCostEditViewModel>();

    //Workdays
    public WorkdaysViewModel WorkdaysViewModel =>
        MyApp.ServiceProvider.GetRequiredService<WorkdaysViewModel>();
}
