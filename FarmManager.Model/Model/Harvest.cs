using System.ComponentModel.DataAnnotations.Schema;
using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Harvest : BaseEntity
{
    public Workday Workday { get; set; } = null!;
    public double CollectingQuantity { get; set; } 
    public double CollectingQuantityAdditional { get; set; }
    public double HourlyQuantity { get; set; }
    public double RemainingCollectingQuantity { get; set; }
    public double RemainingQuantityAdditional { get; set; }
    public double RemainingHourlyQuantity { get; set; }

}
