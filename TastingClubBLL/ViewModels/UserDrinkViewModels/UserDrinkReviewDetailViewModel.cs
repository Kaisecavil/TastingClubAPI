using System.ComponentModel;
using TastingClubBLL.ViewModels.ApplicationUserViewModels;
using TastingClubBLL.ViewModels.DrinkViewModels;
using TastingClubBLL.ViewModels.EventViewModels;
using TastingClubDAL.Constants.ModelConstants.UserDrinkReviewConstants;

namespace TastingClubBLL.ViewModels.UserDrinkReviewViewModels
{
    public class UserDrinkReviewDetailViewModel
    {
        [DefaultValue(UserDrinkReviewDefaultValueConstants.UserDrinkReviewStatusDefaultValueString)]
        public string UserDrinkReviewStatus { get; set; }

        [DefaultValue(UserDrinkReviewDefaultValueConstants.ReviewDefaultValue)]
        public string Review { get; set; }

        [DefaultValue(UserDrinkReviewDefaultValueConstants.UserDrinkReviewStatusDefaultValue)]
        public byte Rating { get; set; }
        public DateTime DateOfDegustation { get; set; }
        public DrinkGeneralViewModel Drink { get; set; }
        public ApplicationUserGeneralViewModel User { get; set; }
        public EventGeneralViewModel? Event { get; set; }
    }
}