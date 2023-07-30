using TastingClubBLL.DTOs.UserGroupDTOs;
using TastingClubBLL.ViewModels.ApplicationUserViewModels;
using TastingClubBLL.ViewModels.GroupViewModels;

namespace TastingClubBLL.Interfaces.IServices
{
    public interface IUserGroupService
    {
        Task<int> CreateUserGroupsAsync(List<UserGroupDtoForCreate> userGroupsDtos);
        Task DeleteUserGroupsAsync(List<int> ids);
        Task<List<ApplicationUserGeneralViewModel>> GetAllGroupUsersAsync(int groupId);
        Task<List<GroupGeneralViewModel>> GetAllUserGroupsAsync(string userId);
    }
}