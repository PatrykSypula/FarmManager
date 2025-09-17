using FarmManager.Model.DatabaseContext;
using FarmManager.Model.Exceptions;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Services.Services;

public class EmployeeService(IFarmManagerContext context, IUnitOfWork unitOfWork) : IEmployeeService
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
            ?? throw new NotFoundException("Nie mozna znaleźć pracownika.");
    }
    public async Task Add(Employee entity)
    {
        context.Employees.Update(entity);
        await unitOfWork.SaveChangesAsync();
    }
    public async Task Update(Employee entity)
    {
        var existingEntity = context.Employees.FirstOrDefault(d => d.Id == entity.Id) ??
            throw new NotFoundException("Nie mozna znaleźć pracownika");
        existingEntity.FirstName = entity.FirstName;
        existingEntity.LastName = entity.LastName;
        existingEntity.IdNumber = entity.IdNumber;
        existingEntity.IsRentable = entity.IsRentable;
        existingEntity.BaseRent = entity.BaseRent;
        existingEntity.PhoneNumber = entity.PhoneNumber;
        existingEntity.Email = entity.Email;
        existingEntity.IsActive = entity.IsActive;
        await unitOfWork.SaveChangesAsync();
    }
    public async Task Delete(int id)
    {
        var entity = context.Employees.FirstOrDefault(d => d.Id == id) ??
            throw new NotFoundException("Nie mozna znaleźć pracownika");
        entity.IsDeleted = true;
        await unitOfWork.SaveChangesAsync();
    }
    
    
    
}
