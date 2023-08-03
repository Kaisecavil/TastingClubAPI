using AutoMapper;
using System.Net;
using TastingClubBLL.DTOs.GroupDTOs;
using TastingClubBLL.DTOs.UserGroupDTOs;
using TastingClubBLL.Exceptions;
using TastingClubBLL.Interfaces.IProvider;
using TastingClubBLL.Interfaces.IServices;
using TastingClubBLL.ViewModels.GroupViewModels;
using TastingClubDAL.Interfaces;
using TastingClubDAL.Models;

namespace TastingClubBLL.Services
{
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IApplicationUserProvider _userProvider;
        private readonly IUserGroupService _userGroupService;

        public GroupService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IApplicationUserProvider userProvider)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userProvider = userProvider;
        }

        public async Task<int> CreateGroupAsync(GroupDtoForCreate groupDto)
        {
            var mappedGroup = _mapper.Map<Group>(groupDto);
            await _unitOfWork.Groups.CreateAsync(mappedGroup);
            var currentUserId = await _userProvider.GetUserIdAsync();
            _userGroupService.CreateGroupAdmin(new UserGroupDtoForCreate()
            {
                UserId = currentUserId,
                GroupId = mappedGroup.Id
            });
            await _unitOfWork.SaveAsync();
            return mappedGroup.Id;
        }

        public async Task DeleteGroupAsync(int id)
        {
            if (!await _unitOfWork.Groups.EntityExistsAsync(id))
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, "Group not found");
            }

            var currentUserId = await _userProvider.GetUserIdAsync();
            var groupToDeleteAdmin = await _userGroupService.GetGroupAdminAsync(id);
            if (groupToDeleteAdmin.Id != currentUserId)
            {
                throw new HttpStatusException(HttpStatusCode.Forbidden, "You can't delete group due to a lack of rights");
            }

            await _unitOfWork.Groups.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<GroupGeneralViewModel>> GetAllGroupsAsync()
        {
            var groups = await _unitOfWork.Groups.GetAllAsync();
            return _mapper.Map<List<GroupGeneralViewModel>>(groups);
        }

        public async Task<GroupDetailViewModel> GetGroupAsync(int id)
        {
            var entity = await _unitOfWork.Groups.GetAsync(id);
            if (entity == null)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, "Group not found");
            }
            return _mapper.Map<GroupDetailViewModel>(entity);
        }

        public async Task UpdateGroupAsync(GroupDtoForUpdate groupDto)
        {
            if (!await _unitOfWork.Groups.EntityExistsAsync(groupDto.Id))
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, "Group not found");
            }

            var currentUserId = await _userProvider.GetUserIdAsync();
            var groupToUpdateAdmin = await _userGroupService.GetGroupAdminAsync(groupDto.Id);
            if (groupToUpdateAdmin.Id != currentUserId)
            {
                throw new HttpStatusException(HttpStatusCode.Forbidden, "You can't update group due to a lack of rights");
            }

            await _unitOfWork.Groups.UpdateAsync(_mapper.Map<Group>(groupDto));
            await _unitOfWork.SaveAsync();
        }
    }
}

