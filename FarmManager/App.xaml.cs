using System.Windows;
using FarmManager.App.ViewModels;
using FarmManager.App.ViewModels.Deposits;
using FarmManager.App.ViewModels.Diseases;
using FarmManager.App.ViewModels.Employees;
using FarmManager.App.ViewModels.Fertilizers;
using FarmManager.App.ViewModels.Varieties;
using FarmManager.App.ViewModels.Vendors;
using FarmManager.App.Views.Deposits;
using FarmManager.App.Views.Diseases;
using FarmManager.App.Views.Employees;
using FarmManager.App.Views.Fertilizers;
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
    //public static ServiceProvider ServiceProvider { get; private set; }
    public MyApp()
    {

        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                // Database
                services.AddDbContext<FarmManagerContext>(options =>
                    options.UseNpgsql("Host=localhost;Port=5433;Database=FarmManager;Username=postgres;Password=admin"));

                #region Windows
                services.AddSingleton<MainWindow>();

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
                #endregion


                #region ViewModels
                services.AddTransient<MainWindowViewModel>();

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
                #endregion

                //ServiceProdivers
                //ServiceProvider = services.BuildServiceProvider();
                //ServiceProvider.GetRequiredService<DepositAddViewModel>();

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
