using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastingClubBLL.DTOs.EventDTOs;
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
        EventService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateEventAsync(EventDtoForCreate eventDto)
        {
            var mappedEvent = _mapper.Map<Event>(eventDto);
            await _unitOfWork.Events.CreateAsync(mappedEvent);
        }

        public async Task DeleteEvent(int id)
        {
            if(!await _unitOfWork.Events.EntityExistsAsync(id))
            {
                throw new Exception();
            }
            await _unitOfWork.Events.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<EventGeneralViewModel>> GetAllEventsAsync()
        {
            var events = await _unitOfWork.Events.GetAllAsync();
            return _mapper.Map<List<EventGeneralViewModel>>(events);
        }

        public async Task<EventDetailViewModel> GetEventAsync(int id)
        {
            if (!await _unitOfWork.Events.EntityExistsAsync(id))
            {
                throw new Exception();
            }
            return _mapper.Map<EventDetailViewModel>(await _unitOfWork.Events.GetAsync(id));
        }

        public async Task UpdateEvent(EventDtoForUpdate eventDto)
        {
            //if (!await _unitOfWork.Events.EntityExistsAsync(id))
            //{
            //    throw new Exception();
            //}
            //await _unitOfWork.Events.GetAsync(id);
        }
    }
}
