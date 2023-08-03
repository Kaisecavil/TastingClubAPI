using Microsoft.AspNetCore.Identity;

namespace TastingClubDAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public virtual List<UserDrinkReview> UserDrinkReviews { get; } = new();
        public virtual List<UserGroup> UserGroups { get; } = new();
        public virtual List<EventParticipant> EventParticipants { get; } = new();

    }
}
