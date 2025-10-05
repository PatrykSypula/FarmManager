using System.ComponentModel.DataAnnotations.Schema;
using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Harvest : BaseEntity
{
    private double _collectingQuantity;
    private double _collectingQuantityAdditional;
    private double _hourlyQuantity;

    public Workday Workday { get; set; } = null!;
    public double CollectingQuantity
    {
        get => _collectingQuantity;
        set
        {
            _collectingQuantity = value;
            UpdateTotalQuantity();
        }
    }

    public double CollectingQuantityAdditional
    {
        get => _collectingQuantityAdditional;
        set
        {
            _collectingQuantityAdditional = value;
            UpdateTotalQuantity();
        }
    }

    public double HourlyQuantity
    {
        get => _hourlyQuantity;
        set
        {
            _hourlyQuantity = value;
            UpdateTotalQuantity();
        }
    }

    [NotMapped]
    public double TotalQuantity { get; private set; }
    private void UpdateTotalQuantity()
    {
        TotalQuantity = CollectingQuantity + CollectingQuantityAdditional + HourlyQuantity;
    }
}
