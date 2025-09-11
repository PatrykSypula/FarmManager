using FarmManager.Model.DatabaseContext;
using FarmManager.Model.Exceptions;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Services.Services;

public class VarietyService(IFarmManagerContext context, IUnitOfWork unitOfWork) : IVarietyService
{
    public async Task<ICollection<Variety>> GetAll()
    {
        return await context.Varieties.AsNoTracking().ToListAsync();
    }
    public async Task<Variety> Get(int id)
    {
        return await context.Varieties.AsNoTracking().Where(d => d.Id == id).FirstOrDefaultAsync()
            ?? throw new NotFoundException("Nie mozna znaleźć odmiany.");
    }
    public async Task Add(Variety entity)
    {
        await context.Varieties.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task Update(Variety entity)
    {
        var existingEntity = context.Varieties.FirstOrDefault(d => d.Id == entity.Id) ??
            throw new NotFoundException("Nie mozna znaleźć odmiany.");
        existingEntity.Name = entity.Name;
        existingEntity.Description = entity.Description;
        existingEntity.IsActive = entity.IsActive;
        await unitOfWork.SaveChangesAsync();
    }
    public async Task Delete(int id)
    {
        var entity = context.Varieties.FirstOrDefault(d => d.Id == id) ??
            throw new NotFoundException("Nie mozna znaleźć odmiany.");
        entity.IsDeleted = true;
        await unitOfWork.SaveChangesAsync();
    }
}
