using TastingClubBLL.DTOs.UserDrinkReviewDTOs;
using TastingClubBLL.ViewModels.UserDrinkReviewViewModels;

namespace TastingClubBLL.Interfaces.IServices
{
    public interface IUserDrinkReviewService
    {
        Task<int> CreateUserDrinkReviewAsync(UserDrinkReviewDtoForCreate userDrinkReviewDto);
        Task DeleteUserDrinkReviewAsync(int id);
        Task<List<UserDrinkReviewGeneralViewModel>> GetAllUserDrinkReviewsAsync();
        Task<UserDrinkReviewDetailViewModel> GetUserDrinkReviewAsync(int id);
        Task UpdateUserDrinkReviewAsync(UserDrinkReviewDtoForUpdate userDrinkReviewDto);
    }
}