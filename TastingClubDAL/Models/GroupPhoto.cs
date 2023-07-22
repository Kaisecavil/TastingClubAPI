using TastingClubDAL.Models.Base;

namespace TastingClubDAL.Models
{
    public class GroupPhoto : BaseModel
    {
        public string PhotoPath { get; set; }
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
    }
}