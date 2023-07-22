using System.IO;
using TastingClubDAL.Models.Base;

namespace TastingClubDAL.Models
{
    public class Drink : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double AlcoholPercentage { get; set; }
        public string Color { get; set; }
        public string Taste { get; set; }
        public string Aroma { get; set; }
        public string GastronomicCombination { get; set; }

        public int DrinkTypeId { get; set; }
        public int ProducingCountryId { get; set; }
        public virtual DrinkType DrinkType { get; set; }
        public virtual ProducingCountry ProducingCountry { get; set; }
        public virtual List<DrinkSuitableProduct> DrinkSuitableProducts { get; } = new();
        public virtual List<SuitableProduct> SuitableProducts { get; } = new();
        public virtual List<DrinkPhoto> DrinkPhotos { get; } = new();

    }
}
