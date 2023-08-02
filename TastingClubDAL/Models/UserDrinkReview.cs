using System.ComponentModel.DataAnnotations;
using TastingClubDAL.Constants.ModelConstants.UserDrinkReviewConstants;
using TastingClubDAL.Enums;
using TastingClubDAL.Models.Base;

namespace TastingClubDAL.Models
{
    public class UserDrinkReview : BaseModel
    {
        public UserDrinkReviewStatus UserDrinkReviewStatus { get; set; }
        public string Review { get; set; }
        [Range(UserDrinkReviewValueConstraintConstants.MinRating,UserDrinkReviewValueConstraintConstants.MaxRating)]
        public byte Rating { get; set; }
        public DateTime DateOfDegustation { get; set;}
        public string UserId { get; set; }
        public int DrinkId { get; set; }
        public int? EventId { get; set; }
        public virtual Drink Drink { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Event? Event { get; set; }
    }
}
