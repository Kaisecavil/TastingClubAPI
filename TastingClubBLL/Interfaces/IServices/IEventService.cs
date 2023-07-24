using TastingClubBLL.DTOs.EventDTOs;
using TastingClubBLL.ViewModels.EventViewModels;

namespace TastingClubBLL.Interfaces.IServices
{
    public interface IEventService
    {
        public Task<EventDetailViewModel> GetEventAsync(int id);
        public Task<List<EventGeneralViewModel>> GetAllEventsAsync();
        public Task<int> CreateEventAsync(EventDtoForCreate eventDto);
        public Task UpdateEventAsync(EventDtoForUpdate eventDto);
        public Task DeleteEventAsync(int id);
    }
}
