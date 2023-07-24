using TastingClubBLL.DTOs.EventParticipantDTOs;
using TastingClubBLL.ViewModels.DrinkViewModels;

namespace TastingClubBLL.Interfaces.IServices
{
    public interface IEventParticipantService
    {
        Task<int> CreateEventParticipantAsync(List<EventParticipantDtoForCreate> eventParticipantDtos);
        Task DeleteEventParticipantAsync(List<int> ids);
        Task<List<DrinkGeneralViewModel>> GetAllEventParticipantAsync(int eventId);
    }
}