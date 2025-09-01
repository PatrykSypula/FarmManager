using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model
{
    public class Spraying : BaseEntity
    {
        public Plant Plant { get; set; } = null!;
        public Fertilizer Fertilizer { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
