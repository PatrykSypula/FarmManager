using FarmManager.Model.DatabaseContext;
using FarmManager.Model.Exceptions;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Services.Services;

public class VendorService(IFarmManagerContext context) : IVendorService
{
    public async Task<ICollection<Vendor>> GetAll(bool activeOnly = true)
    {
        IQueryable<Vendor> query = context.Vendors.AsQueryable();
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
    public async Task<Vendor> Get(int id)
    {
        return await context.Vendors.AsNoTracking().Where(d => d.Id == id).FirstOrDefaultAsync()
            ?? throw new NotFoundException("Nie można znaleźć sprzedawcy.");
    }
    public async Task Add(Vendor entity)
    {
        context.Vendors.Update(entity);
    }

    public async Task Update(Vendor entity)
    {
        var existingEntity = await context.Vendors.FirstOrDefaultAsync(d => d.Id == entity.Id) ??
            throw new NotFoundException("Nie można znaleźć sprzedawcy.");
        existingEntity.Name = entity.Name;
        existingEntity.PhoneNumber = entity.PhoneNumber;
        existingEntity.Email = entity.Email;
        existingEntity.Description = entity.Description;
        existingEntity.IsActive = entity.IsActive;
    }
    public async Task Delete(int id)
    {
        var entity = await context.Vendors.FirstOrDefaultAsync(d => d.Id == id) ??
            throw new NotFoundException("Nie można znaleźć sprzedawcy.");
        entity.IsDeleted = true;
    }
}
