using TastingClubBLL.ViewModels.ApplicationUserViewModels;

namespace TastingClubBLL.ViewModels.EventParticipantViewModels
{
    public class EventParticipantGeneralViewModel
    {
        public int Id { get; set; }
        public ApplicationUserGeneralViewModel Participant { get; set; }
    }
}