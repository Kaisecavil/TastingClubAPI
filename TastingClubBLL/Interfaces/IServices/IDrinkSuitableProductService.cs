using TastingClubBLL.DTOs.DrinkSuitableProductDTOs;
using TastingClubBLL.ViewModels.DrinkSuitableProductViewModels;
using TastingClubBLL.ViewModels.DrinkViewModels;

namespace TastingClubBLL.Interfaces.IServices
{
    public interface IDrinkSuitableProductService
    {
        Task<int> CreateDrinkSuitableProductsAsync(List<DrinkSuitableProductDtoForCreate> drinkSuitableProductsDtos);
        Task DeleteDrinkSuitableProductsAsync(List<int> ids);
        Task<List<SuitableProductGeneralViewModel>> GetAllDrinkSuitableProductsAsync(int drinkId);
        Task<List<DrinkGeneralViewModel>> GetAllSuitableProductDrinksAsync(int suitableProductId);
    }
}