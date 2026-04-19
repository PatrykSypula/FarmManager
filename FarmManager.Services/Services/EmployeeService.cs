using FarmManager.Model.DatabaseContext;
using FarmManager.Model.Exceptions;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Services.Services;

public class EmployeeService(IFarmManagerContext context) : IEmployeeService
{
    public async Task<ICollection<Employee>> GetAll(bool activeOnly = true)
    {
        IQueryable<Employee> query = context.Employees.AsQueryable();
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
    public async Task<Employee> Get(int id)
    {
        return await context.Employees.AsNoTracking().Where(d => d.Id == id).FirstOrDefaultAsync()
            ?? throw new NotFoundException("Nie można znaleźć pracownika.");
    }
    public async Task Add(Employee entity)
    {
        context.Employees.Update(entity);
    }
    public async Task Update(Employee entity)
    {
        var existingEntity = await context.Employees.FirstOrDefaultAsync(d => d.Id == entity.Id) ??
            throw new NotFoundException("Nie można znaleźć pracownika");
        existingEntity.FirstName = entity.FirstName;
        existingEntity.LastName = entity.LastName;
        existingEntity.Description = entity.Description;
        existingEntity.IsRentable = entity.IsRentable;
        existingEntity.BaseRent = entity.BaseRent;
        existingEntity.PhoneNumber = entity.PhoneNumber;
        existingEntity.Nickname = entity.Nickname;
        existingEntity.IsActive = entity.IsActive;
    }
    public async Task Delete(int id)
    {
        var entity = await context.Employees.FirstOrDefaultAsync(d => d.Id == id) ??
            throw new NotFoundException("Nie można znaleźć pracownika");
        entity.IsDeleted = true;
    }

    public async Task<ICollection<Employee>> GetActiveForWorkday(IEnumerable<int> ids)
    {
        return await context.Employees
            .Where(d => !ids.Contains(d.Id) && d.IsActive)
            .AsNoTracking()
            .ToListAsync();
    }
}
