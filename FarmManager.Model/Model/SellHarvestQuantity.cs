using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class SellHarvestQuantity : BaseEntity
{
    public int SellId { get; set; }
    public Sell Sell { get; set; } = null!;
    public int HarvestId { get; set; }
    private decimal? _collectingQuantity;
    public decimal? CollectingQuantity
    {
        get => _collectingQuantity;
        set => _collectingQuantity = value is null ? null : Math.Round(value.Value, 2);
    }

    private decimal? _collectingQuantityAdditional;
    public decimal? CollectingQuantityAdditional
    {
        get => _collectingQuantityAdditional;
        set => _collectingQuantityAdditional = value is null ? null : Math.Round(value.Value, 2);
    }

    private decimal? _hourlyQuantity;
    public decimal? HourlyQuantity
    {
        get => _hourlyQuantity;
        set => _hourlyQuantity = value is null ? null : Math.Round(value.Value, 2);
    }
}

