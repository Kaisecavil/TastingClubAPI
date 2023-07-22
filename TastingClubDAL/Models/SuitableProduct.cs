using TastingClubDAL.Models.Base;

namespace TastingClubDAL.Models
{
    public class SuitableProduct : BaseModel
    {
        public string Title { get; set; }
        public virtual List<Drink> Drinks { get; } = new();
    }
}