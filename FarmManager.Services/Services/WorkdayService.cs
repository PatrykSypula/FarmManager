using FarmManager.Model.DatabaseContext;
using FarmManager.Model.Exceptions;
using FarmManager.Model.Model;
using FarmManager.Model.Model.Base;
using FarmManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Services.Services;

public class WorkdayService(IFarmManagerContext context) : IWorkdayService
{
    public async Task<ICollection<Workday>> GetAll(bool activeOnly = true)
    {
        IQueryable<Workday> query = context.Workdays.AsQueryable();
        if (activeOnly)
        {
            query = query.Where(d => d.IsActive);
        }

        return await query
            .OrderByDescending(d => d.IsActive)
            .ThenBy(d => d.Id)
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<Workday?> GetWorkday(int id)
    {
        return await context.Workdays.AsNoTracking().Where(d => d.Id == id).FirstOrDefaultAsync();
    }
    public async Task<WorkdayCollecting> GetWorkdayCollecting(int Id)
    {
        return await context.WorkdayCollecting.AsNoTracking().Where(d => d.Id == Id).FirstOrDefaultAsync()
            ?? throw new NotFoundException("Nie można znaleźć dnia na wagę.");
    }
    public async Task<WorkdayHourly> GetWorkdayHourly(int id)
    {
        return await context.WorkdayHourly.AsNoTracking().Where(d => d.Id == id).FirstOrDefaultAsync()
            ?? throw new NotFoundException("Nie można znaleźć dnia na godziny."); ;
    }
    public async Task Add(Workday entity)
    {
        context.Workdays.Update(entity);
    }
    public async Task Update(Workday entity)
    {
        var existingEntity = await context.Workdays.FirstOrDefaultAsync(d => d.Id == entity.Id) ??
            throw new NotFoundException("Nie mozna znaleźć choroby");
        existingEntity.Description = entity.Description;
        existingEntity.WorkdaysHourly = entity.WorkdaysHourly;
        existingEntity.WorkdaysCollecting = entity.WorkdaysCollecting;
        double remainingToPay = 0;
        foreach (var item in existingEntity.WorkdaysHourly)
        {
            remainingToPay += item.RemainingToPay;
        }
        if (remainingToPay == 0)
        {
            existingEntity.IsHourlyPayed = true;
        }
        else
        {
                       existingEntity.IsHourlyPayed = false;
        }
        remainingToPay = 0;
        foreach (var item in existingEntity.WorkdaysCollecting)
        {
            remainingToPay += item.RemainingToPay;
        }
        if (remainingToPay == 0)
        {
            existingEntity.IsHourlyPayed = true;
        }
        else
        {
            existingEntity.IsHourlyPayed = false;
        }

    }
    public async Task Delete(int id)
    {
        var entity = await context.Workdays.FirstOrDefaultAsync(d => d.Id == id) ??
            throw new NotFoundException("Nie mozna znaleźć dnia pracy.");
        entity.IsDeleted = true;
    }
}
