using AutoMapper;
using System.Net;
using TastingClubBLL.DTOs.DrinkDTOs;
using TastingClubBLL.Exceptions;
using TastingClubBLL.Interfaces.IServices;
using TastingClubBLL.ViewModels.DrinkViewModels;
using TastingClubDAL.Interfaces;
using TastingClubDAL.Models;

namespace TastingClubBLL.Services
{
    public class DrinkService : IDrinkService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DrinkService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> CreateDrinkAsync(DrinkDtoForCreate drinkDto)
        {
            var mappedDrink = _mapper.Map<Drink>(drinkDto);
            await _unitOfWork.Drinks.CreateAsync(mappedDrink);
            await _unitOfWork.SaveAsync();
            return mappedDrink.Id;
        }

        public async Task DeleteDrinkAsync(int id)
        {
            if (!await _unitOfWork.Drinks.EntityExistsAsync(id))
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, "Drink not found");
            }
            await _unitOfWork.Drinks.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<DrinkGeneralViewModel>> GetAllDrinksAsync()
        {
            var drinks = await _unitOfWork.Drinks.GetAllAsync();
            return _mapper.Map<List<DrinkGeneralViewModel>>(drinks);
        }

        public async Task<DrinkDetailViewModel> GetDrinkAsync(int id)
        {
            var entity = await _unitOfWork.Drinks.GetAsync(id);
            if (entity == null)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, "Drink not found");
            }
            return _mapper.Map<DrinkDetailViewModel>(entity);
        }

        public async Task UpdateDrinkAsync(DrinkDtoForUpdate drinkDto)
        {
            if (!await _unitOfWork.Drinks.EntityExistsAsync(drinkDto.Id))
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, "Drink not found");
            }
            await _unitOfWork.Drinks.UpdateAsync(_mapper.Map<Drink>(drinkDto));
            await _unitOfWork.SaveAsync();
        }
    }
}
