using System.ComponentModel;
using TastingClubBLL.ViewModels.DrinkBrandViewModels;
using TastingClubBLL.ViewModels.DrinkSuitableProductViewModels;
using TastingClubBLL.ViewModels.DrinkTypeViewModels;
using TastingClubBLL.ViewModels.PhotoViewModels;
using TastingClubBLL.ViewModels.ProducerViewModels;
using TastingClubBLL.ViewModels.ProducingCountryViewModels;
using TastingClubDAL.Constants.ModelConstants.DrinkConstants;
using TastingClubDAL.Models;

namespace TastingClubBLL.ViewModels.DrinkViewModels
{
    public class DrinkDetailViewModel
    {
        [DefaultValue(DrinkDefaultValueConstants.TitleDefaultValue)]
        public string Title { get; set; }
        [DefaultValue(DrinkDefaultValueConstants.DescriptionDefaultValue)]
        public string Description { get; set; }
        [DefaultValue(DrinkDefaultValueConstants.PriceDefaultValue)]
        public decimal Price { get; set; }
        [DefaultValue(DrinkDefaultValueConstants.RatingDefaultValue)]
        public float Rating { get; set; }
        [DefaultValue(DrinkDefaultValueConstants.AlcoholPercentageDefaultValue)]
        public float AlcoholPercentage { get; set; }
        [DefaultValue(DrinkDefaultValueConstants.ColorDefaultValue)]
        public string Color { get; set; }
        [DefaultValue(DrinkDefaultValueConstants.TasteDefaultValue)]
        public string Taste { get; set; }
        [DefaultValue(DrinkDefaultValueConstants.AromaDefaultValue)]
        public string Aroma { get; set; }
        [DefaultValue(DrinkDefaultValueConstants.GastronomyDefaultValue)]
        public string Gastronomy { get; set; }


        public DrinkTypeGeneralViewModel DrinkType { get; set; }
        public ProducingCountryDetailViewModel ProducingCountry { get; set; }
        public ProducerGeneralViewModel Producer { get; set; }
        public DrinkBrandGeneralViewModel DrinkBrand { get; set; }
        public List<SuitableProductGeneralViewModel> SuitableProducts { get; } = new();
        public List<DrinkPhotoViewModel> DrinkPhotos { get; } = new();
    }
}