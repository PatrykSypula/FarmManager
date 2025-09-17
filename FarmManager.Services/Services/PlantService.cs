using FarmManager.Model.DatabaseContext;
using FarmManager.Model.Exceptions;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Services.Services;
public class PlantService(IFarmManagerContext context, IUnitOfWork unitOfWork) : IPlantService
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
        return await context.Plants.Include(p => p.Variety).AsNoTracking().Where(d => d.Id == id).FirstOrDefaultAsync()
            ?? throw new NotFoundException("Nie mozna znaleźć rośliny.");
    }
    public async Task Add(Plant entity)
    {
        context.Plants.Update(entity);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task Update(Plant entity)
    {
        var existingEntity = context.Plants.FirstOrDefault(d => d.Id == entity.Id) ??
            throw new NotFoundException("Nie mozna znaleźć rośliny.");
        existingEntity.Name = entity.Name;
        existingEntity.Description = entity.Description;
        existingEntity.VarietyId = entity.Variety.Id;
        existingEntity.IsActive = entity.IsActive;
        await unitOfWork.SaveChangesAsync();
    }
    public async Task Delete(int id)
    {
        var entity = context.Plants.FirstOrDefault(d => d.Id == id) ??
            throw new NotFoundException("Nie mozna znaleźć rośliny.");
        entity.IsDeleted = true;
        await unitOfWork.SaveChangesAsync();
    }
}
