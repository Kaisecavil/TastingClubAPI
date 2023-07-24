using AutoMapper;
using System.Net;
using TastingClubBLL.DTOs.EventDrinkDTOs;
using TastingClubBLL.Exceptions;
using TastingClubBLL.Interfaces.IServices;
using TastingClubBLL.ViewModels.DrinkViewModels;
using TastingClubDAL.Interfaces;
using TastingClubDAL.Models;

namespace TastingClubBLL.Services
{
    public class EventDrinkService : IEventDrinkService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EventDrinkService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> CreateEventDrinksAsync(List<EventDrinkDtoForCreate> eventDrinksDtos)
        {
            var mappedEventDrinks = _mapper.Map<List<EventDrink>>(eventDrinksDtos);
            await _unitOfWork.EventDrinks.CreateRangeAsync(mappedEventDrinks);
            await _unitOfWork.SaveAsync();
            return 1;
        }

        public async Task DeleteEventDrinksAsync(List<int> ids)
        {
            if (ids.Any(async id => !await _unitOfWork.EventDrinks.EntityExistsAsync(id)))
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, "EventDrink not found");
            }
            await _unitOfWork.EventDrinks.DeleteRange(ids);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<DrinkGeneralViewModel>> GetAllEventDrinksAsync(int eventId)
        {
            var drinks = _unitOfWork.EventDrinks.GetAllQueryable(true)
                .Where(eventDrink => eventDrink.EventId == eventId)
                .Select(eventDrink => eventDrink.Drink);
            return _mapper.Map<List<DrinkGeneralViewModel>>(drinks);
        }

    }
}
