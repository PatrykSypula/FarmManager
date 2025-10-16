using FarmManager.Model.DatabaseContext;
using FarmManager.Model.Exceptions;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Services.Services;
public class PlantService(IFarmManagerContext context) : IPlantService
{
    public async Task<ICollection<Plant>> GetAll(bool activeOnly = true)
    {
        IQueryable<Plant> query = context.Plants.AsQueryable();
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
    public async Task<Plant> Get(int id)
    {
        return await context.Plants.AsNoTracking().Where(d => d.Id == id).FirstOrDefaultAsync()
            ?? throw new NotFoundException("Nie mozna znaleźć rośliny.");
    }
    public async Task Add(Plant entity)
    {
        context.Plants.Update(entity);
    }

    public async Task Update(Plant entity)
    {
        var existingEntity = await context.Plants.FirstOrDefaultAsync(d => d.Id == entity.Id) ??
            throw new NotFoundException("Nie mozna znaleźć rośliny.");
        existingEntity.Name = entity.Name;
        existingEntity.Description = entity.Description;
        existingEntity.IsActive = entity.IsActive;
    }
    public async Task Delete(int id)
    {
        var entity = await context.Plants.FirstOrDefaultAsync(d => d.Id == id) ??
            throw new NotFoundException("Nie mozna znaleźć rośliny.");
        entity.IsDeleted = true;
    }
    public async Task<decimal> GetQuantity(int plantId)
    {
        var harvests = await context.Harvests
            .Where(h => h.Workday.PlantId == plantId && !h.IsDeleted)
            .ToListAsync();

        return harvests.Sum(h =>
            h.RemainingCollectingQuantity +
            h.RemainingQuantityAdditional +
            h.RemainingHourlyQuantity
        );
    }
}
