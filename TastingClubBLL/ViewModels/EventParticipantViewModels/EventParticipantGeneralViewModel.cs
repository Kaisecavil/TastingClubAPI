using TastingClubBLL.ViewModels.ApplicationUserViewModels;
using TastingClubDAL.Enums;

namespace TastingClubBLL.ViewModels.EventParticipantViewModels
{
    public class EventParticipantGeneralViewModel
    {
        public int Id { get; set; }
        public EventPartisipantStatus Status { get; set; }
        public int EventId { get; set; }
        public string UserId { get; set; }
        public ApplicationUserGeneralViewModel User { get; set; }
    }
}