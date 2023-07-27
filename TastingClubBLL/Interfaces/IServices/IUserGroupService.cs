using TastingClubBLL.DTOs.UserGroupDTOs;
using TastingClubBLL.ViewModels.DrinkViewModels;

namespace TastingClubBLL.Interfaces.IServices
{
    public interface IUserGroupService
    {
        Task<int> CreateUserGroupsAsync(List<UserGroupDtoForCreate> userGroupsDtos);
        Task DeleteUserGroupsAsync(List<int> ids);
        Task<List<DrinkGeneralViewModel>> GetAllUserGroupsAsync(int eventId);
    }
}