using TastingClubBLL.ViewModels.ApplicationUserViewModels;
using TastingClubDAL.Enums;

namespace TastingClubBLL.ViewModels.UserGroupViewModels
{
    public class UserGroupGeneralViewModel
    {
        public int Id { get; set; }
        public GroupMembershipStatus Status { get; set; }
        public UserGroupRole Role { get; set; }
        public ApplicationUserGeneralViewModel User { get; set; }
    }
}
