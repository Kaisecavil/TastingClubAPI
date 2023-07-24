namespace TastingClubBLL.DTOs.EventDTOs
{
    public class EventDtoForCreate
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int GruopId { get; set; }
    }
}