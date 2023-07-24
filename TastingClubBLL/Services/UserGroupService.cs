using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TastingClubBLL.DTOs.UserGroupDTOs;
using TastingClubBLL.Exceptions;
using TastingClubBLL.ViewModels.DrinkViewModels;
using TastingClubDAL.Interfaces;
using TastingClubDAL.Models;

namespace TastingClubBLL.Services
{
    public class UserGroupService
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
            _unitOfWork.UserGroups.DeleteRange(ids);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<DrinkGeneralViewModel>> GetAllUserGroupsAsync(int eventId)
        {
            var drinks = _unitOfWork.UserGroups.GetAllQueryable(true)
                .Where(userGroup => userGroup.EventId == eventId)
                .Select(userGroup => userGroup.Drink);
            return _mapper.Map<List<DrinkGeneralViewModel>>(drinks);
        }
    }
}
