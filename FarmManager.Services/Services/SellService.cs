using FarmManager.Model.DatabaseContext;
using FarmManager.Model.Exceptions;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Services.Services;

public class SellService(IFarmManagerContext context) : ISellService
{
    public async Task<ICollection<Sell>> GetAll(bool activeOnly = true)
    {
        IQueryable<Sell> query = context.Sells.AsQueryable();
        if (activeOnly)
        {
            query = query.Where(d => d.IsActive);
        }

        return await query
            .Include(s => s.Deposit)
            .Include(s => s.HarvestQuantity)
            .OrderByDescending(d => d.IsActive)
            .ThenBy(d => d.Id)
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<Sell> Get(int id)
    {
        return await context.Sells
            .AsNoTracking()
            .Include(s => s.Deposit)
            .Include(s => s.HarvestQuantity)
            .Where(d => d.Id == id).FirstOrDefaultAsync()
            ?? throw new NotFoundException("Nie mozna znaleźć sprzedaży.");
    }
    public async Task Add(Sell entity)
    {
        context.Sells.Update(entity);
    }

    public async Task Delete(int id)
    {
        var entity = await context.Sells.FirstOrDefaultAsync(d => d.Id == id) ??
            throw new NotFoundException("Nie mozna znaleźć sprzedaży.");
        entity.IsDeleted = true;
    }
}
