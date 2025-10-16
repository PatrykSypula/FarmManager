using System.ComponentModel.DataAnnotations.Schema;
using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Fertilizer : BaseEntity, IDescribable
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    private decimal? _quantity;
    [NotMapped]
    public decimal? Quantity { get => _quantity; set => _quantity = value is null ? null : Math.Round(value.Value, 2); }
}
