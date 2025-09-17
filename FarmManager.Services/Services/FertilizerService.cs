using FarmManager.Model.DatabaseContext;
using FarmManager.Model.Exceptions;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Services.Services;

public class FertilizerService(IFarmManagerContext context, IUnitOfWork unitOfWork) : IFertilizerService
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
            ?? throw new NotFoundException("Nie mozna znaleźć nawozu.");
    }
    public async Task Add(Fertilizer entity)
    {
        context.Fertilizers.Update(entity);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task Update(Fertilizer entity)
    {
        var existingEntity = context.Fertilizers.FirstOrDefault(d => d.Id == entity.Id) ??
            throw new NotFoundException("Nie mozna znaleźć nawozu.");
        existingEntity.Name = entity.Name;
        existingEntity.Description = entity.Description;
        existingEntity.IsActive = entity.IsActive;
        await unitOfWork.SaveChangesAsync();
    }
    public async Task Delete(int id)
    {
        var entity = context.Fertilizers.FirstOrDefault(d => d.Id == id) ??
            throw new NotFoundException("Nie mozna znaleźć nawozu.");
        entity.IsDeleted = true;
        await unitOfWork.SaveChangesAsync();
    }
}
