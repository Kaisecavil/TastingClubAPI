using TastingClubBLL.DTOs.EventDrinkDTOs;
using TastingClubBLL.ViewModels.DrinkViewModels;

namespace TastingClubBLL.Interfaces.IServices
{
    public interface IEventDrinkService
    {
        Task<int> CreateEventDrinksAsync(List<EventDrinkDtoForCreate> eventDrinksDtos);
        Task DeleteEventDrinksAsync(List<int> ids);
        Task<List<DrinkGeneralViewModel>> GetAllEventDrinksAsync(int eventId);
    }
}