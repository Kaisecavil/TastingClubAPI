namespace TastingClubBLL.DTOs.UserDrinkReviewDTOs
{
    public class UserDrinkReviewDtoForUpdate
    {
        public int Id { get; set; }
        public string Review { get; set; }
        public byte Rating { get; set; }
        public DateTime DateOfDegustation { get; set; }
        //public string UserId { get; set; }
        public int DrinkId { get; set; }
        public int? EventId { get; set; }
    }
}