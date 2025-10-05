using System.Xml.Linq;
using FarmManager.Model.Model;
using FarmManager.Services.Services;

namespace FarmManager.App.ViewModels.Workdays.WorkdaysCollecting;

public class WorkdayHarvestCollectingAddViewModel
{
    public event Action<Workday>? RequestClose;
    public async Task InitializeAsync(DateOnly date, WorkdayType workdayType)
    {
        
    }
}
