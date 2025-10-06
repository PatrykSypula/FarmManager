using FarmManager.Model.DatabaseContext;
using FarmManager.Model.Exceptions;
using FarmManager.Model.Model;
using FarmManager.Model.Model.Base;
using FarmManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Services.Services;

public class WorkdayService(FarmManagerContext context) : IWorkdayService
{
    public async Task<ICollection<Workday>> GetWorkdays(DateOnly date)
    {
        return await context.Workdays
            .Include(w => w.Plant)
            .Include(w => w.Action)
            .Where(w => w.Date == date)
            .OrderByDescending(d => d.Id)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Workday> GetWorkday(int id)
    {
        return await context.Workdays.Include(x => x.WorkdaysCollecting).Include(x => x.WorkdaysHourly).AsNoTracking().Where(d => d.Id == id).FirstOrDefaultAsync();
    }
    public async Task<ICollection<WorkdayCollecting>> GetWorkdaysCollecting(int id)
    {
        return await context.WorkdayCollecting.Include(x => x.Employee).AsNoTracking().Where(d => d.WorkdayId == id).ToListAsync();
    }
    public async Task<ICollection<WorkdayHourly>> GetWorkdaysHourly(int id)
    {
        return await context.WorkdayHourly.Include(x => x.Employee).AsNoTracking().Where(d => d.WorkdayId == id).ToListAsync();
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
        existingEntity.ActionId = entity.ActionId;
        existingEntity.PlantId = entity.PlantId;

    }
    public async Task Delete(int id)
    {
        var entity = await context.Workdays.FirstOrDefaultAsync(d => d.Id == id) ??
            throw new NotFoundException("Nie mozna znaleźć dnia pracy.");
        entity.IsDeleted = true;
    }

    public async Task Detach(Workday entity)
    {
        context.Entry(entity).State = EntityState.Detached;
    }
}
