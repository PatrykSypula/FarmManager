using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Fertilizer : BaseEntity, IDescribable
{
    public double Quantity { get; set; } = 0;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
