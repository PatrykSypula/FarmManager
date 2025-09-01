using FarmManager.Model.Model.Base;

namespace FarmManager.Model.Model
{
    public class WorkDayCollecting : BaseEntity
    {
        public Employee Employee { get; set; } = null!;
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
