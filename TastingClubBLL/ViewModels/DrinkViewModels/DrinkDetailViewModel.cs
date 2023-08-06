using TastingClubBLL.ViewModels.DrinkBrandViewModels;
using TastingClubBLL.ViewModels.DrinkSuitableProductViewModels;
using TastingClubBLL.ViewModels.DrinkTypeViewModels;
using TastingClubBLL.ViewModels.PhotoViewModels;
using TastingClubBLL.ViewModels.ProducerViewModels;
using TastingClubBLL.ViewModels.ProducingCountryViewModels;
using TastingClubDAL.Models;

namespace TastingClubBLL.ViewModels.DrinkViewModels
{
    public class DrinkDetailViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public float Rating { get; set; }
        public float AlcoholPercentage { get; set; }
        public string Color { get; set; }
        public string Taste { get; set; }
        public string Aroma { get; set; }
        public string GastronomicCombination { get; set; }

        public DrinkTypeGeneralViewModel DrinkType { get; set; }
        public ProducingCountryDetailViewModel ProducingCountry { get; set; }
        public ProducerGeneralViewModel Producer { get; set; }
        public DrinkBrandGeneralViewModel DrinkBrand { get; set; }
        public List<SuitableProductGeneralViewModel> SuitableProducts { get; } = new();
        public List<DrinkPhotoViewModel> DrinkPhotos { get; } = new();
    }
}