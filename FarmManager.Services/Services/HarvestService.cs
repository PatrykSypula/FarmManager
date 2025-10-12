using FarmManager.Model.DatabaseContext;
using FarmManager.Model.Exceptions;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Services.Services;

public class HarvestService(FarmManagerContext context) : IHarvestService
{
    public async Task<ICollection<Harvest>> GetAll(bool activeOnly = true)
    {
        IQueryable<Harvest> query = context.Harvests.AsQueryable();
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
    public async Task<Harvest> Get(int id)
    {
        return await context.Harvests.AsNoTracking().Where(d => d.Id == id).FirstOrDefaultAsync()
            ?? throw new NotFoundException("Nie mozna znaleźć zbioru.");
    }
    public async Task<int> Add(Harvest entity)
    {
        var harvest = context.Harvests.Update(entity);
        await context.SaveChangesAsync();
        return harvest.Entity.Id;
    }
    public async Task Update(Harvest entity)
    {
        var existingEntity = await context.Harvests.FirstOrDefaultAsync(d => d.Id == entity.Id) ??
            throw new NotFoundException("Nie mozna znaleźć zbioru.");
        existingEntity.CollectingQuantity = entity.CollectingQuantity;
        existingEntity.CollectingQuantityAdditional = entity.CollectingQuantityAdditional;
        existingEntity.HourlyQuantity = entity.HourlyQuantity;
        existingEntity.RemainingHourlyQuantity = entity.RemainingHourlyQuantity;
        existingEntity.RemainingCollectingQuantity = entity.RemainingCollectingQuantity;
        existingEntity.RemainingQuantityAdditional = entity.RemainingQuantityAdditional;
    }
    public async Task Delete(int id)
    {
        var entity = await context.Harvests.FirstOrDefaultAsync(d => d.Id == id) ??
            throw new NotFoundException("Nie mozna znaleźć zbioru.");
        entity.IsDeleted = true;
    }
}
