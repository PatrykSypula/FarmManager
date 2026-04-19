using FarmManager.Model.DatabaseContext;
using FarmManager.Model.Exceptions;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Services.Services;

public class ReportService(IFarmManagerContext context) : IReportService
{
    public async Task<ICollection<Season>> GetSeasons()
    {
        return await context.Seasons
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Report> GenerateReport(int seasonId)
    {
        var season = await context.Seasons
            .Include(s => s.Plant)
            .Where(s => s.Id == seasonId)
            .FirstOrDefaultAsync()
            ?? throw new NotFoundException("Nie znaleziono sezonu");

        var workdays = await context.Workdays
            .Include(w => w.WorkdaysCollecting)
            .Include(w => w.WorkdaysHourly)
            .Where(w => w.Date >= season.StartDate && w.Date <= season.EndDate & w.PlantId == season.PlantId)
            .ToListAsync();

        var employeeEarnings = workdays.SelectMany(w => w.WorkdaysCollecting).Sum(c => c.Quantity * c.Price) + workdays.SelectMany(w => w.WorkdaysHourly).Sum(h => h.Hours * h.Price);

        var sells = await context.Sells
            .Include(h => h.HarvestQuantity)
            .Where(h => h.Date >= season.StartDate && h.Date <= season.EndDate)
            .ToListAsync();

        var income = sells.Sum(s => s.Price);

        var sprayings = await context.Sprayings
            .Include(s => s.BuyQuantity)
            .Where(s => s.Date >= season.StartDate && s.Date <= season.EndDate)
            .ToListAsync();

        var sprayingCost = sprayings.Sum(s => s.BuyQuantity.Sum(bq => bq.TotalPrice));

        var employeeIds = workdays
            .SelectMany(w => w.WorkdaysCollecting.Select(c => c.EmployeeId))
            .Concat(workdays.SelectMany(w => w.WorkdaysHourly.Select(h => h.EmployeeId)))
            .Distinct()
            .ToList();

        var rentableEmployees = await context.Employees
            .Where(e => employeeIds.Contains(e.Id) && e.IsRentable)
            .ToListAsync();

        decimal totalRent = 0;

        foreach (var employee in rentableEmployees)
        {
            var uniqueWorkdays = workdays
                .Where(w => w.WorkdaysCollecting.Any(c => c.EmployeeId == employee.Id) ||
                            w.WorkdaysHourly.Any(h => h.EmployeeId == employee.Id))
                .Select(w => w.Date)
                .Distinct()
                .Count();

            decimal rentPerDay = employee.BaseRent ?? 0;
            totalRent += uniqueWorkdays * rentPerDay;
        }

        var investments = await context.Investments
            .Where(i => i.Date >= season.StartDate && i.Date <= season.EndDate && i.PlantId == season.PlantId)
            .ToListAsync();

        var investmentCost = investments.Sum(s => s.Price);

        return new Report
        {
            SeasonName = season.Name,
            PlantName = season.Plant.Name,
            Duration = season.StartDate.ToString() + " - " + season.EndDate.ToString(),
            EmployeeEarnings = employeeEarnings,
            Rent = totalRent,
            SprayingCost = sprayingCost,
            Investment = investmentCost,
            Income = income,
            CountedInvestment = income - (employeeEarnings + totalRent + sprayingCost + investmentCost),
            CountedIncome = income - (employeeEarnings + totalRent + sprayingCost)
        };
    }
}
