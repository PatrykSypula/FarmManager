using FarmManager.Model.DatabaseContext;
using FarmManager.Model.Exceptions;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Services.Services;

public class FertilizerService(IFarmManagerContext context, IUnitOfWork unitOfWork) : IFertilizerService
{
    public async Task<ICollection<Fertilizer>> GetAll()
    {
        return await context.Fertilizers.AsNoTracking().ToListAsync();
    }
    public async Task<Fertilizer> Get(int id)
    {
        return await context.Fertilizers.AsNoTracking().Where(d => d.Id == id).FirstOrDefaultAsync()
            ?? throw new NotFoundException("Nie mozna znaleźć nawozu.");
    }
    public async Task Add(Fertilizer entity)
    {
        await context.Fertilizers.AddAsync(entity);
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
