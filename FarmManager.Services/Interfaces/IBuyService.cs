using FarmManager.Model.Model;

namespace FarmManager.Services.Interfaces;

public interface IBuyService
{
    Task<ICollection<Buy>> GetAll(bool activeOnly = true);
    Task<Buy> Get(int id);
    Task Add(Buy entity);
    Task Update(Buy entity);
    Task Delete(int id);
    Task<ICollection<SprayingBuyQuantity>> AdjustRemainingQuantity(decimal quantityChange, int fertilizerId);
    Task RevertRemainingQuantity(ICollection<SprayingBuyQuantity> buyQuantities);
}
