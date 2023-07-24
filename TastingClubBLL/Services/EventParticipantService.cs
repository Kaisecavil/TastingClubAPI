﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TastingClubBLL.DTOs.EventParticipantDTOs;
using TastingClubBLL.Exceptions;
using TastingClubBLL.Interfaces.IServices;
using TastingClubBLL.ViewModels.DrinkViewModels;
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
            if (ids.Any(async id => !await _unitOfWork.EventParticipants.EntityExistsAsync(id)))
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, "EventParticipant not found");
            }
            await _unitOfWork.EventParticipants.DeleteRange(ids);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<DrinkGeneralViewModel>> GetAllEventParticipantAsync(int eventId)
        {
            var drinks = _unitOfWork.EventParticipant.GetAllQueryable(true)
                .Where(eventDrink => eventDrink.EventId == eventId)
                .Select(eventDrink => eventDrink.Drink);
            return _mapper.Map<List<DrinkGeneralViewModel>>(drinks);
        }
    }
}