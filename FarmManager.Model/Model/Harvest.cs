using System.ComponentModel.DataAnnotations.Schema;
using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Harvest : BaseEntity
{
    public Workday Workday { get; set; } = null!;
    private decimal _collectingQuantity;
    public decimal CollectingQuantity
    {
        get => _collectingQuantity;
        set => _collectingQuantity = Math.Round(value, 2);
    }

    private decimal _collectingQuantityAdditional;
    public decimal CollectingQuantityAdditional
    {
        get => _collectingQuantityAdditional;
        set => _collectingQuantityAdditional = Math.Round(value, 2);
    }

    private decimal _hourlyQuantity;
    public decimal HourlyQuantity
    {
        get => _hourlyQuantity;
        set => _hourlyQuantity = Math.Round(value, 2);
    }

    private decimal _remainingCollectingQuantity;
    public decimal RemainingCollectingQuantity
    {
        get => _remainingCollectingQuantity;
        set => _remainingCollectingQuantity = Math.Round(value, 2);
    }

    private decimal _remainingQuantityAdditional;
    public decimal RemainingQuantityAdditional
    {
        get => _remainingQuantityAdditional;
        set => _remainingQuantityAdditional = Math.Round(value, 2);
    }

    private decimal _remainingHourlyQuantity;
    public decimal RemainingHourlyQuantity
    {
        get => _remainingHourlyQuantity;
        set => _remainingHourlyQuantity = Math.Round(value, 2);
    }


}
