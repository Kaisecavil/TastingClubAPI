using System.ComponentModel.DataAnnotations;
using TastingClubDAL.Constants.ModelConstants.DrinkConstants;

namespace TastingClubBLL.DTOs.DrinkDTOs
{
    public class DrinkDtoForCreate
    {
        public string Title { get; set; }
        public string Description { get; set; }
        //public float Volume { get; set; }
        [Range((double)DrinkValueConstraintConstants.MinPrice, (double)decimal.MaxValue)]
        public decimal Price { get; set; }
        [Range(DrinkValueConstraintConstants.MinAlcoholPercentage,DrinkValueConstraintConstants.MaxAlcoholPercentage)]
        public float AlcoholPercentage { get; set; }
        public string Color { get; set; }
        public string Taste { get; set; }
        public string Aroma { get; set; }
        public string Gastronomy { get; set; }

        public int DrinkTypeId { get; set; }
        public int ProducingCountryId { get; set; }
    }
}