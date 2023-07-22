using TastingClubDAL.Enums;
using TastingClubDAL.Models.Base;

namespace TastingClubDAL.Models
{
    public class UserGroup : BaseModel
    {
        public GroupMembershipStatus Status { get; set; }
        public UserGroupRole Role { get; set; }
        public int GroupId { get; set; }
        public string UserId { get; set; }
        public virtual Group Group { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}