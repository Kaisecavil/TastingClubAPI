using TastingClubBLL.DTOs.UserGroupDTOs;
using TastingClubBLL.ViewModels.ApplicationUserViewModels;
using TastingClubBLL.ViewModels.GroupViewModels;
using TastingClubDAL.Models;

namespace TastingClubBLL.Interfaces.IServices
{
    public interface IUserGroupService
    {
        Task CreateGroupAdmin(UserGroupDtoForCreate userGroupDto);
        Task<int> CreateUserGroupsAsync(List<UserGroupDtoForCreate> userGroupsDtos);
        Task DeleteUserGroupsAsync(List<int> ids);
        Task<List<ApplicationUserGeneralViewModel>> GetAllGroupUsersAsync(int groupId);
        Task<List<GroupGeneralViewModel>> GetAllUserGroupsAsync(string userId);
        Task<ApplicationUser> GetGroupAdminAsync(int groupId);
    }
}