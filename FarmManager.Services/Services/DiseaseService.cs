using FarmManager.Model.DatabaseContext;
using FarmManager.Model.Exceptions;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Services.Services;

public class DiseaseService(IFarmManagerContext context) : IDiseaseService
{
    public async Task<ICollection<Disease>> GetAll(bool activeOnly = true)
    {
        IQueryable<Disease> query = context.Diseases.AsQueryable();
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
    public async Task<Disease> Get(int id)
    {
        return await context.Diseases.AsNoTracking().Where(d => d.Id == id).FirstOrDefaultAsync()
            ?? throw new NotFoundException("Nie mozna znaleźć choroby.");
    }
    public async Task Add(Disease entity)
    {
        context.Diseases.Update(entity);
    }
    public async Task Update(Disease entity)
    {
        var existingEntity = await context.Diseases.FirstOrDefaultAsync(d => d.Id == entity.Id) ??
            throw new NotFoundException("Nie mozna znaleźć choroby");
        existingEntity.Name = entity.Name;
        existingEntity.Description = entity.Description;
        existingEntity.IsActive = entity.IsActive;
    }
    public async Task Delete(int id)
    {
        var entity = await context.Diseases.FirstOrDefaultAsync(d => d.Id == id) ??
            throw new NotFoundException("Nie mozna znaleźć choroby");
        entity.IsDeleted = true;
    }
}
