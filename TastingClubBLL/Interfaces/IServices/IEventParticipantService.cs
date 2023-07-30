using TastingClubBLL.DTOs.EventParticipantDTOs;
using TastingClubBLL.ViewModels.ApplicationUserViewModels;

namespace TastingClubBLL.Interfaces.IServices
{
    public interface IEventParticipantService
    {
        Task<int> CreateEventParticipantAsync(List<EventParticipantDtoForCreate> eventParticipantDtos);
        Task DeleteEventParticipantAsync(List<int> ids);
        Task<List<ApplicationUserGeneralViewModel>> GetAllEventParticipantAsync(int eventId);
    }
}