using FarmManager.Model.DatabaseContext;
using FarmManager.Model.Exceptions;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Services.Services;

public class DiseaseService(IFarmManagerContext context, IUnitOfWork unitOfWork) : IDiseaseService
{
    public async Task<ICollection<Disease>> GetAll()
    {
        return await context.Diseases
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
        await context.Diseases.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
    }
    public async Task Update(Disease entity)
    {
        var existingEntity = context.Diseases.FirstOrDefault(d => d.Id == entity.Id) ??
            throw new NotFoundException("Nie mozna znaleźć choroby");
        existingEntity.Name = entity.Name;
        existingEntity.Description = entity.Description;
        existingEntity.IsActive = entity.IsActive;
        await unitOfWork.SaveChangesAsync();
    }
    public async Task Delete(int id)
    {
        var entity = context.Diseases.FirstOrDefault(d => d.Id == id) ??
            throw new NotFoundException("Nie mozna znaleźć choroby");
        entity.IsDeleted = true;
        await unitOfWork.SaveChangesAsync();
    }



}
