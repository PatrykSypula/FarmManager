using System.Windows;
using FarmManager.App.ViewModels;
using FarmManager.App.ViewModels.Deposits;
using FarmManager.App.ViewModels.Diseases;
using FarmManager.App.Views.Deposits;
using FarmManager.App.Views.Diseases;
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
                //Windows
                services.AddSingleton<MainWindow>();

                //Deposits
                services.AddTransient<DepositsWindow>();
                services.AddTransient<DepositAddWindow>();
                services.AddTransient<DepositEditWindow>();

                //Diseases
                services.AddTransient<DiseasesWindow>();
                services.AddTransient<DiseaseAddWindow>();
                services.AddTransient<DiseaseEditWindow>();


                //ViewModels
                services.AddTransient<MainWindowViewModel>();

                //Deposits
                services.AddTransient<DepositsViewModel>();
                services.AddTransient<DepositAddViewModel>();
                services.AddTransient<DepositEditViewModel>();

                //Diseases
                services.AddTransient<DiseasesViewModel>();
                services.AddTransient<DiseaseAddViewModel>();
                services.AddTransient<DiseaseEditViewModel>();

                // Database
                services.AddDbContext<FarmManagerContext>(options =>
                    options.UseNpgsql("Host=localhost;Port=5433;Database=FarmManager;Username=postgres;Password=admin"));

                // Services
                services.AddScoped<IFarmManagerContext>(provider => provider.GetRequiredService<FarmManagerContext>());
                services.AddScoped<IUnitOfWork, UnitOfWork>();
                services.AddScoped<IDepositService, DepositService>();
                services.AddScoped<IDiseaseService, DiseaseService>();

                //ServiceProdivers
                ServiceProvider = services.BuildServiceProvider();
                ServiceProvider.GetRequiredService<DepositAddViewModel>();

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
