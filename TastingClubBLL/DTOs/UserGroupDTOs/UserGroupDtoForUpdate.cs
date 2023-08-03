using TastingClubDAL.Enums;

namespace TastingClubBLL.DTOs.UserGroupDTOs
{
    public class UserGroupDtoForUpdate
    {
        public int Id { get; set; }
        public GroupMembershipStatus Status { get; set; }
        public UserGroupRole Role { get; set; }
    }
}
