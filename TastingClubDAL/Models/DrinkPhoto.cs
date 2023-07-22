using TastingClubDAL.Models.Base;

namespace TastingClubDAL.Models
{
    public class DrinkPhoto : BaseModel
    {
        public string PhotoPath { get; set; }
        public int DrinkId { get; set; }
        public virtual Drink Drink { get; set; }
    }
}