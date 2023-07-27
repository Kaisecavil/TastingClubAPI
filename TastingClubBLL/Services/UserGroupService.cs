using AutoMapper;
using System.Net;
using TastingClubBLL.DTOs.UserGroupDTOs;
using TastingClubBLL.Exceptions;
using TastingClubBLL.Interfaces.IServices;
using TastingClubBLL.ViewModels.ApplicationUserViewModels;
using TastingClubBLL.ViewModels.DrinkViewModels;
using TastingClubBLL.ViewModels.GroupViewModels;
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

        public async Task DeleteUserGroupsAsync(List<int> ids)
        {
            if (ids.Any(async id => !await _unitOfWork.UserGroups.EntityExistsAsync(id)))
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, "UserGroup not found");
            }
            var a = ids.All(async id => await _unitOfWork.UserGroups.EntityExistsAsync(id));
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
    }
}
