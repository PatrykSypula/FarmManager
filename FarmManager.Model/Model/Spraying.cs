using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Spraying : BaseEntity
{
    public Plant Plant { get; set; } = new Plant();
    public Fertilizer Fertilizer { get; set; } = new Fertilizer();
    public int Quantity { get; set; }
}
