using System.Windows;
using FarmManager.App.ViewModels;
using FarmManager.App.ViewModels.Buys;
using FarmManager.App.ViewModels.ChooseEntity;
using FarmManager.App.ViewModels.Deposits;
using FarmManager.App.ViewModels.Diseases;
using FarmManager.App.ViewModels.Employees;
using FarmManager.App.ViewModels.Fertilizers;
using FarmManager.App.ViewModels.Plants;
using FarmManager.App.ViewModels.Seasons;
using FarmManager.App.ViewModels.Varieties;
using FarmManager.App.ViewModels.Vendors;
using FarmManager.App.Views.Buys;
using FarmManager.App.Views.ChooseEntity;
using FarmManager.App.Views.Deposits;
using FarmManager.App.Views.Diseases;
using FarmManager.App.Views.Employees;
using FarmManager.App.Views.Fertilizers;
using FarmManager.App.Views.Plants;
using FarmManager.App.Views.Seasons;
using FarmManager.App.Views.Varieties;
using FarmManager.App.Views.Vendors;
using FarmManager.Model.DatabaseContext;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;
using FarmManager.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FarmManager;

public partial class MyApp : Application
{
    public static IHost? AppHost { get; private set; }
    public static ServiceProvider ServiceProvider { get; private set; }
    public MyApp()
    {

        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                // Database
                services.AddDbContext<FarmManagerContext>(options =>
                    options.UseNpgsql("Host=localhost;Port=5433;Database=FarmManager;Username=postgres;Password=admin"));

                #region Windows

                // Main Window
                services.AddSingleton<MainWindow>();

                //Choose Entity
                services.AddTransient<ChooseVarietyWindow>();
                services.AddTransient<ChoosePlantWindow>();
                services.AddTransient<ChooseFertilizerWindow>();
                services.AddTransient<ChooseVendorWindow>();

                //Deposits
                services.AddTransient<DepositsWindow>();
                services.AddTransient<DepositAddWindow>();
                services.AddTransient<DepositEditWindow>();

                //Diseases
                services.AddTransient<DiseasesWindow>();
                services.AddTransient<DiseaseAddWindow>();
                services.AddTransient<DiseaseEditWindow>();

                //Employees
                services.AddTransient<EmployeesWindow>();
                services.AddTransient<EmployeeAddWindow>();
                services.AddTransient<EmployeeEditWindow>();

                //Fertilizers
                services.AddTransient<FertilizersWindow>();
                services.AddTransient<FertilizerAddWindow>();
                services.AddTransient<FertilizerEditWindow>();

                //Varieties
                services.AddTransient<VarietiesWindow>();
                services.AddTransient<VarietyAddWindow>();
                services.AddTransient<VarietyEditWindow>();

                //Vendors
                services.AddTransient<VendorsWindow>();
                services.AddTransient<VendorAddWindow>();
                services.AddTransient<VendorEditWindow>();

                //Plants
                services.AddTransient<PlantsWindow>();
                services.AddTransient<PlantAddWindow>();
                services.AddTransient<PlantEditWindow>();
                

                //Seasons
                services.AddTransient<SeasonsWindow>();
                services.AddTransient<SeasonAddWindow>();
                services.AddTransient<SeasonEditWindow>();

                //Buys
                services.AddTransient<BuysWindow>();
                services.AddTransient<BuyAddWindow>();
                services.AddTransient<BuyEditWindow>();

                #endregion


                #region ViewModels

                // Main Window
                services.AddTransient<MainWindowViewModel>();

                //Choose Entity
                services.AddTransient<ChooseFertilizerViewModel>();
                services.AddTransient<ChooseVendorViewModel>();
                services.AddTransient<ChoosePlantViewModel>();
                services.AddTransient<ChooseVarietyViewModel>();

                //Deposits
                services.AddTransient<DepositsViewModel>();
                services.AddTransient<DepositAddViewModel>();
                services.AddTransient<DepositEditViewModel>();

                //Diseases
                services.AddTransient<DiseasesViewModel>();
                services.AddTransient<DiseaseAddViewModel>();
                services.AddTransient<DiseaseEditViewModel>();

                //Employees
                services.AddTransient<EmployeesViewModel>();
                services.AddTransient<EmployeeAddViewModel>();
                services.AddTransient<EmployeeEditViewModel>();

                //Fertilizers
                services.AddTransient<FertilizersViewModel>();
                services.AddTransient<FertilizerAddViewModel>();
                services.AddTransient<FertilizerEditViewModel>();

                //Varieties
                services.AddTransient<VarietiesViewModel>();
                services.AddTransient<VarietyAddViewModel>();
                services.AddTransient<VarietyEditViewModel>();

                //Vendors
                services.AddTransient<VendorsViewModel>();
                services.AddTransient<VendorAddViewModel>();
                services.AddTransient<VendorEditViewModel>();

                //Plants
                services.AddTransient<PlantsViewModel>();
                services.AddTransient<PlantAddViewModel>();
                services.AddTransient<PlantEditViewModel>();

                //Seasons
                services.AddTransient<SeasonsViewModel>();
                services.AddTransient<SeasonAddViewModel>();
                services.AddTransient<SeasonEditViewModel>();

                //Buys
                services.AddTransient<BuysViewModel>();
                services.AddTransient<BuyAddViewModel>();
                services.AddTransient<BuyEditViewModel>();

                #endregion

                #region Services

                // Services
                services.AddScoped<IFarmManagerContext>(provider => provider.GetRequiredService<FarmManagerContext>());
                services.AddScoped<IUnitOfWork, UnitOfWork>();
                services.AddScoped<IDepositService, DepositService>();
                services.AddScoped<IDiseaseService, DiseaseService>();
                services.AddScoped<IEmployeeService, EmployeeService>();
                services.AddScoped<IFertilizerService, FertilizerService>();
                services.AddScoped<IVarietyService, VarietyService>();
                services.AddScoped<IVendorService, VendorService>();
                services.AddScoped<IPlantService, PlantService>();
                services.AddScoped<ISeasonService, SeasonService>();
                services.AddScoped<IBuyService, BuyService>();

                //Service Prodiver
                ServiceProvider = services.BuildServiceProvider();

                #endregion

            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();
        var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        AppHost.Dispose();
        base.OnExit(e);
    }
}
