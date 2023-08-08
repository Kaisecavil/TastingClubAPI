using System.ComponentModel;
using TastingClubBLL.ViewModels.DrinkViewModels;
using TastingClubBLL.ViewModels.EventParticipantViewModels;
using TastingClubBLL.ViewModels.GroupViewModels;
using TastingClubDAL.Constants.ModelConstants.EventConstants;

namespace TastingClubBLL.ViewModels.EventViewModels
{
    public class EventDetailViewModel
    {
        public int Id { get; set; }

        [DefaultValue(EventDefaultValueConstatns.TitleDefaultValue)]
        public string Title { get; set; }

        [DefaultValue(EventDefaultValueConstatns.DescriptionDefaultValue)]
        public string Description { get; set; }
        public DateTime Date { get; set; }

        [DefaultValue(EventDefaultValueConstatns.StatusDefaultValueString)]
        public string Status { get; set; }
        public GroupGeneralViewModel Group { get; set; }
        public List<EventParticipantGeneralViewModel> EventParticipants { get; } = new();
        public List<DrinkGeneralViewModel> Drinks { get; } = new();
    }
}