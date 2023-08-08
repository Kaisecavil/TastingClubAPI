using System.ComponentModel;
using TastingClubBLL.ViewModels.ApplicationUserViewModels;
using TastingClubBLL.ViewModels.DrinkViewModels;
using TastingClubDAL.Constants.ModelConstants.UserDrinkReviewConstants;

namespace TastingClubBLL.ViewModels.UserDrinkReviewViewModels
{
    public class UserDrinkReviewGeneralViewModel
    {
        [DefaultValue(UserDrinkReviewDefaultValueConstants.UserDrinkReviewStatusDefaultValueString)]
        public string UserDrinkReviewStatus { get; set; }

        [DefaultValue(UserDrinkReviewDefaultValueConstants.RatingDefaultValue)]
        public byte Rating { get; set; }
        public DrinkGeneralViewModel Drink { get; set; }
        public ApplicationUserGeneralViewModel User { get; set; }
    }
}