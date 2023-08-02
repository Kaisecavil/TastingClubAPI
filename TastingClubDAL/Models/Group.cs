using TastingClubDAL.Enums;
using TastingClubDAL.Interfaces.IModel;
using TastingClubDAL.Models.Base;

namespace TastingClubDAL.Models
{
    public class Group : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public GroupAccessType AccessType { get; set; }
        public virtual List<Event> Events { get; } = new();
        public virtual List<GroupPhoto> Photos { get; } = new();
    }
}
