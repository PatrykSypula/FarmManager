using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Season : BaseEntity, IDescribable
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int PlantId { get; set; }
    public Plant Plant { get; set; } = null!;
}
