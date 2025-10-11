using FarmManager.Model.DatabaseContext;
using FarmManager.Model.Exceptions;
using FarmManager.Model.Model;
using FarmManager.Model.UnitOfWork;
using FarmManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Services.Services;

public class BuyService(IFarmManagerContext context) : IBuyService
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
    }
    //Unused
    public async Task Update(Buy entity)
    {
        var existingEntity = await context.Buys.FirstOrDefaultAsync(d => d.Id == entity.Id) ??
            throw new NotFoundException("Nie mozna znaleźć zakupu.");
        existingEntity.Name = entity.Name;
        existingEntity.Description = entity.Description;
        existingEntity.Price = entity.Price;
        existingEntity.Quantity = entity.Quantity;
        existingEntity.FertilizerId = entity.FertilizerId;
        existingEntity.VendorId = entity.VendorId;
    }
    public async Task Delete(int id)
    {
        var entity = await context.Buys.FirstOrDefaultAsync(d => d.Id == id) ??
            throw new NotFoundException("Nie mozna znaleźć zakupu.");
        entity.IsDeleted = true;
    }

    public async Task<ICollection<SprayingBuyQuantity>> AdjustRemainingQuantity(double quantityChange, int fertilizerId)
    {
        var buy = await context.Buys
            .Where(b => b.IsActive && b.RemainingQuantity > 0 && b.FertilizerId == fertilizerId)
            .ToListAsync();
        ICollection<SprayingBuyQuantity> adjustments = [];
        for (var i = 0; i < buy.Count && quantityChange != 0; i++)
        {
            if (quantityChange > 0)
            {
                if (buy[i].RemainingQuantity > quantityChange)
                {
                    adjustments.Add(new SprayingBuyQuantity()
                    {
                        BuyId = buy[i].Id,
                        Quantity = quantityChange
                    });

                    buy[i].RemainingQuantity -= quantityChange;
                }
                else
                {
                    adjustments.Add(new SprayingBuyQuantity()
                    {
                        BuyId = buy[i].Id,
                        Quantity = buy[i].RemainingQuantity
                    });
                    quantityChange -= buy[i].RemainingQuantity;
                    buy[i].RemainingQuantity = 0;
                }
            }
            else
            {
                break;
            }
        }
        return adjustments;
    }
    public async Task RevertRemainingQuantity(ICollection<SprayingBuyQuantity> buyQuantities)
    {
        foreach (var item in buyQuantities)
        {
            var buy = await context.Buys.Where(b => b.Id == item.BuyId).FirstOrDefaultAsync()
                ?? throw new NotFoundException("Nie mozna znaleźć zakupu.");
            buy.RemainingQuantity += item.Quantity;
        }
    }
}
