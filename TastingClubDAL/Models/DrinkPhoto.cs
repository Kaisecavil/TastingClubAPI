using TastingClubDAL.Interfaces.IModel;
using TastingClubDAL.Models.Base;

namespace TastingClubDAL.Models
{
    public class DrinkPhoto : BaseModel, IPhotoModel
    {
        public int PhotoId { get; set; }
        public virtual Photo Photo { get; set; }
        public int DrinkId { get; set; }
        public virtual Drink Drink { get; set; }
    }
}