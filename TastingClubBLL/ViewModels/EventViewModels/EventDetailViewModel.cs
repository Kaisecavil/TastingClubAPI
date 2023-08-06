using TastingClubBLL.ViewModels.DrinkViewModels;
using TastingClubBLL.ViewModels.EventParticipantViewModels;
using TastingClubBLL.ViewModels.GroupViewModels;
using TastingClubDAL.Enums;
using TastingClubDAL.Models;

namespace TastingClubBLL.ViewModels.EventViewModels
{
    public class EventDetailViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public GroupGeneralViewModel Group { get; set; }
        public List<EventParticipantGeneralViewModel> EventParticipants { get; } = new();
        public List<DrinkGeneralViewModel> Drinks { get; } = new();
    }
}