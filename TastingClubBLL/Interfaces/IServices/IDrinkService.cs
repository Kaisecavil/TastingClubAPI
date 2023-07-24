using TastingClubBLL.DTOs.DrinkDTOs;
using TastingClubBLL.ViewModels.DrinkViewModels;

namespace TastingClubBLL.Interfaces.IServices
{
    public interface IDrinkService
    {
        Task<int> CreateDrinkAsync(DrinkDtoForCreate drinkDto);
        Task DeleteDrinkAsync(int id);
        Task<List<DrinkGeneralViewModel>> GetAllDrinksAsync();
        Task<DrinkDetailViewModel> GetDrinkAsync(int id);
        Task UpdateDrinkAsync(DrinkDtoForUpdate drinkDto);
    }
}