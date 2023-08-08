using System.ComponentModel;
using TastingClubBLL.ViewModels.EventViewModels;
using TastingClubBLL.ViewModels.PhotoViewModels;
using TastingClubBLL.ViewModels.UserGroupViewModels;
using TastingClubDAL.Constants.ModelConstants.GroupConstants;

namespace TastingClubBLL.ViewModels.GroupViewModels
{
    public class GroupDetailViewModel
    {
        public int Id { get; set; }

        [DefaultValue(GroupDefaultValueConstatns.TitleDefaultValue)]
        public string Title { get; set; }

        [DefaultValue(GroupDefaultValueConstatns.DescriptionDefaultValue)]
        public string Description { get; set; }

        [DefaultValue(GroupDefaultValueConstatns.AccessTypeDefaultValueString)]
        public string AccessType { get; set; }
        public List<EventGeneralViewModel> Events { get; } = new();
        public List<UserGroupGeneralViewModel> UserGroups { get; } = new();
        public List<GroupPhotoViewModel> Photos { get; } = new();
    }
}