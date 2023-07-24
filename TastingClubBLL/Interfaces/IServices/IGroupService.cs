using TastingClubBLL.DTOs.GroupDTOs;
using TastingClubBLL.ViewModels.GroupViewModels;

namespace TastingClubBLL.Interfaces.IServices
{
    public interface IGroupService
    {
        Task<int> CreateGroupAsync(GroupDtoForCreate groupDto);
        Task DeleteGroupAsync(int id);
        Task<List<GroupGeneralViewModel>> GetAllGroupsAsync();
        Task<GroupDetailViewModel> GetGroupAsync(int id);
        Task UpdateGroupAsync(GroupDtoForUpdate groupDto);
    }
}