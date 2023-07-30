using System.ComponentModel.DataAnnotations;
using TastingClubDAL.Constants.ModelConstants.DrinkConstants;

namespace TastingClubBLL.DTOs.DrinkDTOs
{
    public class DrinkDtoForUpdate
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //public float Volume { get; set; }
        [Range((double)DrinkValueConstraintConstants.MinPrice, (double)decimal.MaxValue)]
        public decimal Price { get; set; }
        [Range(DrinkValueConstraintConstants.MinAlcoholPercentage, DrinkValueConstraintConstants.MaxAlcoholPercentage)]
        public string Color { get; set; }
        public string Taste { get; set; }
        public string Aroma { get; set; }
        public string Gastronomy { get; set; }

        public int DrinkTypeId { get; set; }
        public int ProducingCountryId { get; set; }
        public int ProducerId { get; set; }
        public int DrinkBrandId { get; set; }
    }
}