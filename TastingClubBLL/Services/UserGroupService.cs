using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TastingClubBLL.DTOs.UserGroupDTOs;
using TastingClubBLL.Exceptions;
using TastingClubBLL.Interfaces.IServices;
using TastingClubBLL.ViewModels.ApplicationUserViewModels;
using TastingClubBLL.ViewModels.GroupViewModels;
using TastingClubDAL.Enums;
using TastingClubDAL.Interfaces;
using TastingClubDAL.Models;

namespace TastingClubBLL.Services
{
    public class UserGroupService : IUserGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserGroupService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> CreateUserGroupsAsync(List<UserGroupDtoForCreate> userGroupsDtos)
        {
            var mappedUserGroups = _mapper.Map<List<UserGroup>>(userGroupsDtos);
            await _unitOfWork.UserGroups.CreateRangeAsync(mappedUserGroups);
            await _unitOfWork.SaveAsync();
            return 1;
        }

        public async Task UpdateUserGroupsAsync(UserGroupDtoForUpdate userGroupDto)
        {
            // check Patch!! @
            var mappedUserGroup = _mapper.Map<UserGroup>(userGroupDto);
            await _unitOfWork.UserGroups.UpdateAsync(mappedUserGroup);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteUserGroupsAsync(List<int> ids)
        {
            // kirill
            var allEntitiesExists = _unitOfWork.UserGroups.GetAllQueryable(true)
                .Select(userGroup => userGroup.Id).Intersect(ids).Count() == ids.Count;
            if (!allEntitiesExists)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, "UserGroup not found");
            }
            _unitOfWork.UserGroups.DeleteRange(ids);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<GroupGeneralViewModel>> GetAllUserGroupsAsync(string userId)
        {
            var drinks = _unitOfWork.UserGroups.GetAllQueryable(true)
                .Where(userGroup => userGroup.UserId == userId)
                .Select(userGroup => userGroup.Group);
            return _mapper.Map<List<GroupGeneralViewModel>>(drinks);
        }

        public async Task<List<ApplicationUserGeneralViewModel>> GetAllGroupUsersAsync(int groupId)
        {
            var drinks = _unitOfWork.UserGroups.GetAllQueryable(true)
                .Where(userGroup => userGroup.GroupId == groupId)
                .Select(userGroup => userGroup.User);
            return _mapper.Map<List<ApplicationUserGeneralViewModel>>(drinks);
        }

        public async Task<ApplicationUser> GetGroupAdminAsync(int groupId)
        {
            var userGroup = await _unitOfWork.UserGroups.GetAllQueryable(true)
                .FirstOrDefaultAsync(ug => ug.GroupId == groupId && ug.Role == UserGroupRole.Admin);
            return userGroup.User;
        }

        public async Task CreateGroupAdmin(UserGroupDtoForCreate userGroupDto)
        {
            var mappedUserGroup = _mapper.Map<UserGroup>(userGroupDto);
            mappedUserGroup.Role = UserGroupRole.Admin;
            mappedUserGroup.Status = GroupMembershipStatus.Member;
            await _unitOfWork.UserGroups.CreateAsync(mappedUserGroup);
            await _unitOfWork.SaveAsync();
        }
    }
}
