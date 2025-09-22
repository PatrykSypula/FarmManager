using FarmManager.Model.DatabaseContext;
using FarmManager.Model.Exceptions;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Services.Services;

public class VarietyService(IFarmManagerContext context) : IVarietyService
{
    public async Task<ICollection<Variety>> GetAll(bool activeOnly = true)
    {
        IQueryable<Variety> query = context.Varieties.AsQueryable();
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
    public async Task<Variety> Get(int id)
    {
        return await context.Varieties.AsNoTracking().Where(d => d.Id == id).FirstOrDefaultAsync()
            ?? throw new NotFoundException("Nie mozna znaleźć odmiany.");
    }
    public async Task Add(Variety entity)
    {
        context.Varieties.Update(entity);
    }

    public async Task Update(Variety entity)
    {
        var existingEntity = await context.Varieties.FirstOrDefaultAsync(d => d.Id == entity.Id) ??
            throw new NotFoundException("Nie mozna znaleźć odmiany.");
        existingEntity.Name = entity.Name;
        existingEntity.Description = entity.Description;
        existingEntity.IsActive = entity.IsActive;
    }
    public async Task Delete(int id)
    {
        var entity = await context.Varieties.FirstOrDefaultAsync(d => d.Id == id) ??
            throw new NotFoundException("Nie mozna znaleźć odmiany.");
        entity.IsDeleted = true;
    }
}
