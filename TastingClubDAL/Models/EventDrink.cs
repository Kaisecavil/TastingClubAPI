using TastingClubDAL.Models.Base;

namespace TastingClubDAL.Models
{
    public class EventDrink : BaseModel
    {
        public int EventId { get; set; }
        public int DrinkId { get; set; }
        public virtual Event Event { get; set; }
        public virtual Drink Drink { get; set; }
    }
}