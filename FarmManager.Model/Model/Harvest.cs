using System.ComponentModel.DataAnnotations.Schema;
using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Harvest : BaseEntity
{
    public Workday Workday { get; set; } = null!;
    public decimal CollectingQuantity { get; set; } 
    public decimal CollectingQuantityAdditional { get; set; }
    public decimal HourlyQuantity { get; set; }
    public decimal RemainingCollectingQuantity { get; set; }
    public decimal RemainingQuantityAdditional { get; set; }
    public decimal RemainingHourlyQuantity { get; set; }

}
