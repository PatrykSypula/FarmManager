using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Spraying : BaseEntity
{
    public int PlantId { get; set; }
    public Plant Plant { get; set; } = null!;
    public int FertilizerId { get; set; }
    public Fertilizer Fertilizer { get; set; } = null!;
    public int Quantity { get; set; }
}
