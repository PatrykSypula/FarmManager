using FarmManager.App.ViewModels;
using FarmManager.App.ViewModels.Actions;
using FarmManager.App.ViewModels.Buys;
using FarmManager.App.ViewModels.ChooseEntity;
using FarmManager.App.ViewModels.Deposits;
using FarmManager.App.ViewModels.EmployeeCosts;
using FarmManager.App.ViewModels.Employees;
using FarmManager.App.ViewModels.Fertilizers;
using FarmManager.App.ViewModels.Payments;
using FarmManager.App.ViewModels.Plants;
using FarmManager.App.ViewModels.Seasons;
using FarmManager.App.ViewModels.Sells;
using FarmManager.App.ViewModels.Sprayings;
using FarmManager.App.ViewModels.Vendors;
using FarmManager.App.ViewModels.Workdays;
using FarmManager.App.ViewModels.Workdays.WorkdaysCollecting;
using FarmManager.App.ViewModels.Workdays.WorkdaysHourly;
using Microsoft.Extensions.DependencyInjection;

namespace FarmManager.App.Helpers;

public class ViewModelLocator
{
    public MainWindowViewModel MainWindowViewModel =>
        MyApp.ServiceProvider.GetRequiredService<MainWindowViewModel>();

    //Choose Entity
    public ChooseVendorViewModel ChooseVendorViewModel =>
        MyApp.ServiceProvider.GetRequiredService<ChooseVendorViewModel>();
    public ChooseFertilizerViewModel ChooseFertilizerViewModel =>
        MyApp.ServiceProvider.GetRequiredService<ChooseFertilizerViewModel>();
    public ChoosePlantViewModel ChoosePlantViewModel =>
        MyApp.ServiceProvider.GetRequiredService<ChoosePlantViewModel>();
    public ChooseEmployeeViewModel ChooseEmployeeViewModel =>
        MyApp.ServiceProvider.GetRequiredService<ChooseEmployeeViewModel>();
    public ChooseActionViewModel ChooseActionViewModel =>
        MyApp.ServiceProvider.GetRequiredService<ChooseActionViewModel>();
    public ChooseDepositViewModel ChooseDepositViewModel =>
        MyApp.ServiceProvider.GetRequiredService<ChooseDepositViewModel>();

    // Deposits
    public DepositsViewModel DepositsViewModel =>
        MyApp.ServiceProvider.GetRequiredService<DepositsViewModel>();
    public DepositAddViewModel DepositAddViewModel =>
        MyApp.ServiceProvider.GetRequiredService<DepositAddViewModel>();
    public DepositEditViewModel DepositEditViewModel =>
        MyApp.ServiceProvider.GetRequiredService<DepositEditViewModel>();


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
    public WorkdaysCalendarViewModel WorkdaysCalendarViewModel =>
        MyApp.ServiceProvider.GetRequiredService<WorkdaysCalendarViewModel>();
    public WorkdaysViewModel WorkdaysViewModel =>
        MyApp.ServiceProvider.GetRequiredService<WorkdaysViewModel>();
    public WorkdayCollectingAddOneViewModel WorkdayCollectingAddOneViewModel =>
        MyApp.ServiceProvider.GetRequiredService<WorkdayCollectingAddOneViewModel>();
    public WorkdayCollectingAddAllViewModel WorkdayCollectingAddAllViewModel =>
        MyApp.ServiceProvider.GetRequiredService<WorkdayCollectingAddAllViewModel>();
    public WorkdayCollectingEditViewModel WorkdayCollectingEditViewModel =>
        MyApp.ServiceProvider.GetRequiredService<WorkdayCollectingEditViewModel>();
    public WorkdayHourlyAddAllViewModel WorkdayHourlyAddAllViewModel =>
        MyApp.ServiceProvider.GetRequiredService<WorkdayHourlyAddAllViewModel>();
    public WorkdayHourlyAddOneViewModel WorkdayHourlyAddOneViewModel =>
        MyApp.ServiceProvider.GetRequiredService<WorkdayHourlyAddOneViewModel>();
    public WorkdayHourlyEditViewModel WorkdayHourlyEditViewModel =>
       MyApp.ServiceProvider.GetRequiredService<WorkdayHourlyEditViewModel>();
    public WorkdaysSelectTypeViewModel WorkdaysSelectTypeViewModel =>
        MyApp.ServiceProvider.GetRequiredService<WorkdaysSelectTypeViewModel>();
    public WorkdayHarvestCollectingAddViewModel WorkdayHarvestCollectingAddViewModel =>
        MyApp.ServiceProvider.GetRequiredService<WorkdayHarvestCollectingAddViewModel>();
    public WorkdayHarvestCollectingEditViewModel WorkdayHarvestCollectingEditViewModel =>
        MyApp.ServiceProvider.GetRequiredService<WorkdayHarvestCollectingEditViewModel>();
    public WorkdayHarvestHourlyAddViewModel WorkdayHarvestHourlyAddViewModel =>
        MyApp.ServiceProvider.GetRequiredService<WorkdayHarvestHourlyAddViewModel>();
    public WorkdayHarvestHourlyEditViewModel WorkdayHarvestHourlyEditViewModel =>
        MyApp.ServiceProvider.GetRequiredService<WorkdayHarvestHourlyEditViewModel>();
    public WorkdayHourlyWorkAddViewModel WorkdayHourlyWorkAddViewModel =>
        MyApp.ServiceProvider.GetRequiredService<WorkdayHourlyWorkAddViewModel>();
    public WorkdayHourlyWorkEditViewModel WorkdayHourlyWorkEditViewModel =>
        MyApp.ServiceProvider.GetRequiredService<WorkdayHourlyWorkEditViewModel>();

    //Actions
    public ActionsViewModel ActionsViewModel =>
        MyApp.ServiceProvider.GetRequiredService<ActionsViewModel>();
    public ActionAddViewModel ActionAddViewModel =>
        MyApp.ServiceProvider.GetRequiredService<ActionAddViewModel>();
    public ActionEditViewModel ActionEditViewModel => 
        MyApp.ServiceProvider.GetRequiredService<ActionEditViewModel>();

    //Sells
    public SellsViewModel SellsViewModel =>
        MyApp.ServiceProvider.GetRequiredService<SellsViewModel>();
    public SellAddViewModel SellAddViewModel =>
        MyApp.ServiceProvider.GetRequiredService<SellAddViewModel>();
    public SellEditViewModel SellEditViewModel =>
        MyApp.ServiceProvider.GetRequiredService<SellEditViewModel>();

    //Payments
    public PaymentsViewModel PaymentsViewModel =>
        MyApp.ServiceProvider.GetRequiredService<PaymentsViewModel>();
    public PaymentAddViewModel PaymentAddViewModel =>
        MyApp.ServiceProvider.GetRequiredService<PaymentAddViewModel>();
    public PaymentEditViewModel PaymentEditViewModel =>
        MyApp.ServiceProvider.GetRequiredService<PaymentEditViewModel>();


}
