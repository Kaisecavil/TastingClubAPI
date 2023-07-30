namespace TastingClubBLL.DTOs.UserDrinkReviewDTOs
{
    public class UserDrinkReviewDtoForUpdate
    {
        public int Id { get; set; }
        public string Review { get; set; }
        public byte Rating { get; set; }
        public DateTime DateOfDegustation { get; set; }

    }
}