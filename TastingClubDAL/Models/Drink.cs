using System.ComponentModel.DataAnnotations;
using TastingClubDAL.Constants.ModelConstants.DrinkConstants;
using TastingClubDAL.Models.Base;

namespace TastingClubDAL.Models
{
    public class Drink : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        [Range((double)DrinkValueConstraintConstants.MinPrice, (double)decimal.MaxValue)]
        public decimal Price { get; set; }

        [Range(DrinkValueConstraintConstants.MinAlcoholPercentage, DrinkValueConstraintConstants.MaxAlcoholPercentage)]
        public float AlcoholPercentage { get; set; }
        [Range(DrinkValueConstraintConstants.MinRating, DrinkValueConstraintConstants.MaxRating)]
        public float Rating { get; set; }
        public string Color { get; set; }
        public string Taste { get; set; }
        public string Aroma { get; set; }
        public string Gastronomy { get; set; }

        public int DrinkTypeId { get; set; }
        public int ProducingCountryId { get; set; }
        public int ProducerId { get; set; }
        public int DrinkBrandId { get; set; }
        public virtual DrinkType DrinkType { get; set; }
        public virtual ProducingCountry ProducingCountry { get; set; }
        public virtual Producer Producer { get; set; }
        public virtual DrinkBrand DrinkBrand { get; set; }
        public virtual List<DrinkSuitableProduct> DrinkSuitableProducts { get; } = new();
        public virtual List<SuitableProduct> SuitableProducts { get; } = new();
        public virtual List<DrinkPhoto> DrinkPhotos { get; } = new();

    }
}
