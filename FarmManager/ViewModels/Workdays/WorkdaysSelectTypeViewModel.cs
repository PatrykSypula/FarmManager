using FarmManager.App.Helpers;
using FarmManager.Model.Model;
namespace FarmManager.App.ViewModels.Workdays;

public class WorkdaysSelectTypeViewModel : BaseViewModel
{
    public event Action<WorkdayType>? RequestClose;

    public RelayCommand SelectHarvestCollecting => new RelayCommand(async execute => await SelectHarvestCollectingAsync());

    private async Task SelectHarvestCollectingAsync()
    {
            RequestClose?.Invoke(WorkdayType.HarvestCollecting);
    }
    public RelayCommand SelectHarvestHourly => new RelayCommand(async execute => await SelectHarvestHourlyAsync());

    private async Task SelectHarvestHourlyAsync()
    {
        RequestClose?.Invoke(WorkdayType.HarvestHourly);
    }
    public RelayCommand SelectHourlyWork => new RelayCommand(async execute => await SelectHourlyWorkAsync());

    private async Task SelectHourlyWorkAsync()
    {
        RequestClose?.Invoke(WorkdayType.HourlyWork);
    }

}
