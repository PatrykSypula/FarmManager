using FarmManager.Model.DatabaseContext;
using FarmManager.Model.Exceptions;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Services.Services;

public class DepositService(IFarmManagerContext context) : IDepositService
{
    public async Task<ICollection<Deposit>> GetAll(bool activeOnly = true)
    {
        IQueryable<Deposit> query = context.Deposits.AsQueryable();
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
    public async Task<Deposit> Get(int id)
    {
        return await context.Deposits.AsNoTracking().Where(d => d.Id == id).FirstOrDefaultAsync()
            ?? throw new NotFoundException("Nie można znaleźć kupca.");
    }
    public async Task Add(Deposit entity)
    {
        context.Deposits.Update(entity);
    }

    public async Task Update(Deposit entity)
    {
        var existingEntity = await context.Deposits.FirstOrDefaultAsync(d => d.Id == entity.Id) ??
            throw new NotFoundException("Nie można znaleźć kupca.");
        existingEntity.Name = entity.Name;
        existingEntity.PhoneNumber = entity.PhoneNumber;
        existingEntity.Email = entity.Email;
        existingEntity.Description = entity.Description;
        existingEntity.IsActive = entity.IsActive;
    }
    public async Task Delete(int id)
    {
        var entity = await context.Deposits.FirstOrDefaultAsync(d => d.Id == id) ??
            throw new NotFoundException("Nie można znaleźć kupca.");
        entity.IsDeleted = true;
    }
}
