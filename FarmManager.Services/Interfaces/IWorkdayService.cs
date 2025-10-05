using FarmManager.Model.Model;

namespace FarmManager.Services.Interfaces;

public interface IWorkdayService
{
    Task<ICollection<Workday>> GetWorkdays(DateOnly date);
    Task<Workday> GetWorkday(int id);
    Task<ICollection<WorkdayCollecting>> GetWorkdaysCollecting(int id);
    Task<ICollection<WorkdayHourly>> GetWorkdaysHourly(int id);
    Task Add(Workday entity);
    Task Update(Workday entity);
    Task Delete(int id);
}
