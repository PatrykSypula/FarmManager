using FarmManager.Model.Model;

namespace FarmManager.Services.Interfaces;

public interface ISellService
{
    Task<ICollection<Sell>> GetAll(bool activeOnly = true);
    Task<Sell> Get(int id);
    Task Add(Sell entity);
    Task Delete(int id);
    Task<ICollection<SellHarvestQuantity>> AdjustRemainingQuantity(decimal quantityChange, int plantId);
    Task RevertRemainingQuantity(ICollection<SellHarvestQuantity> harvestQuantities);
    Task<decimal> GetAvailableQuantity(int plantId);
}
