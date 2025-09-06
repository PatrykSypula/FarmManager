using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Fertilizer : BaseEntity, IDescribable
{
    public int Quantity { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
