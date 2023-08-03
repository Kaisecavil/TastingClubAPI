using AutoMapper;
using System.Net;
using TastingClubBLL.DTOs.EventDTOs;
using TastingClubBLL.Exceptions;
using TastingClubBLL.Interfaces.IProvider;
using TastingClubBLL.Interfaces.IServices;
using TastingClubBLL.ViewModels.EventViewModels;
using TastingClubDAL.Interfaces;
using TastingClubDAL.Models;

namespace TastingClubBLL.Services
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IApplicationUserProvider _userProvider;
        private readonly IUserGroupService _userGroupService;

        public EventService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IApplicationUserProvider userProvider,
            IUserGroupService usergroupService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userProvider = userProvider;
            _userGroupService = usergroupService;
        }

        public async Task<int> CreateEventAsync(EventDtoForCreate eventDto)
        {
            if(!await _unitOfWork.Groups.EntityExistsAsync(eventDto.GruopId))
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, $"Can't find group with id = {eventDto.GruopId}");
            }
            var mappedEvent = _mapper.Map<Event>(eventDto);
            await _unitOfWork.Events.CreateAsync(mappedEvent);
            await _unitOfWork.SaveAsync();
            return mappedEvent.Id;
        }

        public async Task DeleteEventAsync(int id)
        {
            if(!await _unitOfWork.Events.EntityExistsAsync(id))
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, "Event not found"); 
            }
            var currentUserId = await _userProvider.GetUserIdAsync();
            var eventToDeleteGroupAdmin  = await _userGroupService.GetGroupAdminAsync(id);
            if (eventToDeleteGroupAdmin.Id != currentUserId)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "You can't delete events of groups due to lack of your rights in this group");
            }
            await _unitOfWork.Events.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<EventGeneralViewModel>> GetAllEventsAsync()
        {
            var events = await _unitOfWork.Events.GetAllAsync(true);
            return _mapper.Map<List<EventGeneralViewModel>>(events);
        }

        public async Task<EventDetailViewModel> GetEventAsync(int id)
        {
            var entity = await _unitOfWork.Events.GetAsync(id,true);
            if (entity == null)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, "Event not found");
            }
            return _mapper.Map<EventDetailViewModel>(entity);
        }

        public async Task UpdateEventAsync(EventDtoForUpdate eventDto)
        {
            if (!await _unitOfWork.Events.EntityExistsAsync(eventDto.Id))
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, "Event not found");
            }
            await _unitOfWork.Events.UpdateAsync(_mapper.Map<Event>(eventDto));
            await _unitOfWork.SaveAsync();
        }
    }
}
