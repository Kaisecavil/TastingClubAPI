using TastingClubDAL.Enums;

namespace TastingClubBLL.DTOs.EventDTOs
{
    public class EventDtoForUpdate
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public EventStatus Status { get; set; }
    }
}