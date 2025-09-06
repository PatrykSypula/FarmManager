using System.Windows;
using FarmManager.App.ViewModels;
using FarmManager.App.ViewModels.Deposits;
using FarmManager.App.Views.Deposits;
using FarmManager.Model.DatabaseContext;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;
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
                services.AddTransient<AddDepositWindow>();

                //ViewModels
                services.AddTransient<MainWindowViewModel>();
                services.AddTransient<AddDepositViewModel>();


                // Database
                services.AddDbContext<FarmManagerContext>(options =>
                    options.UseNpgsql("Host=localhost;Port=5433;Database=FarmManager;Username=postgres;Password=admin"));

                // Services
                services.AddScoped<IFarmManagerContext>(provider => provider.GetRequiredService<FarmManagerContext>());
                services.AddScoped<IUnitOfWork, UnitOfWork>();
                services.AddScoped<IDepositService, DepositService>();

                //ServiceProdivers
                ServiceProvider = services.BuildServiceProvider();
                ServiceProvider.GetRequiredService<AddDepositViewModel>();

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
