using TastingClubDAL.Models.Base;

namespace TastingClubDAL.Models
{
    public class Producer : BaseModel
    {
        public string Name { get; set; }
        public virtual List<Drink> Drinks { get; set; }
    }
}
