using FarmManager.App.ViewModels.Deposits;
using Microsoft.Extensions.DependencyInjection;

namespace FarmManager.App.Helpers;

public class ViewModelLocator
{
    public AddDepositViewModel AddDepositViewModel =>
        MyApp.ServiceProvider.GetRequiredService<AddDepositViewModel>();
}
