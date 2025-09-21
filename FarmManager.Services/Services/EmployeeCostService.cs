using FarmManager.Model.DatabaseContext;
using FarmManager.Model.Exceptions;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Services.Services;

public class EmployeeCostService(IFarmManagerContext context, IUnitOfWork unitOfWork) : IEmployeeCostService
{
    public async Task<ICollection<EmployeeCost>> GetAll(bool activeOnly = true)
    {
        IQueryable<EmployeeCost> query = context.EmployeeCosts.AsQueryable();
        if (activeOnly)
        {
            query = query.Where(d => d.IsActive);
        }

        return await query
            .Include(d => d.Employee)
            .OrderByDescending(d => d.IsActive)
            .ThenBy(d => d.Id)
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<EmployeeCost> Get(int id)
    {
        return await context.EmployeeCosts.AsNoTracking().Include(d => d.Employee).Where(d => d.Id == id).FirstOrDefaultAsync()
            ?? throw new NotFoundException("Nie mozna znaleźć pożyczki pracownika.");
    }
    public async Task Add(EmployeeCost entity)
    {
        context.EmployeeCosts.Update(entity);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task Update(EmployeeCost entity)
    {
        var existingEntity = context.EmployeeCosts.FirstOrDefault(d => d.Id == entity.Id) ??
            throw new NotFoundException("Nie mozna znaleźć pożyczki pracownika.");
        existingEntity.EmployeeId = entity.EmployeeId;
        existingEntity.Quantity = entity.Quantity;
        existingEntity.Date = entity.Date;
        await unitOfWork.SaveChangesAsync();
    }
    public async Task Delete(int id)
    {
        var entity = context.EmployeeCosts.FirstOrDefault(d => d.Id == id) ??
            throw new NotFoundException("Nie mozna znaleźć pożyczki pracownika.");
        entity.IsDeleted = true;
        await unitOfWork.SaveChangesAsync();
    }
}
