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
        return await context.Diseases.AsNoTracking().ToListAsync();
    }
    public async Task<Disease> Get(int id)
    {
        return await context.Diseases.AsNoTracking().Where(d => d.Id == id).FirstOrDefaultAsync()
            ?? throw new NotFoundException("Nie mozna choroby.");
    }
    public async Task Add(Disease disease)
    {
        await context.Diseases.AddAsync(disease);
        await unitOfWork.SaveChangesAsync();
    }
    public async Task Update(Disease disease)
    {
        var existingDisease = context.Diseases.FirstOrDefault(d => d.Id == disease.Id) ??
            throw new NotFoundException("Nie mozna znaleźć choroby");
        existingDisease.Name = disease.Name;
        existingDisease.Description = disease.Description;
        existingDisease.IsActive = disease.IsActive;
        await unitOfWork.SaveChangesAsync();
    }
    public async Task Delete(int id)
    {
        var disease = context.Diseases.FirstOrDefault(d => d.Id == id) ??
            throw new NotFoundException("Nie mozna znaleźć choroby");
        disease.IsDeleted = true;
        await unitOfWork.SaveChangesAsync();
    }



}
