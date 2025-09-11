using FarmManager.Model.DatabaseContext;
using FarmManager.Model.Exceptions;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Services.Services;

public class DepositService(IFarmManagerContext context, IUnitOfWork unitOfWork) : IDepositService
{
    public async Task<ICollection<Deposit>> GetAll()
    {
        return await context.Deposits
            .OrderByDescending(d => d.IsActive)
            .ThenBy(d => d.Id)
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<Deposit> Get(int id)
    {
        return await context.Deposits.AsNoTracking().Where(d => d.Id == id).FirstOrDefaultAsync()
            ?? throw new NotFoundException("Nie mozna znaleźć depozytu.");
    }
    public async Task Add(Deposit entity)
    {
        await context.Deposits.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task Update(Deposit entity)
    {
        var existingEntity = context.Deposits.FirstOrDefault(d => d.Id == entity.Id) ??
            throw new NotFoundException("Nie mozna znaleźć depozytu.");
        existingEntity.Name = entity.Name;
        existingEntity.PhoneNumber = entity.PhoneNumber;
        existingEntity.Email = entity.Email;
        existingEntity.Description = entity.Description;
        existingEntity.IsActive = entity.IsActive;
        await unitOfWork.SaveChangesAsync();
    }
    public async Task Delete(int id)
    {
        var entity = context.Deposits.FirstOrDefault(d => d.Id == id) ??
            throw new NotFoundException("Nie mozna znaleźć depozytu.");
        entity.IsDeleted = true;
        await unitOfWork.SaveChangesAsync();
    }
}
