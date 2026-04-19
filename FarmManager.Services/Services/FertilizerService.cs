using FarmManager.Model.DatabaseContext;
using FarmManager.Model.Exceptions;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Services.Services;

public class FertilizerService(IFarmManagerContext context) : IFertilizerService
{
    public async Task<ICollection<Fertilizer>> GetAll(bool activeOnly = true)
    {
        IQueryable<Fertilizer> query = context.Fertilizers.AsQueryable();
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
    public async Task<Fertilizer> Get(int id)
    {
        return await context.Fertilizers.AsNoTracking().Where(d => d.Id == id).FirstOrDefaultAsync()
            ?? throw new NotFoundException("Nie można znaleźć nawozu.");
    }
    public async Task Add(Fertilizer entity)
    {
        context.Fertilizers.Update(entity);
    }

    public async Task Update(Fertilizer entity)
    {
        var existingEntity = await context.Fertilizers.FirstOrDefaultAsync(d => d.Id == entity.Id) ??
            throw new NotFoundException("Nie można znaleźć nawozu.");
        existingEntity.Name = entity.Name;
        existingEntity.Description = entity.Description;
        existingEntity.IsActive = entity.IsActive;
    }
    public async Task<DeletionResult> Delete(int id)
    {
        var entity = await context.Fertilizers.FirstOrDefaultAsync(d => d.Id == id) ??
            throw new NotFoundException("Nie można znaleźć nawozu.");

        var buy = await context.Buys.FirstOrDefaultAsync(d => d.FertilizerId == id);
        if (buy != null)
        {
            return new DeletionResult() { DidDelete = false, Message = "Nie można usunąć nawozu, ponieważ jest powiązany z zakupami. Rozważ zaznaczenie go jako nieaktywnego." };
        }

        var spraying = await context.Sprayings.FirstOrDefaultAsync(d => d.FertilizerId == id);
        if (spraying != null)
        {
            return new DeletionResult() { DidDelete = false, Message = "Nie można usunąć nawozu, ponieważ jest powiązany z pryskaniami. Rozważ zaznaczenie go jako nieaktywnego." };
        }

        entity.IsDeleted = true;

        return new DeletionResult() { DidDelete = true, Message = "Nawóz został pomyślnie usunięty." };  
    }

    public async Task<decimal> GetAvailableQuantity(int fertilizerId)
    {
        var list = await context.Buys
            .Where(d => d.FertilizerId == fertilizerId && d.RemainingQuantity > 0)
            .ToListAsync();
        return list.Sum(d => d.RemainingQuantity);
    }
}
