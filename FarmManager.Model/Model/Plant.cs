using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model;

public class Plant : DetailedEntity
{
    public Variety Variety { get; set; } = null!;
}
