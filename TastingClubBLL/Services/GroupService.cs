using AutoMapper;
using System.Net;
using TastingClubBLL.DTOs.GroupDTOs;
using TastingClubBLL.Exceptions;
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

        public GroupService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> CreateGroupAsync(GroupDtoForCreate groupDto)
        {
            var mappedGroup = _mapper.Map<Group>(groupDto);
            await _unitOfWork.Groups.CreateAsync(mappedGroup);
            await _unitOfWork.SaveAsync();
            return mappedGroup.Id;
        }

        public async Task DeleteGroupAsync(int id)
        {
            if (!await _unitOfWork.Groups.EntityExistsAsync(id))
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, "Group not found");
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
            await _unitOfWork.Groups.UpdateAsync(_mapper.Map<Group>(groupDto));
            await _unitOfWork.SaveAsync();
        }
    }
}

