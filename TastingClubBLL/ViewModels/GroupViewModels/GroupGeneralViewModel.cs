using System.ComponentModel;
using TastingClubBLL.ViewModels.PhotoViewModels;
using TastingClubDAL.Constants.ModelConstants.GroupConstants;

namespace TastingClubBLL.ViewModels.GroupViewModels
{
    public class GroupGeneralViewModel
    {
        public int Id { get; set; }

        [DefaultValue(GroupDefaultValueConstatns.TitleDefaultValue)]
        public string Title { get; set; }

        [DefaultValue(GroupDefaultValueConstatns.DescriptionDefaultValue)]
        public string Description { get; set; }

        [DefaultValue(GroupDefaultValueConstatns.AccessTypeDefaultValueString)]
        public string AccessType { get; set; } 
        public List<GroupPhotoViewModel> Photos { get; } = new();
    }
}