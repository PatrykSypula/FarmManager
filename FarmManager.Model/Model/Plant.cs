using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Plant : BaseEntity, IDescribable
{
    public int VarietyId { get; set; }
    public Variety Variety { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
