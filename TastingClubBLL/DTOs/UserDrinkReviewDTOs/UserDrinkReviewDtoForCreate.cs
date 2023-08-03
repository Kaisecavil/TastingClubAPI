using System.ComponentModel.DataAnnotations;
using TastingClubDAL.Constants.ModelConstants.UserDrinkReviewConstants;

namespace TastingClubBLL.DTOs.UserDrinkReviewDTOs
{
    public class UserDrinkReviewDtoForCreate
    {
        public string Review { get; set; }

        [Range(UserDrinkReviewValueConstraintConstants.MinRating, UserDrinkReviewValueConstraintConstants.MaxRating)]
        public byte Rating { get; set; }
        public DateTime DateOfDegustation { get; set; }
        public int DrinkId { get; set; }
        public int? EventId { get; set; }
    }
}