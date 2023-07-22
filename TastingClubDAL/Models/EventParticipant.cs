using TastingClubDAL.Models.Base;
using TastingClubDAL.Enums;

namespace TastingClubDAL.Models
{
    public class EventParticipant : BaseModel
    {
        public EventPartisipantStatus Status { get; set; }
        public int EventId { get; set; }
        public string UserId { get; set; }
        public virtual Event Event { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}