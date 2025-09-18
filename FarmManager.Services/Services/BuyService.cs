using FarmManager.Model.DatabaseContext;
using FarmManager.Model.Exceptions;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Services.Services;

public class BuyService(IFarmManagerContext context, IUnitOfWork unitOfWork) : IBuyService
{
    public async Task<ICollection<Buy>> GetAll(bool activeOnly = true)
    {
        IQueryable<Buy> query = context.Buys.AsQueryable();
        if (activeOnly)
        {
            query = query.Where(d => d.IsActive);
        }

        return await query
            .Include(b => b.Fertilizer)
            .OrderByDescending(d => d.IsActive)
            .ThenBy(d => d.Id)
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<Buy> Get(int id)
    {
        return await context.Buys.AsNoTracking().Where(d => d.Id == id).FirstOrDefaultAsync()
            ?? throw new NotFoundException("Nie mozna znaleźć zakupu.");
    }
    public async Task Add(Buy entity)
    {
        context.Buys.Update(entity);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task Update(Buy entity)
    {
        var existingEntity = context.Buys.FirstOrDefault(d => d.Id == entity.Id) ??
            throw new NotFoundException("Nie mozna znaleźć zakupu.");
        existingEntity.Name = entity.Name;
        existingEntity.Description = entity.Description;
        existingEntity.Price = entity.Price;
        existingEntity.Quantity = entity.Quantity;
        existingEntity.FertilizerId = entity.FertilizerId;
        existingEntity.VendorId = entity.VendorId;
        await unitOfWork.SaveChangesAsync();
    }
    public async Task Delete(int id)
    {
        var entity = context.Buys.FirstOrDefault(d => d.Id == id) ??
            throw new NotFoundException("Nie mozna znaleźć zakupu.");
        entity.IsDeleted = true;
        await unitOfWork.SaveChangesAsync();
    }
}
