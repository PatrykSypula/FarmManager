using FarmManager.Model.Model;

namespace FarmManager.Services.Interfaces;

public interface IReportService
{
    Task<ICollection<Season>> GetSeasons();
    public Task<Report> GenerateReport(int seasonId);
}
