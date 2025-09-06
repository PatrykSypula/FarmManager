using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Harvest : BaseEntity
{
    public Plant Plant { get; set; } = new Plant();
    public double Quantity { get; set; }
}
