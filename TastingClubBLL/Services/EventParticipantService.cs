﻿using AutoMapper;
using System.Net;
using TastingClubBLL.DTOs.EventParticipantDTOs;
using TastingClubBLL.Exceptions;
using TastingClubBLL.Interfaces.IServices;
using TastingClubBLL.ViewModels.ApplicationUserViewModels;
using TastingClubDAL.Interfaces;
using TastingClubDAL.Models;

namespace TastingClubBLL.Services
{
    public class EventParticipantService : IEventParticipantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EventParticipantService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> CreateEventParticipantAsync(List<EventParticipantDtoForCreate> eventParticipantDtos)
        {
            var mappedEventParticipant = _mapper.Map<List<EventParticipant>>(eventParticipantDtos);
            await _unitOfWork.EventParticipants.CreateRangeAsync(mappedEventParticipant);
            await _unitOfWork.SaveAsync();
            return 1;
        }

        public async Task DeleteEventParticipantAsync(List<int> ids)
        {
            var allEntitiesExists = _unitOfWork.EventParticipants.GetAllQueryable(true)
                .Select(eventParticipant => eventParticipant.Id).Intersect(ids).Count() == ids.Count;
            if (!allEntitiesExists)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, "EventParticipant not found");
            }
            _unitOfWork.EventParticipants.DeleteRange(ids);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<ApplicationUserGeneralViewModel>> GetAllEventParticipantAsync(int eventId)
        {
            var users = _unitOfWork.EventParticipants.GetAllQueryable(true)
                .Where(eventParticipant => eventParticipant.EventId == eventId)
                .Select(eventParticipant => eventParticipant.User);
            return _mapper.Map<List<ApplicationUserGeneralViewModel>>(users);
        }
    }
}
