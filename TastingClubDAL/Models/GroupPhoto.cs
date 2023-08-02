using TastingClubDAL.Interfaces.IModel;
using TastingClubDAL.Models.Base;

namespace TastingClubDAL.Models
{
    public class GroupPhoto : BaseModel, IPhotoModel
    {
        public int PhotoId { get; set; }
        public virtual Photo Photo { get; set; }
        public int GroupId { get; set; }
        public int EntityId { get; set; }
        public virtual Group Group { get; set; }
    }
}