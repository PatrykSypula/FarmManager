using FarmManager.Model.DatabaseContext;
using FarmManager.Model.Exceptions;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Services.Services;

public class SprayingService(IFarmManagerContext context) : ISprayingService
{
    public async Task<ICollection<Spraying>> GetAll(bool activeOnly = true)
    {
        IQueryable<Spraying> query = context.Sprayings.AsQueryable();
        if (activeOnly)
        {
            query = query.Where(d => d.IsActive);
        }

        return await query
            .Include(s => s.Plant)
            .Include(s => s.Fertilizer)
            .Include(s => s.BuyQuantity)
            .OrderByDescending(d => d.IsActive)
            .ThenBy(d => d.Id)
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<Spraying> Get(int id)
    {
        return await context.Sprayings
            .AsNoTracking()
            .Include(s => s.Plant)
            .Include(s => s.Fertilizer)
            .Include(s => s.BuyQuantity)
            .Where(d => d.Id == id).FirstOrDefaultAsync()
            ?? throw new NotFoundException("Nie mozna znaleźć pryskania.");
    }
    public async Task Add(Spraying entity)
    {
        context.Sprayings.Update(entity);
    }


    //Unused
    public async Task Update(Spraying entity)
    {
        var existingEntity = await context.Sprayings.FirstOrDefaultAsync(d => d.Id == entity.Id) ??
            throw new NotFoundException("Nie mozna znaleźć pryskania.");
        existingEntity.PlantId = entity.PlantId;
        existingEntity.FertilizerId = entity.FertilizerId;
        existingEntity.Quantity = entity.Quantity;
        existingEntity.Date = entity.Date;
    }
    public async Task Delete(int id)
    {
        var entity = await context.Sprayings.FirstOrDefaultAsync(d => d.Id == id) ??
            throw new NotFoundException("Nie mozna znaleźć pryskania.");
        entity.IsDeleted = true;
    }
}
