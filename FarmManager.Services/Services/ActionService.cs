using FarmManager.Model.DatabaseContext;
using FarmManager.Model.Exceptions;
using FarmManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Action = FarmManager.Model.Model.Action;

namespace FarmManager.Services.Services;

public class ActionService(IFarmManagerContext context) : IActionService
{
    public async Task<ICollection<Action>> GetAll(bool activeOnly = true)
    {
        IQueryable<Action> query = context.Actions.AsQueryable();
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
    public async Task<Action> Get(int id)
    {
        return await context.Actions.AsNoTracking().Where(d => d.Id == id).FirstOrDefaultAsync()
            ?? throw new NotFoundException("Nie mozna znaleźć czynności.");
    }
    public async Task Add(Action entity)
    {
        context.Actions.Update(entity);
    }
    public async Task Update(Action entity)
    {
        var existingEntity = await context.Actions.FirstOrDefaultAsync(d => d.Id == entity.Id) ??
            throw new NotFoundException("Nie mozna znaleźć czynności");
        existingEntity.Name = entity.Name;
        existingEntity.Description = entity.Description;
        existingEntity.IsActive = entity.IsActive;
    }
    public async Task Delete(int id)
    {
        var entity = await context.Actions.FirstOrDefaultAsync(d => d.Id == id) ??
            throw new NotFoundException("Nie mozna znaleźć czynności");
        entity.IsDeleted = true;
    }
}
