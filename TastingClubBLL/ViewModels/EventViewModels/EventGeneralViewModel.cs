using System.ComponentModel;
using TastingClubDAL.Constants.ModelConstants.EventConstants;

namespace TastingClubBLL.ViewModels.EventViewModels
{
    public class EventGeneralViewModel
    {
        public int Id { get; set; }

        [DefaultValue(EventDefaultValueConstatns.TitleDefaultValue)]
        public string Title { get; set; }

        [DefaultValue(EventDefaultValueConstatns.DescriptionDefaultValue)]
        public string Description { get; set; }
        public DateTime Date { get; set; }

        [DefaultValue(EventDefaultValueConstatns.StatusDefaultValueString)]
        public string Status { get; set; }
    }
}