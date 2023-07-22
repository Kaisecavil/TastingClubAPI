using TastingClubDAL.Models.Base;

namespace TastingClubDAL.Models
{
    public class ProducingCountry : BaseModel
    {
        public string Name { get; set; }

        //public int DrinkId { get; set; }
        public virtual List<Drink> Drinks { get; set; }
    }
}