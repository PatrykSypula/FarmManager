using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model
{
    public class Harvest : BaseEntity
    {
        public Plant Plant { get; set; } = null!;
        public double Quantity { get; set; }
    }
}
