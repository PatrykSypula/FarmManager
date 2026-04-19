using FarmManager.Model.DatabaseContext;
using FarmManager.Model.Exceptions;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Services.Services;

public class SprayingService(IFarmManagerContext context) : ISprayingService
{
    public async Task<ICollection<Spraying>> GetAll(bool activeOnly = true)
    {
        IQueryable<Spraying> query = context.Sprayings.AsQueryable();
        if (activeOnly)
        {
            query = query.Where(d => d.IsActive);
        }

        return await query
            .Include(s => s.Plant)
            .Include(s => s.Fertilizer)
            .Include(s => s.BuyQuantity)
            .OrderByDescending(d => d.IsActive)
            .ThenBy(d => d.Id)
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<Spraying> Get(int id)
    {
        return await context.Sprayings
            .AsNoTracking()
            .Include(s => s.Plant)
            .Include(s => s.Fertilizer)
            .Include(s => s.BuyQuantity)
            .Where(d => d.Id == id).FirstOrDefaultAsync()
            ?? throw new NotFoundException("Nie można znaleźć pryskania.");
    }
    public async Task Add(Spraying entity)
    {
        context.Sprayings.Update(entity);
    }

    public async Task Delete(int id)
    {
        var entity = await context.Sprayings.FirstOrDefaultAsync(d => d.Id == id) ??
            throw new NotFoundException("Nie można znaleźć pryskania.");
        entity.IsDeleted = true;
    }
    public async Task<ICollection<Spraying>> GetSprayingsInMonth(int year, int month)
    {
        return await context.Sprayings
            .Include(w => w.Plant)
            .Where(w => w.Date.Year == year && w.Date.Month == month)
            .AsNoTracking()
            .ToListAsync();
    }
}
