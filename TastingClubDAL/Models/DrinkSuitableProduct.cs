using TastingClubDAL.Models.Base;

namespace TastingClubDAL.Models
{
    public class DrinkSuitableProduct : BaseModel
    {
        public int DrinkId { get; set; }
        public int SuitableProductId { get; set; }
        public virtual Drink Drink { get; set; }
        public virtual SuitableProduct SuitableProduct { get; set; }
    }
}