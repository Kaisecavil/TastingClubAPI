using TastingClubDAL.Models.Base;

namespace TastingClubDAL.Models
{
    public class DrinkType : BaseModel
    {
        public string Title { get; set; }

        //public int DrinkId { get; set; }
        public virtual List<Drink> Drinks { get; set; }
    }
}