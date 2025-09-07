using FarmManager.App.ViewModels.Deposits;
using Microsoft.Extensions.DependencyInjection;

namespace FarmManager.App.Helpers;

public class ViewModelLocator
{
    public DepositsViewModel DepositsViewModel =>
        MyApp.ServiceProvider.GetRequiredService<DepositsViewModel>();
    public DepositAddViewModel DepositAddViewModel =>
        MyApp.ServiceProvider.GetRequiredService<DepositAddViewModel>();
    public DepositEditViewModel DepositEditViewModel =>
        MyApp.ServiceProvider.GetRequiredService<DepositEditViewModel>();
}
