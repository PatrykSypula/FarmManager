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
            ?? throw new NotFoundException("Nie można znaleźć rośliny.");
    }
    public async Task Add(Plant entity)
    {
        context.Plants.Update(entity);
    }

    public async Task Update(Plant entity)
    {
        var existingEntity = await context.Plants.FirstOrDefaultAsync(d => d.Id == entity.Id) ??
            throw new NotFoundException("Nie można znaleźć rośliny.");
        existingEntity.Name = entity.Name;
        existingEntity.Description = entity.Description;
        existingEntity.IsActive = entity.IsActive;
    }
    public async Task<DeletionResult> Delete(int id)
    {
        var entity = await context.Plants.FirstOrDefaultAsync(d => d.Id == id) ??
            throw new NotFoundException("Nie można znaleźć rośliny.");
        entity.IsDeleted = true;

        var sell = await context.Sells.FirstOrDefaultAsync(d => d.PlantId == id);
        if (sell != null)
        {
            return new DeletionResult() { DidDelete = false, Message = "Nie można usunąć rośliny, ponieważ jest ona powiązana z istniejącą sprzedażą. Rozważ zaznaczenie jej jako nieaktywnej." };
        }

        var workday = await context.Workdays.FirstOrDefaultAsync(d => d.PlantId == id);
        if (workday != null) {
            return new DeletionResult() { DidDelete = false, Message = "Nie można usunąć rośliny, ponieważ jest ona powiązana z istniejącym dniem pracy. Rozważ zaznaczenie jej jako nieaktywnej." };
        }

        var spraying = await context.Sprayings.FirstOrDefaultAsync(d => d.PlantId == id);
        if (spraying != null) {
            return new DeletionResult() { DidDelete = false, Message = "Nie można usunąć rośliny, ponieważ jest ona powiązana z istniejącym pryskaniem. Rozważ zaznaczenie jej jako nieaktywnej." };
        }

        var season = await context.Seasons.FirstOrDefaultAsync(d => d.PlantId == id);
        if (season != null) {
            return new DeletionResult() { DidDelete = false, Message = "Nie można usunąć rośliny, ponieważ jest ona powiązana z istniejącym sezonem. Rozważ zaznaczenie jej jako nieaktywnej." };
        }

        return new DeletionResult() { DidDelete = true, Message = "Roślina została pomyślnie usunięta." };
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
