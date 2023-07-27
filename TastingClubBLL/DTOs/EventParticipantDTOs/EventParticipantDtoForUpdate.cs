using TastingClubDAL.Enums;

namespace TastingClubBLL.DTOs.EventParticipantDTOs
{
    public class EventParticipantDtoForUpdate
    {
        public int Id { get; set; }
        public EventPartisipantStatus EventParticipantStatus { get; set; }
    }
}
