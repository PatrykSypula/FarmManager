using System.ComponentModel.DataAnnotations.Schema;
using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Plant : BaseEntity, IDescribable
{
    private decimal? _quantity;
    [NotMapped]
    public decimal? Quantity { get => _quantity; set => _quantity = value is null ? null : Math.Round(value.Value, 2); }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
