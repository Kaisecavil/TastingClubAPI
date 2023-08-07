using TastingClubDAL.Enums;
using TastingClubDAL.Models.Base;

namespace TastingClubDAL.Models
{
    public class Event : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public EventStatus Status { get; set; }
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
        public virtual List<EventParticipant> EventParticipants { get; } = new();
        public virtual List<EventDrink> EventDrinks { get; } = new();
        public virtual List<Drink> Drinks { get; } = new(); //??
    }
}
