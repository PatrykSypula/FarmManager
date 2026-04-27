using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;
public class Investment : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int? PlantId { get; set; }
    public Plant? Plant { get; set; }

    private decimal _price;
    public decimal Price { get => _price; set => _price = Math.Round(value, 2); }
    public DateOnly Date { get; set; }
    public string? Description { get; set; }
}
