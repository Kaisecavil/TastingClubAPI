using TastingClubDAL.Enums;
using TastingClubDAL.Models.Base;

namespace TastingClubDAL.Models
{
    public class Group : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public GroupAccessType AccessType { get; set; }
        //public string AdminUserId { get; set; } cycles??
        //public virtual ApplicationUser AdminUser { get; set; }
        public virtual List<Event> Events { get; } = new();
    }
}
