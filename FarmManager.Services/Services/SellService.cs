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
            .Include(s => s.Plant)
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
    public async Task<ICollection<SellHarvestQuantity>> AdjustRemainingQuantity(decimal quantityChange, int plantId)
    {
        var harvest = await context.Harvests
            .Where(b => b.IsActive && (b.RemainingCollectingQuantity > 0 || b.RemainingHourlyQuantity > 0 || b.RemainingQuantityAdditional > 0) && b.Workday.PlantId == plantId)
            .ToListAsync();
        ICollection<SellHarvestQuantity> adjustments = [];
        for (var i = 0; i < harvest.Count; i++)
        {
            if (quantityChange > 0)
            {
                if (harvest[i].RemainingCollectingQuantity != null && harvest[i].RemainingCollectingQuantity > 0)
                {
                    if (harvest[i].RemainingCollectingQuantity > quantityChange)
                    {
                        adjustments.Add(new SellHarvestQuantity()
                        {
                            HarvestId = harvest[i].Id,
                            CollectingQuantity = quantityChange,
                        });

                        harvest[i].RemainingCollectingQuantity -= quantityChange;
                        quantityChange = 0;
                    }
                    else
                    {
                        adjustments.Add(new SellHarvestQuantity()
                        {
                            HarvestId = harvest[i].Id,
                            CollectingQuantity = harvest[i].RemainingCollectingQuantity,
                        });
                        quantityChange -= harvest[i].RemainingCollectingQuantity;
                        harvest[i].RemainingCollectingQuantity = 0;
                    }
                }

                if (harvest[i].RemainingQuantityAdditional != null && harvest[i].RemainingQuantityAdditional > 0)
                {
                    if (harvest[i].RemainingQuantityAdditional > quantityChange)
                    {
                        adjustments.Add(new SellHarvestQuantity()
                        {
                            HarvestId = harvest[i].Id,
                            CollectingQuantityAdditional = quantityChange,
                        });

                        harvest[i].RemainingQuantityAdditional -= quantityChange;
                        quantityChange = 0;
                    }
                    else
                    {
                        adjustments.Add(new SellHarvestQuantity()
                        {
                            HarvestId = harvest[i].Id,
                            CollectingQuantityAdditional = harvest[i].RemainingQuantityAdditional,
                        });
                        quantityChange -= harvest[i].RemainingQuantityAdditional;
                        harvest[i].RemainingQuantityAdditional = 0;
                    }
                }

                if (harvest[i].RemainingHourlyQuantity != null && harvest[i].RemainingHourlyQuantity > 0)
                {
                    if (harvest[i].RemainingHourlyQuantity > quantityChange)
                    {
                        adjustments.Add(new SellHarvestQuantity()
                        {
                            HarvestId = harvest[i].Id,
                            HourlyQuantity = quantityChange,
                        });

                        harvest[i].RemainingHourlyQuantity -= quantityChange;
                        quantityChange = 0;
                    }
                    else
                    {
                        adjustments.Add(new SellHarvestQuantity()
                        {
                            HarvestId = harvest[i].Id,
                            HourlyQuantity = harvest[i].RemainingHourlyQuantity,
                        });
                        quantityChange -= harvest[i].RemainingHourlyQuantity;
                        harvest[i].RemainingHourlyQuantity = 0;
                    }
                }
            }
            else
            {
                break;
            }
        }
        return adjustments;
    }
    public async Task RevertRemainingQuantity(ICollection<SellHarvestQuantity> harvestQuantities)
    {
        foreach (var item in harvestQuantities)
        {
            var harvest = await context.Harvests.Where(b => b.Id == item.HarvestId).FirstOrDefaultAsync()
                ?? throw new NotFoundException("Nie mozna znaleźć zbioru.");
            if(item.HourlyQuantity != null)
            {
                harvest.RemainingHourlyQuantity += item.HourlyQuantity ?? 00m;
            }
            if (item.CollectingQuantity != null)
            {
                harvest.RemainingCollectingQuantity += item.CollectingQuantity ?? 00m;
            }
            if (item.CollectingQuantityAdditional != null)
            {
                harvest.RemainingQuantityAdditional += item.CollectingQuantityAdditional ?? 00m;
            }

        }
    }
    public async Task<decimal> GetAvailableQuantity(int plantId)
    {
        var harvests = await context.Harvests
            .Where(b => b.IsActive &&
                (b.RemainingCollectingQuantity > 0 || b.RemainingHourlyQuantity > 0 || b.RemainingQuantityAdditional > 0) &&
                b.Workday.PlantId == plantId)
            .ToListAsync();
        return harvests.Sum(h => h.RemainingCollectingQuantity + h.RemainingHourlyQuantity + h.RemainingQuantityAdditional);
    }
}
