using FarmManager.Model.DatabaseContext;
using FarmManager.Model.Exceptions;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Services.Services;

public class SeasonService(IFarmManagerContext context, IUnitOfWork unitOfWork) : ISeasonService
{
    public async Task<ICollection<Season>> GetAll(bool activeOnly = true)
    {
        IQueryable<Season> query = context.Seasons.AsQueryable();
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
    public async Task<Season> Get(int id)
    {
        return await context.Seasons.Include(p => p.Plant).AsNoTracking().Where(d => d.Id == id).FirstOrDefaultAsync()
            ?? throw new NotFoundException("Nie mozna znaleźć sezonu.");
    }
    public async Task Add(Season entity)
    {
        context.Seasons.Update(entity);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task Update(Season entity)
    {
        var existingEntity = context.Seasons.FirstOrDefault(d => d.Id == entity.Id) ??
            throw new NotFoundException("Nie mozna znaleźć sezonu.");
        existingEntity.Name = entity.Name;
        existingEntity.Description = entity.Description;
        existingEntity.StartDate = entity.StartDate;
        existingEntity.EndDate = entity.EndDate;
        existingEntity.PlantId = entity.Plant.Id;
        existingEntity.IsActive = entity.IsActive;
        await unitOfWork.SaveChangesAsync();
    }
    public async Task Delete(int id)
    {
        var entity = context.Seasons.FirstOrDefault(d => d.Id == id) ??
            throw new NotFoundException("Nie mozna znaleźć sezonu.");
        entity.IsDeleted = true;
        await unitOfWork.SaveChangesAsync();
    }
}
