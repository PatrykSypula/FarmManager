using System.ComponentModel.DataAnnotations.Schema;
using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Fertilizer : BaseEntity, IDescribable
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    [NotMapped]
    public decimal? Quantity { get; set; }
}
