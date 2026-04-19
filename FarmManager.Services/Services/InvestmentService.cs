using FarmManager.Model.DatabaseContext;
using FarmManager.Model.Exceptions;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Services.Services;

public class InvestmentService(IFarmManagerContext context) : IInvestmentService
{
    public async Task<ICollection<Investment>> GetAll(bool activeOnly = true)
    {
        IQueryable<Investment> query = context.Investments.AsQueryable();
        if (activeOnly)
        {
            query = query.Where(d => d.IsActive);
        }

        return await query
            .Include(d => d.Plant)
            .OrderByDescending(d => d.IsActive)
            .ThenBy(d => d.Id)
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<Investment> Get(int id)
    {
        return await context.Investments.AsNoTracking().Include(d => d.Plant).Where(d => d.Id == id).FirstOrDefaultAsync()
            ?? throw new NotFoundException("Nie można znaleźć inwestycji.");
    }
    public async Task Add(Investment entity)
    {
        context.Investments.Update(entity);
    }

    public async Task Update(Investment entity)
    {
        var existingEntity = await context.Investments.FirstOrDefaultAsync(d => d.Id == entity.Id) ??
            throw new NotFoundException("Nie można znaleźć inwestycji.");
        existingEntity.Name = entity.Name;
        existingEntity.PlantId = entity.PlantId;
        existingEntity.Price = entity.Price;
        existingEntity.Date = entity.Date;
        existingEntity.Description = entity.Description;
    }
    public async Task Delete(int id)
    {
        var entity = await context.Investments.FirstOrDefaultAsync(d => d.Id == id) ??
            throw new NotFoundException("Nie można znaleźć inwestycji.");
        entity.IsDeleted = true;
    }
}
