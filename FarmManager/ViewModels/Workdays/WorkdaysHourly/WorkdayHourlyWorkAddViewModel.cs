using FarmManager.Model.Model;

namespace FarmManager.App.ViewModels.Workdays.WorkdaysHourly;

public class WorkdayHourlyWorkAddViewModel
{
    public event Action<Workday>? RequestClose;
    public async Task InitializeAsync(DateOnly date, WorkdayType workdayType)
    {

    }
}
