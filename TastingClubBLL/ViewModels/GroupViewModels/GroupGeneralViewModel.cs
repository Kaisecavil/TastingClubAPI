using TastingClubBLL.ViewModels.EventViewModels;
using TastingClubBLL.ViewModels.PhotoViewModels;
using TastingClubDAL.Enums;
using TastingClubDAL.Models;

namespace TastingClubBLL.ViewModels.GroupViewModels
{
    public class GroupGeneralViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string AccessType { get; set; } //@
        public List<EventGeneralViewModel> Events { get; } = new();
        public List<GroupPhotoViewModel> Photos { get; } = new();
    }
}